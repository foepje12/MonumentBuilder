using System.Collections.Generic;
using Assets.Scripts.Projects;
using UnityEngine;

namespace Assets.Scripts.World
{
    public class GridHandler : MonoBehaviour
    {
        public enum Level { FULL, NETHERLANDS, GERMANY, FRANCE }
        public Level CurrentLevel;

        public GameObject TilePrefab;
        public Dictionary<Vector2, GameObject> TileDictionary; //Used for checking if the tile is empty and space calculations

        public GameObject CurrentBuilding;

        private ProjectCardManager _projectCardManager;
        private Camera _camera;

        public void Start()
        {
            _projectCardManager = GetComponent<ProjectCardManager>();
            _camera = Camera.main;
            TileDictionary = new Dictionary<Vector2, GameObject>();


            string levelGrid;

            switch (CurrentLevel)
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

        public void Update()
        {
            if (_projectCardManager.CurrentProject == null || _projectCardManager.IsPlacedDown)
                return;

            var building = _projectCardManager.CurrentProject.Building;

            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit) == false)
                return;
            
            var obj = hit.transform.gameObject;

            if (hit.collider != null && obj.tag == "Tile")
            {
                var tile = obj.GetComponent<Tile>();

                var positions = building.GetPositions(0);

                foreach (var keyValuePair in TileDictionary)
                {
                    Debug.Log(keyValuePair.Key);
                    keyValuePair.Value.GetComponent<Renderer>().material.color = Color.gray;
                }

                foreach (var pos in positions)
                {
                    Debug.Log(tile.Position + pos);
                    TileDictionary[tile.Position + pos].GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }


        public void InitiateGrid(string level)
        {
            var levelLines = level.Split('\n');

            var z = -1;
            foreach (string t in levelLines)
            {
                var x = 0;
                foreach (char c in t)
                {
                    switch (c)
                    {
                        case 'X':
                            var tile = Instantiate(TilePrefab);
                            var pos = new Vector2(x, z);

                            tile.GetComponent<Tile>().Position = pos;
                            tile.transform.position = new Vector3(x * 2, 0, z * 2);

                            TileDictionary.Add(pos, tile);
                            break;
                        default: break;
                    }
                    x++;
                }
                z++;
            }
        }
    }
}
