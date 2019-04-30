using System.Collections;
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

        private int _overBudget;
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
                StartCoroutine(EndProject(true));
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

            int max = 3;//  ((int)project.Difficulty + 1) * 2 * 12 + Random.Range(3, 16);
            _maxProgress = max;

            string startMonthText = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(_gameManager.CurrentMonth);
            YearStart.text = $"{startMonthText} {_gameManager.CurrentYear}";

            string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(_maxProgress % 12 + 1);
            YearEnd.text = $"{month} {_gameManager.CurrentYear + _maxProgress / 12}";

            ProjectName.text = project.Name;

            //Project funding text
            ProjectFundingCost.text = $"{100 + _overBudget}%";


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
            if (rand > 90 && _currentProgress > 5 && _currentProgress < _maxProgress - 5)
            {
                EventInProgress = true;
                _eventCardManager.CreateEvent();
            }
        }

        public IEnumerator EndProject(bool positive)
        {
            _gameManager.AddTime(_maxProgress);

            if (positive)
                _gameManager.CreateNewspaper("Project Finished", "Citizens very happy!", "Close", () => { });
            else
            {
                string description = 100 + (double)_overBudget > 125 ? "Budget too high!" : "Architect decides to stop work";
                _gameManager.CreateNewspaper("Project Failed", description, "Close", () => { });
            }


            _maxProgress = 0;
            _gridHandler.FinishBuilding(positive);

            if (_gridHandler.TilesLeft() == 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().IsGameEnded = true;
                _gridHandler.InstantiateVictory();
            }

            _projectCardManager.CurrentProject = null;
            _projectCardManager.HasGenerated = false;

            GetComponent<Animator>().SetTrigger("PopUp");
            yield return new WaitForSeconds(1);

            //TODO SHOW ENDING SCREEN
            Destroy(gameObject);
        }


        /// <summary>
        /// Add more time to the project
        /// </summary>
        /// <param name="months"></param>
        public void AddOverTime(int months)
        {
            _maxProgress += months;

            string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(_maxProgress % 12 + 1);
            YearEnd.text = $"{month} {_gameManager.CurrentYear + _maxProgress / 12}";
        }

        public void AddFundingCost(int amount)
        {
            _overBudget += amount;
            var percentage = 100 + (double)_overBudget;
            ProjectFundingCost.text = $"{percentage}%";

            if (percentage >= 125)
            {
                //TODO SHOW Project failed
                StartCoroutine(EndProject(false));
            }
        }

        public void CancelProject()
        {
            StartCoroutine(EndProject(false));
        }
    }
}
