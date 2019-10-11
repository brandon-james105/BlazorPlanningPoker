using Microsoft.AspNetCore.Identity;
using PlanningPoker.Data;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlanningPoker.Services
{
    public interface IPlanningPokerSessionManager
    {
        event EventHandler SessionCreated;
        event EventHandler SessionRemoved;
        event EventHandler UserConnectedToSession;
        event EventHandler UserDisconnectedFromSession;
        event EventHandler SessionStateChanged;

        ISet<IPlanningPokerSession> GetSessions();
        IPlanningPokerSession CreateSession(ClaimsPrincipal host, string title);
        bool EndSession(string sessionId);
        IPlanningPokerSession GetSession(string sessionId);
        bool RemoveSession(string sessionId);
        bool StartSession(string sessionId);
        bool ConnectUserToSession(ClaimsPrincipal user, string sessionId);
        bool DisconnectUserFromSession(ClaimsPrincipal user, string sessionId);
        bool DisconnectUserFromLiveSessions(ClaimsPrincipal user);
    }
}