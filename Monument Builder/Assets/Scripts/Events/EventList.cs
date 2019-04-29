using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Events
{
    public class EventList
    {
        private static readonly List<EventOption> OptionsList = new List<EventOption>
        {
            /*00*/ new EventOption("Cancel", EventOption.EventOptionType.CANCEL, -1),
            /*01*/ new EventOption("Buy More materials",EventOption.EventOptionType.FUNDING, 1000),
            /*02*/ new EventOption("Wait", EventOption.EventOptionType.MONTHS, 8),

            /*03*/ new EventOption("Louvre", EventOption.EventOptionType.FUNDING, -5000),
            /*04*/ new EventOption("Klompen Museum", EventOption.EventOptionType.FUNDING, 1000),

            /*05*/ new EventOption("Bordeaux", EventOption.EventOptionType.FUNDING, 5000),
            /*06*/ new EventOption("Paris", EventOption.EventOptionType.FUNDING, -1000),

            /*07*/ new EventOption("17", EventOption.EventOptionType.MONTHS, 4),
            /*08*/ new EventOption("More than 17", EventOption.EventOptionType.MONTHS, -4),


            /*09*/ new EventOption("Pain", EventOption.EventOptionType.FUNDING, -4),
            /*10*/ new EventOption("Le Bread", EventOption.EventOptionType.FUNDING, 8),
            
            /*11*/ new EventOption("l'Hiver", EventOption.EventOptionType.MONTHS, 2),
            /*12*/ new EventOption("Été", EventOption.EventOptionType.FUNDING, -10),

            /*13*/ new EventOption("Pay him off", EventOption.EventOptionType.FUNDING, 5000),
            /*14*/ new EventOption("Delay the project", EventOption.EventOptionType.MONTHS, 3)

        };

        private static readonly List<Event> EventsList = new List<Event>
        {
            //Positive Events


            //Quiz Events
            new Event("Quiz Time!", "Which of the following two is a museum in Paris?", OptionsList[3], OptionsList[4]),
            new Event("Quiz Time!", "Which of the following two is the capital of France?", OptionsList[5], OptionsList[6]),
            new Event("Quiz Time!", "How many countries in the world speak French?", OptionsList[7], OptionsList[8]),

            new Event("Quiz Time!", "What is the French word for: Bread?", OptionsList[9], OptionsList[10]),
            new Event("Quiz Time!", "Which of the following seasons is the hottest?", OptionsList[11], OptionsList[12]),


            //Negative Events
            new Event("Out of Materials!", "We are out of materials! We could buy more or cancel the project", OptionsList[1], OptionsList[0]),
            new Event("Slow Progress!", "Construction is going slower than expected. We need at least 8 more months, or we can spend 1000 coins to hire more people", OptionsList[2], OptionsList[1]),

            new Event("Blackmail!", "The local mayor wants to see money! Pay him or delay the project", OptionsList[2], OptionsList[1])
        };

        public Event GetRandomEvent()
        {
            //TODO CHANGE TO RANDOM
            var randomEvent = EventsList[Random.Range(0, EventsList.Count)];
            randomEvent.Occurance += 1;

            return randomEvent;
        }
    }
}
