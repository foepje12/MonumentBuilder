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
            _cameraAnim = Camera.main.gameObject.GetComponent<Animator>();
        }


        public void Update()
        {
            if (CurrentProject == null && HasGenerated == false)
            {
                HasGenerated = true;
                GenerateProjects();
            }

        }

        private int _projectCounter;
        /// <summary>
        /// Get 3 project cards
        /// </summary>
        public void GenerateProjects()
        {
            if (_projectCounter != 0)
                _cameraAnim.SetTrigger("ToOverview");
            _cardHolderAnim.SetTrigger("Popup");

            Debug.Log("New Project");

            _projectCounter++;

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

            //Add button click
            ProjectCards[index].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => ChooseProject(project));
        }

        public void ChooseProject(Project project)
        {
            _cardHolderAnim.SetTrigger("PopDown");
            var projectProgress = Instantiate(ProjectProgressPrefab);
            projectProgress.transform.SetParent(GameObject.Find("Canvas").transform);
            projectProgress.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

            project.Building.GetPositions(90);

            _projectCardList.ForEach(Destroy);
            IsPlacedDown = false;
            CurrentProject = project;
        }

        public void RerollProjects()
        {
            CurrentProject = null;
            HasGenerated = false;

            _gameManager.AddTime(Random.Range(6, 18));
        }
    }
}
