using System;
using System.Security.Claims;

namespace PlanningPoker.Data
{
    public class PlanningPokerPlay : IPlanningPokerPlay
    {
        public string Id { get; set; }

        public ClaimsPrincipal User { get; set; }

        public PlanningPokerCard Card { get; set; }

        public IPlanningPokerStory Story { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}