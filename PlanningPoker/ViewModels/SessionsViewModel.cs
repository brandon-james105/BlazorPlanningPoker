using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MvvmBlazor.ViewModel;
using PlanningPoker.Data;
using PlanningPoker.Events;
using PlanningPoker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlanningPoker.ViewModels
{
    public class SessionsViewModel : ViewModelBase
    {
        private readonly IPlanningPokerSessionManager _sessionManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        
        private IPlanningPokerSession _newSession;

        public IPlanningPokerSession NewSession 
        {
            get => _newSession; 
            set => Set(ref _newSession, value); 
        }

        private IdentityUser _user;

        public IdentityUser User 
        {
            get => _user; 
            set => Set(ref _user, value); 
        }

        private ObservableCollection<IPlanningPokerSession> _sessions;

        public ObservableCollection<IPlanningPokerSession> Sessions
        {
            get => _sessions;
            set => Set(ref _sessions, value);
        }

        public SessionsViewModel(IPlanningPokerSessionManager sessionManager,
                                    AuthenticationStateProvider authenticationStateProvider)
        {
            _sessionManager = sessionManager;
            _authenticationStateProvider = authenticationStateProvider;
            Sessions = new ObservableCollection<IPlanningPokerSession>(sessionManager.GetSessions());
            sessionManager.SessionCreated += SessionManager_SessionCreated;
            sessionManager.SessionRemoved += SessionManager_SessionRemoved;
            sessionManager.SessionStateChanged += SessionManager_SessionStateChanged;
            NewSession = new SessionViewModel();
        }

        private void SessionManager_SessionStateChanged(object sender, EventArgs e)
        {
            var eventArgs = (SessionStateChangedEventArgs)e;
            Sessions.First(s => s.Id.ToString() == eventArgs.SessionId).State = eventArgs.State;
        }

        private void SessionManager_SessionCreated(object sender, EventArgs e)
        {
            Sessions.Add(((SessionCreatedEventArgs)e).Session);
        }

        private void SessionManager_SessionRemoved(object sender, EventArgs e)
        {
            var session = Sessions.First(s => s.Id.ToString() == ((SessionRemovedEventArgs)e).SessionId);
            Sessions.Remove(session);
        }

        public async Task CreateSessionAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            _sessionManager.CreateSession(authState.User, NewSession.Title);
            NewSession = new SessionViewModel();
        }

        public bool RemoveSession(string sessionId)
        {
            return _sessionManager.RemoveSession(sessionId);
        }
    }
}
