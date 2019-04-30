using Assets.Scripts.World;

namespace Assets.Scripts.Events
{
    public class Event
    {
        public int Occurance;

        public GridHandler.Level Level;
        public string Title;
        public string Description;
        public EventOption Option1;
        public EventOption Option2;

        public Event(GridHandler.Level level, string title, string description, EventOption option1, EventOption option2)
        {
            Level = level;
            Title = title;
            Description = description;
            Option1 = option1;
            Option2 = option2;
        }
    }
}