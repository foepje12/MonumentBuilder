using System.Collections.Generic;

namespace Assets.Scripts.Events
{
    public class EventList
    {
        private static readonly List<EventOption> OptionsList = new List<EventOption>
        {
            new EventOption("Cancel", EventOption.EventOptionType.CANCEL, -1),
            new EventOption("Buy More materials",EventOption.EventOptionType.FUNDING, 1000),
            new EventOption("Wait", EventOption.EventOptionType.MONTHS, 8)
        };

        private static readonly List<Event> EventsList = new List<Event>
        {
            //Positive Events

            //Negative Events
            new Event("Out of Materials!", "We have Unfortunately tun out of materials. We could buy more or cancel the project", OptionsList[1], OptionsList[0]),
            new Event("Overtime", "Construction is going slower than expected. We need at least 8 more months, or we can spend 1000 coins to hire more people", OptionsList[2], OptionsList[1])
        };
        
        public Event GetRandomEvent()
        {
            //TODO CHANGE TO RANDOM
            var randomEvent = EventsList[1];
            randomEvent.Occurance += 1;

            return randomEvent;
        }
    }
}
