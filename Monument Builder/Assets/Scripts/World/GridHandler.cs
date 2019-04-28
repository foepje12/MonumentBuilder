using System.Collections.Generic;
using Assets.Scripts.Projects;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.World
{
    public class GridHandler : MonoBehaviour
    {
        public enum Level { FULL, NETHERLANDS, GERMANY, FRANCE }

        public GameObject TilePrefab;
        public GameObject GhostBuilding;
        public Dictionary<Vector2, GameObject> TileDictionary; //Used for checking if the tile is empty and space calculations

        public GameObject CurrentBuilding;

        private ProjectCardManager _projectCardManager;
        private GameVariables _gameVariables;
        private Camera _camera;

        private Vector3 _inProgressLocation;
        private int _spaceBetweenTiles = 1;

        public void Start()
        {
            _projectCardManager = GetComponent<ProjectCardManager>();
            _gameVariables = GameObject.Find("GAME VARIABLES").GetComponent<GameVariables>();
            _camera = Camera.main;
            TileDictionary = new Dictionary<Vector2, GameObject>();


            string levelGrid;

            switch (_gameVariables.CurrentLevel)
            {
                case Level.NETHERLANDS:
                    levelGrid = Levels.GridNetherlands; break;
                case Level.GERMANY:
                    levelGrid = Levels.GridGermany; break;
                case Level.FRANCE:
                    levelGrid = Levels.GridFrance; break;
                default:
                    levelGrid = Levels.GridFull;
                    break;
            }

            InitiateGrid(levelGrid);
        }

        private int _rotation = -90;
        public void Update()
        {
            //If the current project == null or we have already placed a building down, return;
            if (_projectCardManager.CurrentProject == null || _projectCardManager.IsPlacedDown)
                return;

            var building = _projectCardManager.CurrentProject.Building;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            //If we have no visual ghost building
            if (GhostBuilding == null)
            {
                var prefab = Resources.Load<GameObject>($"BuildingShapes/{_gameVariables.CurrentLevel}/{_gameVariables.CurrentLevel}_{_projectCardManager.CurrentProject.Building.ShapeName}");
                GhostBuilding = Instantiate(prefab);
            }


            if (Physics.Raycast(ray, out var hit) == false)
                return;

            var obj = hit.transform.gameObject;

            //If we did not hit a tile, return
            if (hit.collider == null || obj.tag != "Tile")
                return;

            //Set the building rotation based on scrollwheel
            _rotation += 90 * (int)(Input.GetAxis("Mouse ScrollWheel") * 10);

            var tile = obj.GetComponent<Tile>();
            var positions = building.GetPositions(_rotation);

            //Update the ghost building
            UpdateGhostBuilding(tile, positions);

            //Update the colours of the tiles, and check if a building can fit
            bool canFit = UpdateTiles(tile, positions);

            if (canFit && Input.GetMouseButtonUp(0))
            {
                _inProgressLocation = new Vector3(tile.Position.x, 0, tile.Position.y);
                _projectCardManager.IsPlacedDown = true;
            }
        }

        public void InitiateGrid(string level)
        {
            var gridHolder = GameObject.Find("GRID");
            var levelLines = level.Split('\n');

            var z = -1;
            for (var i = levelLines.Length - 1; i >= 0; i--)
            {
                var x = 0;
                foreach (char c in levelLines[i])
                {
                    switch (c)
                    {
                        case 'X':
                            var tile = Instantiate(TilePrefab);
                            var pos = new Vector2(x, z);

                            tile.transform.SetParent(gridHolder.transform);
                            tile.GetComponent<Tile>().Position = pos;
                            tile.transform.position = new Vector3(x, 0, z) * _spaceBetweenTiles;

                            TileDictionary.Add(pos, tile);
                            break;
                        default: break;
                    }
                    x++;
                }
                z++;
            }
        }

        private void UpdateGhostBuilding(Tile tile, List<Vector2> positions)
        {
            GhostBuilding.transform.position = new Vector3(tile.Position.x, 0, tile.Position.y) * _spaceBetweenTiles;
            GhostBuilding.transform.eulerAngles = new Vector3(0, -90 - _rotation, 0);
        }

        private bool UpdateTiles(Tile centerTile, List<Vector2> positions)
        {
            //Set every tile gray
            foreach (var keyValuePair in TileDictionary)
                keyValuePair.Value.GetComponent<Renderer>().material.color = Color.gray;

            //Check if there is a position which does not exist as a tile
            foreach (var pos in positions)
            {
                var newVecPos = centerTile.Position + pos;
                newVecPos.x = Mathf.RoundToInt(newVecPos.x);
                newVecPos.y = Mathf.RoundToInt(newVecPos.y);

                //If there is not tile at the given position, the building can not be placed
                if (TileDictionary.ContainsKey(newVecPos) == false)
                    return false;

                //If the tile is not empty (already something build on)
                if (TileDictionary[newVecPos].GetComponent<Tile>().IsEmpty)
                    continue;

                return false;
            }

            foreach (var pos in positions)
            {
                var newVecPos = centerTile.Position + pos;
                newVecPos.x = Mathf.RoundToInt(newVecPos.x);
                newVecPos.y = Mathf.RoundToInt(newVecPos.y);

                TileDictionary[newVecPos].GetComponent<Renderer>().material.color = Color.red;
            }
            return true;
        }

        public void FinishBuilding(bool positive)
        {
            Destroy(GhostBuilding);

            if (positive == false)
            {
                GhostBuilding = null;
                return;
            }

            var newBuilding = Instantiate(GhostBuilding);
            newBuilding.transform.position = _inProgressLocation * _spaceBetweenTiles;
            GhostBuilding = null;

            foreach (var tileObject in TileDictionary)
            {
                var positions = _projectCardManager.CurrentProject.Building.GetPositions(_rotation);
                var tile = tileObject.Value.GetComponent<Tile>();

                foreach (var pos in positions)
                {
                    var offsetPos = new Vector2(pos.x + _inProgressLocation.x, pos.y + _inProgressLocation.z);
                    bool samePos = Mathf.RoundToInt(offsetPos.x) == Mathf.RoundToInt(tile.Position.x) && Mathf.RoundToInt(offsetPos.y) == Mathf.RoundToInt(tile.Position.y);

                    if (samePos)
                        tile.IsEmpty = false;
                }
            }
        }
    }
}
