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
        string SessionId { get; set; }

        string Title { get; set; }

        PlanningPokerSessionState State { get; set; }

        ClaimsPrincipal Host { get; set; }

        DateTime CreatedDateTime { get; set; }

        DateTime? StartTime { get; set; }

        DateTime? EndTime { get; set; }

        int MaxParticipants { get; set; }

        ObservableCollection<ClaimsPrincipal> ConnectedUsers { get; set; }

        ObservableCollection<PlanningPokerPlay> Plays { get; set; }

        ObservableCollection<string> Stories { get; set; }

        bool Start();

        bool End();
    }
}
