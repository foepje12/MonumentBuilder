using System.Globalization;
using Assets.Scripts.Events;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Projects
{
    public class ProjectProgressor : MonoBehaviour
    {
        public Text YearStart;
        public Text YearEnd;
        public Text ProjectName;
        public Text ProjectFundingCost;
        public GameObject[] Stars;
        public Scrollbar ProgressBar;

        private GameManager _gameManager;
        private ProjectCardManager _projectCardManager;
        private GridHandler _gridHandler;
        private EventCardManager _eventCardManager;


        public bool EventInProgress;

        private int _turnLength = 1;
        private float _turnTimer; //Every n seconds a new turn (month) 
        private int _maxProgress; //In whole months
        private int _overTime;
        private int _fundingCost;
        private int _currentProgress;

        public void Start()
        {
            var obj = GameObject.Find("GameManager");
            _gameManager = obj.GetComponent<GameManager>();
            _projectCardManager = obj.GetComponent<ProjectCardManager>();
            _gridHandler = obj.GetComponent<GridHandler>();
            _eventCardManager = obj.GetComponent<EventCardManager>();
        }

        public void Update()
        {
            //No project? No progress
            if (_projectCardManager.CurrentProject == null || _projectCardManager.IsPlacedDown == false)
                return;

            if (EventInProgress)
                return;

            var project = _projectCardManager.CurrentProject;

            //If we are at the start of the project, we should set a maximum time
            if (_maxProgress == 0)
                SetupProject(project);

            //If the project is finished:
            if (_currentProgress == _maxProgress)
            {
                EndProject(true);
                return;
            }

            //Do a turn
            if (_turnTimer <= 0)
                DoTurn();

            //TODO Make smooth
            ProgressBar.size = 1f / ((float)_maxProgress / _currentProgress);

            //Remove from the countdown timer;
            _turnTimer -= Time.deltaTime;
        }


        /// <summary>
        /// A project card has been clicked and the building location has been selected
        /// </summary>
        /// <param name="project"></param>
        public void SetupProject(Project project)
        {
            _turnTimer = _turnLength;

            //TODO Change to more random value
            int max = ((int)project.Difficulty + 1) * 2 * 12;
            _maxProgress = max;

            string startMonthText = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(_gameManager.CurrentMonth);
            YearStart.text = YearEnd.text = $"{startMonthText} {_gameManager.CurrentYear}";

            string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(_maxProgress % 12 + 1);
            YearEnd.text = $"{month} {_gameManager.CurrentYear + (double)_maxProgress / 12}";

            ProjectName.text = project.Name;
            AddFundingCost(project.InitialFundingCost);

            for (var i = 0; i < (int)project.Difficulty + 1; i++)
                Stars[i].SetActive(true);
        }

        public void DoTurn()
        {
            _currentProgress++;
            _gameManager.CurrentAge += 1d / 12;
            _turnTimer = _turnLength;

            int rand = Random.Range(0, 100);

            //TODO SET MORE BALANCED RANDOM
            if (rand > 90)
            {
                EventInProgress = true;
                _eventCardManager.CreateEvent();
            }
        }

        public void EndProject(bool positive)
        {
            _gameManager.AddTime(_maxProgress);
            _maxProgress = 0;
            _gridHandler.FinishBuilding(positive);

            _projectCardManager.CurrentProject = null;

            //TODO SHOW ENDING SCREEN
            Debug.Log("Project Done!");

            Destroy(gameObject);
        }


        /// <summary>
        /// Add more time to the project
        /// </summary>
        /// <param name="months"></param>
        public void AddOverTime(int months)
        {
            _overTime += months;
            _maxProgress += months;

            string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(_maxProgress % 12 + 1);
            YearEnd.text = $"{month} {_gameManager.CurrentYear + _maxProgress / 12}";
        }

        public void AddFundingCost(int amount)
        {
            _fundingCost += amount;

            string fundingStr = _fundingCost.ToString("C0");
            ProjectFundingCost.text = "€" + fundingStr.Substring(1, fundingStr.Length - 1);
        }

        public void CancelProject()
        {
            EndProject(false);
        }
    }
}
