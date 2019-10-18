using System.Collections.Generic;
using System.Security.Claims;

namespace PlanningPoker.Data
{
    public interface IPlanningPokerSessionTurn
    {
        public string Id { get; set; }

        HashSet<IPlanningPokerPlay> Plays { get; set; }

        public bool IsRevealed { get; set; }

        IPlanningPokerStory Story { get; set; }
    }
}