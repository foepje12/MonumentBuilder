using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Projects
{
    public class ProjectCardManager : MonoBehaviour
    {
        public GameObject ProjectCardsHolder;
        public GameObject CardPrefab;
        
        public Project CurrentProject;
        public bool IsPlacedDown;

        private ProjectList _projectList;
        private List<GameObject> _projectCardList;

        public void Start()
        {
            _projectList = new ProjectList();
            _projectCardList = new List<GameObject>();

        }

        public void Update()
        {
            if (CurrentProject == null && ProjectCardsHolder.transform.childCount == 0)
                GenerateProjects();
        }

        public void GenerateProjects()
        {
            Project.ProjectDifficulty[] toGenerate = { Project.ProjectDifficulty.EASY, Project.ProjectDifficulty.NORMAL, Project.ProjectDifficulty.HARD};

            for (var i = 0; i < toGenerate.Length; i++)
                GenerateProject(toGenerate[i], -150 * -(i - 1));
        }

        public void GenerateProject(Project.ProjectDifficulty difficulty, int xPosition)
        {
            var project = _projectList.GetRandomProject(difficulty);

            //Instantiate Project Card
            var projectCard = Instantiate(CardPrefab);
            projectCard.transform.SetParent(ProjectCardsHolder.transform);
            _projectCardList.Add(projectCard);

            //Set the Project Card
            projectCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, 100);
            var cardScript = projectCard.GetComponent<ProjectCard>();
            cardScript.Project = project;
            
            projectCard.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => ChooseProject(project));
        }

        public void ChooseProject(Project project)
        {
            project.Building.GetPositions(90);

            _projectCardList.ForEach(Destroy);
            IsPlacedDown = false;
            CurrentProject = project;
        }
    }
}
