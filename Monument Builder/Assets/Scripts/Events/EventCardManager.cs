using System;
using Assets.Scripts.Projects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Events
{
    public class EventCardManager : MonoBehaviour
    {
        public GameObject EventPrefab;
        public ProjectProgressor ProjectProgressor;

        private GameObject _canvas;
        private EventList _eventList;

        private GameObject _currentEventCard;

        public void Start()
        {
            _canvas = GameObject.Find("Canvas");
            _eventList = new EventList();
        }

        public void CreateEvent()
        {
            var eventObj = _eventList.GetRandomEvent();

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
                    ProjectProgressor.AddFundingCost(eventOption.Amount);
                    break;

                case EventOption.EventOptionType.MONTHS:
                    ProjectProgressor.AddOverTime(eventOption.Amount);
                    break;

                case EventOption.EventOptionType.CANCEL:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            Destroy(_currentEventCard);
            ProjectProgressor.EventInProgress = false;
        }
    }
}
