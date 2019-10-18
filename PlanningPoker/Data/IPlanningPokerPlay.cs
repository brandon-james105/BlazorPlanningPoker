using System;
using System.Security.Claims;

namespace PlanningPoker.Data
{
    public interface IPlanningPokerPlay
    {
        public string Id { get; set; }
        PlanningPokerCard Card { get; set; }
        DateTime CreatedDateTime { get; set; }
        IPlanningPokerStory Story { get; set; }
        ClaimsPrincipal User { get; set; }
    }
}