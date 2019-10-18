using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Data
{
    public class PlanningPokerStory : IPlanningPokerStory
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public double Points { get; set; }
    }
}
