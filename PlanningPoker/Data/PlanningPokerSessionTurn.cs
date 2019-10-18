using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Data
{
    public class PlanningPokerSessionTurn : IPlanningPokerSessionTurn
    {
        public string Id { get; set; }

        public IPlanningPokerStory Story { get; set; }

        public HashSet<IPlanningPokerPlay> Plays { get; set; }
        public bool IsRevealed { get; set; }

        public PlanningPokerSessionTurn()
        {
            Plays = new HashSet<IPlanningPokerPlay>();
            IsRevealed = false;
        }
    }
}
