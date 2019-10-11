using System;
using System.Security.Claims;

namespace PlanningPoker.Data
{
    public class PlanningPokerPlay
    {
        public ClaimsPrincipal User { get; set; }

        public PlanningPokerCard Card { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}