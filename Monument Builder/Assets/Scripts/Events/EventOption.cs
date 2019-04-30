using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.World;

namespace Assets.Scripts.Events
{
    public class EventOption
    {
        public enum EventOptionType { FUNDING, MONTHS, CANCEL }

        public string Description;
        public EventOptionType OptionType;
        public int Amount;

        public EventOption( string description, EventOptionType optionType, int amount)
        {
            Description = description;
            OptionType = optionType;
            Amount = amount;
        }
    }
}
