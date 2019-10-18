using PlanningPoker.Data;
using PlanningPoker.Events;
using PlanningPoker.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;

namespace PlanningPoker.Services
{
    public class PlanningPokerSessionManager : IPlanningPokerSessionManager
    {
        private readonly IRepository<IPlanningPokerSession> _sessionsRepository;

        public event EventHandler SessionCreated;
        public event EventHandler SessionRemoved;

        public event EventHandler UserConnectedToSession;
        public event EventHandler UserDisconnectedFromSession;
        public event EventHandler SessionStateChanged;
        public event EventHandler TurnChanged;
        public event EventHandler CardPlayed;
        public event EventHandler RevealCardsChanged;
        public event EventHandler StoryValuePicked;

        public PlanningPokerSessionManager(IRepository<IPlanningPokerSession> sessionsRepository)
        {
            _sessionsRepository = sessionsRepository;
        }

        public IPlanningPokerSession CreateSession(ClaimsPrincipal host, IPlanningPokerSession session)
        {
            IPlanningPokerSession newSession = new SessionViewModel
            {
                SessionId = Guid.NewGuid().ToString(),
                Host = host,
                Title = session?.Title,
                Stories = new ObservableCollection<IStoryViewModel>(session?.Stories.Select(s => (IStoryViewModel)s))
            };

            newSession.Turns = new HashSet<IPlanningPokerSessionTurn>(session?.Stories.Select(s => new PlanningPokerSessionTurn 
                                                                                                    {
                                                                                                        Id = Guid.NewGuid().ToString(),
                                                                                                        Story = s
                                                                                                    }));

            newSession = _sessionsRepository.Create(newSession);

            SessionCreated?.Invoke(this, new SessionCreatedEventArgs(newSession));

            return newSession;
        }

        public IPlanningPokerSession GetSession(string sessionId)
        {
            return _sessionsRepository.Find(sessionId);
        }

        public ISet<IPlanningPokerSession> GetSessions()
        {
            return _sessionsRepository.List().ToHashSet();
        }

        public bool StartSession(string sessionId)
        {
            var session = _sessionsRepository.Find(sessionId);
            var success = session.StartSession();

            SessionStateChanged?.Invoke(this, new SessionStateChangedEventArgs(sessionId, session.State));

            return success;
        }

        public bool EndSession(string sessionId)
        {
            var session = _sessionsRepository.Find(sessionId);
            var success = session.StartSession();

            SessionStateChanged?.Invoke(this, new SessionStateChangedEventArgs(sessionId, session.State));

            return success;
        }

        public bool RemoveSession(string sessionId)
        {
            var success = _sessionsRepository.Delete(sessionId);

            SessionRemoved?.Invoke(this, new SessionRemovedEventArgs(sessionId));

            return success;
        }

        public bool ConnectUserToSession(ClaimsPrincipal user, string sessionId)
        {
            _sessionsRepository.Find(sessionId).ConnectedUsers.Add(user);
            UserConnectedToSession(this, new UserConnectedToSessionEventArgs(user, sessionId));

            return true;
        }

        public bool DisconnectUserFromSession(ClaimsPrincipal user, string sessionId)
        {
            var success = _sessionsRepository.Find(sessionId).ConnectedUsers.Remove(user);
            UserDisconnectedFromSession(this, new UserDisconnectedFromSessionEventArgs(user, sessionId));

            return success;
        }

        public bool DisconnectUserFromLiveSessions(ClaimsPrincipal user)
        {
            var sessions = _sessionsRepository.List();
            var allSuccessfulGuestDisconnects = sessions.Where(s => s.ConnectedUsers.Select(i => user.Identity.Name).Contains(user.Identity.Name))
                                                               .Select(s => s.ConnectedUsers.Remove(user))
                                                               .All(d => d == true);

            var allSuccessfulHostDisconnects = sessions.Where(s => s.Host.Identity.Name == user.Identity.Name)
                                                              .Select(s =>
                                                              {
                                                                  s.State = PlanningPokerSessionState.Complete;
                                                                  s.ConnectedUsers.Clear();
                                                                  return true;
                                                              })
                                                              .All(d => d == true);

            return allSuccessfulGuestDisconnects && allSuccessfulHostDisconnects;
        }

        public void PrevTurn(string sessionId)
        {
            var session = _sessionsRepository.Find(sessionId);
            SetTurn(session.CurrentTurnIndex - 1, sessionId);
        }

        public void NextTurn(string sessionId)
        {
            var session = _sessionsRepository.Find(sessionId);
            SetTurn(session.CurrentTurnIndex + 1, sessionId);
        }

        public void SetTurn(int index, string sessionId)
        {
            var session = _sessionsRepository.Find(sessionId);
            session.CurrentTurnIndex = index;
            _sessionsRepository.Update(session);

            TurnChanged?.Invoke(this, new SessionTurnChangedEventArgs(sessionId, session.CurrentTurnIndex));
        }

        public void PlayCard(ClaimsPrincipal user, PlanningPokerCard card, string sessionId)
        {
            var session = _sessionsRepository.Find(sessionId);
            var play = new PlanningPokerPlay
            {
                Id = Guid.NewGuid().ToString(),
                Card = card,
                User = user,
                Story = session.CurrentTurn.Story,
                CreatedDateTime = DateTime.Now
            };
            session.CurrentTurn.Plays.Add(play);
            
            CardPlayed?.Invoke(this, new CardPlayedEventArgs(sessionId, play));
        }

        public void RevealCards(string sessionId)
        {
            var session = _sessionsRepository.Find(sessionId);
            session.CardsRevealed = true;

            RevealCardsChanged?.Invoke(this, new RevealCardsArgs(sessionId, session.CurrentTurnIndex, session.CardsRevealed));
        }

        public void HideCards(string sessionId)
        {
            var session = _sessionsRepository.Find(sessionId);
            session.CardsRevealed = false;

            RevealCardsChanged?.Invoke(this, new RevealCardsArgs(sessionId, session.CurrentTurnIndex, session.CardsRevealed));
        }

        public void PickStoryPoints(string sessionId, int storyIndex, double storyPoints)
        {
            var session = _sessionsRepository.Find(sessionId);
            session.Stories.ElementAt(storyIndex).Points = storyPoints;

            StoryValuePicked?.Invoke(this, new StoryPointsPickedEventArgs(sessionId, storyIndex, storyPoints));
        }
    }
}
