using Microsoft.AspNetCore.Identity;
using PlanningPoker.Data;
using PlanningPoker.Events;
using PlanningPoker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlanningPoker.Services
{
    public class PlanningPokerSessionManager : IPlanningPokerSessionManager
    {
        public event EventHandler SessionCreated;
        public event EventHandler SessionRemoved;
        public event EventHandler UserConnectedToSession;
        public event EventHandler UserDisconnectedFromSession;
        public event EventHandler SessionStateChanged;

        private IDictionary<string, IPlanningPokerSession> Sessions { get; set; }

        public PlanningPokerSessionManager()
        {
            Sessions = new Dictionary<string, IPlanningPokerSession>();
        }

        public IPlanningPokerSession CreateSession(ClaimsPrincipal host, string title)
        {
            var session = new SessionViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Host = host,
                Title = title
            };

            Sessions.Add(session.Id, session);

            SessionCreated.Invoke(this, new SessionCreatedEventArgs(session));

            return Sessions[session.Id];
        }

        public IPlanningPokerSession GetSession(string Id)
        {
            return Sessions[Id];
        }

        public bool StartSession(string Id)
        {
            var success = Sessions[Id].Start();

            SessionStateChanged.Invoke(this, new SessionStateChangedEventArgs(Id, Sessions[Id].State));

            return success;
        }

        public bool EndSession(string Id)
        {
            var success = Sessions[Id].End();

            SessionStateChanged.Invoke(this, new SessionStateChangedEventArgs(Id, Sessions[Id].State));

            return success;
        }

        public bool RemoveSession(string Id)
        {
            var success = Sessions.Remove((Id));

            SessionRemoved.Invoke(this, new SessionRemovedEventArgs(Id));

            return success;
        }

        public ICollection<string> GetStories(string Id)
        {
            return Sessions[Id].Stories;
        }

        public ISet<IPlanningPokerSession> GetSessions()
        {
            return new HashSet<IPlanningPokerSession>(Sessions.Values);
        }

        public bool ConnectUserToSession(ClaimsPrincipal user, string Id)
        {
            UserConnectedToSession(this, new UserConnectedToSessionEventArgs(user, Id));

            return Sessions[Id].ConnectedUsers.Add(user);
        }

        public bool DisconnectUserFromSession(ClaimsPrincipal user, string Id)
        {
            UserDisconnectedFromSession(this, new UserConnectedToSessionEventArgs(user, Id));

            return Sessions[Id].ConnectedUsers.Remove(user);
        }

        public bool DisconnectUserFromLiveSessions(ClaimsPrincipal user)
        {
            var allSuccessfulGuestDisconnects = Sessions.Values.Where(s => s.ConnectedUsers.Select(i => user.Identity.Name).Contains(user.Identity.Name))
                                                               .Select(s => s.ConnectedUsers.Remove(user))
                                                               .All(d => d == true);

            var allSuccessfulHostDisconnects = Sessions.Values.Where(s => s.Host.Identity.Name == user.Identity.Name)
                                                              .Select(s =>
                                                              {
                                                                  s.State = PlanningPokerSessionState.Complete;
                                                                  s.ConnectedUsers.Clear();
                                                                  return true;
                                                              })
                                                              .All(d => d == true);

            return allSuccessfulGuestDisconnects && allSuccessfulHostDisconnects;
        }
    }
}
