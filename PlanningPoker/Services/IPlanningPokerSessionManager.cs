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
        event EventHandler TurnChanged;
        event EventHandler CardPlayed;
        event EventHandler RevealCardsChanged;
        event EventHandler StoryValuePicked;

        ISet<IPlanningPokerSession> GetSessions();
        IPlanningPokerSession CreateSession(ClaimsPrincipal host, IPlanningPokerSession newSession);
        bool EndSession(string sessionId);
        IPlanningPokerSession GetSession(string sessionId);
        bool RemoveSession(string sessionId);
        bool StartSession(string sessionId);
        bool ConnectUserToSession(ClaimsPrincipal user, string sessionId);
        bool DisconnectUserFromSession(ClaimsPrincipal user, string sessionId);
        bool DisconnectUserFromLiveSessions(ClaimsPrincipal user);
        void PlayCard(ClaimsPrincipal user, PlanningPokerCard card, string sessionId);
        void PrevTurn(string sessionId);
        void NextTurn(string sessionId);
        void SetTurn(int index, string sessionId);
        void RevealCards(string sessionId);
        void HideCards(string sessionId);
        void PickStoryPoints(string sessionId, int storyIndex, double storyPoints);
    }
}