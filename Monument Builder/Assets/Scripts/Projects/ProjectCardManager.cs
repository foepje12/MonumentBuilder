using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Projects
{
    public class ProjectCardManager : MonoBehaviour
    {
        public GameObject ProjectProgressPrefab;

        public Project CurrentProject;
        public GameObject ProjectCardsHolder;
        public GameObject[] ProjectCards;
        public bool IsPlacedDown;
        public bool HasGenerated;

        private ProjectList _projectList;
        private List<GameObject> _projectCardList;
        private GameManager _gameManager;

        private Animator _cardHolderAnim;
        private Animator _cameraAnim;

        public void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _projectList = new ProjectList();
            _projectCardList = new List<GameObject>();

            _cardHolderAnim = ProjectCardsHolder.GetComponent<Animator>();
            _cameraAnim = GameObject.Find("CameraRotator").GetComponent<Animator>();
        }

        public void Update()
        {
            if (CurrentProject == null && HasGenerated == false && _gameManager.IsGameEnded == false)
            {
                HasGenerated = true;
                StartCoroutine(GenerateProjects(false));
            }

        }

        /// <summary>
        /// Get 3 project cards
        /// </summary>
        public IEnumerator GenerateProjects(bool reroll)
        {
            if (reroll == false)
            {
                _cameraAnim.SetTrigger("ToOverview");
                yield return new WaitForSeconds(1.5f);
                _cardHolderAnim.SetTrigger("Popup");
            }

            Project.ProjectDifficulty[] toGenerate = { Project.ProjectDifficulty.EASY, Project.ProjectDifficulty.NORMAL, Project.ProjectDifficulty.HARD };

            for (var i = 0; i < toGenerate.Length; i++)
                GenerateProject(toGenerate[i], i);
        }

        public void GenerateProject(Project.ProjectDifficulty difficulty, int index)
        {
            var project = _projectList.GetRandomProject(difficulty);

            //Set the Project Card
            var cardScript = ProjectCards[index].GetComponent<ProjectCard>();
            cardScript.Project = project;
            cardScript.Initialize();

            //Set the Card Building Shape
            var i = 0;
            for (var y = 1; y < 3; y++)
            {
                for (var x = 0; x < 3; x++)
                {
                    bool isBuild = project.Building.IsBuild(new Vector2(x, y));
                    ProjectCards[index].transform.GetChild(2).GetChild(i).gameObject.SetActive(isBuild);
                    i++;
                }
            }

            Button button = ProjectCards[index].transform.GetChild(3).GetComponent<Button>();
            button.onClick.RemoveAllListeners(); // Reset the button

            //Add button click
            button.onClick.AddListener(() => StartCoroutine(ChooseProject(project)));
        }

        public IEnumerator ChooseProject(Project project)
        {
            _cardHolderAnim.SetTrigger("PopDown");
            yield return new WaitForSeconds(0.75f); //Wait for the cards to go down

            var projectProgress = Instantiate(ProjectProgressPrefab);
            projectProgress.transform.SetParent(GameObject.Find("Canvas").transform);
            projectProgress.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

            project.Building.GetPositions(90);
            
            IsPlacedDown = false;
            CurrentProject = project;
        }

        public void CancelBuilding()
        {
            _cardHolderAnim.SetTrigger("Popup");
        }

        public void DestroyCards()
        {
            _projectCardList.ForEach(Destroy);
        }

        public void RerollProjects()
        {
            StartCoroutine(GenerateProjects(true));

            _gameManager.AddTime(Random.Range(6, 18));
        }
    }
}
