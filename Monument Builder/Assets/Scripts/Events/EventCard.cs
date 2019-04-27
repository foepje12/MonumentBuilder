using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Events
{
    public class EventCard : MonoBehaviour
    {
        public Text EventTitle;
        public Text EventDescription;

        public Text Option1;
        public Text Option2;

        public Event Event;

        public void Start()
        {
            EventTitle.text = Event.Title;
            EventDescription.text = Event.Description;
            Option1.text = Event.Option1.Description;
            Option2.text = Event.Option2.Description;

            transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => ChooseEventOption(Event.Option1));
            transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => ChooseEventOption(Event.Option2));
        }

        public void ChooseEventOption(EventOption eventOption)
        {
            var manager = GameObject.Find("GameManager").GetComponent<EventCardManager>();
            manager.ChooseEventOption(eventOption);
        }
    }
}
