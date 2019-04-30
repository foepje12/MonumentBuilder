using System;
using Assets.Scripts.Projects;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Events
{
    public class EventCardManager : MonoBehaviour
    {
        public GameObject EventPrefab;

        private GameObject _canvas;
        private EventList _eventList;

        private ProjectProgressor _projectProgressor;
        private GameObject _currentEventCard;
        private GridHandler _gridHandler;

        public void Start()
        {
            _canvas = GameObject.Find("Canvas");
            _gridHandler = GetComponent<GridHandler>();
            _eventList = new EventList();
        }

        public void CreateEvent()
        {
            var eventObj = _eventList.GetRandomEvent(_gridHandler.CurrentLevel);

            _projectProgressor = GameObject.FindGameObjectWithTag("ProjectProgress").GetComponent<ProjectProgressor>();

            _currentEventCard = Instantiate(EventPrefab);
            _currentEventCard.transform.SetParent(_canvas.transform);

            _currentEventCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            _currentEventCard.GetComponent<EventCard>().Event = eventObj;
        }

        public void ChooseEventOption(EventOption eventOption)
        {
            switch (eventOption.OptionType)
            {
                case EventOption.EventOptionType.FUNDING:
                    _projectProgressor.AddFundingCost(eventOption.Amount);
                    break;

                case EventOption.EventOptionType.MONTHS:
                    _projectProgressor.AddOverTime(eventOption.Amount);
                    break;

                case EventOption.EventOptionType.CANCEL:
                    _projectProgressor.CancelProject();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            Destroy(_currentEventCard);
            _projectProgressor.EventInProgress = false;
        }
    }
}
