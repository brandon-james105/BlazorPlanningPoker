using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlanningPoker.Data
{
    public interface IPlanningPokerSession
    {
        string Id { get; set; }

        string Title { get; set; }

        PlanningPokerSessionState State { get; set; }

        ISet<ClaimsPrincipal> ConnectedUsers { get; set; }

        ClaimsPrincipal Host { get; set; }

        Stack<PlanningPokerPlay> Plays { get; set; }

        DateTime CreatedDateTime { get; set; }

        DateTime? StartTime { get; set; }

        DateTime? EndTime { get; set; }

        int MaxParticipants { get; set; }

        ICollection<string> Stories { get; set; }

        bool Start();

        bool End();
    }
}
