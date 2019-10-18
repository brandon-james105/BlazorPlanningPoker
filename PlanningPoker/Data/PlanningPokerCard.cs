using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Data
{
    public class PlanningPokerCard
    {
        public string FrontDisplay { get; set; }

        public double Value { get; set; }

        public PlanningPokerCard(double value)
        {
            Value = value;
            FrontDisplay = Value.ToString();
        }

        public PlanningPokerCard(string frontDisplay, double value)
        {
            FrontDisplay = frontDisplay;
            Value = value;
        }
    }
}
