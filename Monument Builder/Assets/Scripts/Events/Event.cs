namespace Assets.Scripts.Events
{
    public class Event
    {
        public int Occurance;

        public string Title;
        public string Description;
        public EventOption Option1;
        public EventOption Option2;

        public Event(string title, string description, EventOption option1, EventOption option2)
        {
            Title = title;
            Description = description;
            Option1 = option1;
            Option2 = option2;
        }
    }
}