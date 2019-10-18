using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlanningPoker.Data
{
    public interface IPlanningPokerSession
    {
        IPlanningPokerSessionTurn CurrentTurn { get; }

        string SessionId { get; set; }

        string Title { get; set; }

        int CurrentTurnIndex { get; set; }

        PlanningPokerSessionState State { get; set; }

        bool CardsRevealed { get; set; }

        ClaimsPrincipal Host { get; set; }

        DateTime CreatedDateTime { get; set; }

        DateTime? StartTime { get; set; }

        DateTime? EndTime { get; set; }

        int MaxParticipants { get; set; }

        HashSet<ClaimsPrincipal> ConnectedUsers { get; set; }

        HashSet<IPlanningPokerStory> Stories { get; set; }

        HashSet<IPlanningPokerSessionTurn> Turns { get; set; }

        bool StartSession();

        bool EndSession();
    }
}
