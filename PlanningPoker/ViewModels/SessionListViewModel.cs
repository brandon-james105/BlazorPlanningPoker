using Microsoft.AspNetCore.Http;
using MvvmBlazor.ViewModel;
using PlanningPoker.Data;
using PlanningPoker.Events;
using PlanningPoker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlanningPoker.ViewModels
{
    public class SessionListViewModel : ViewModelBase
    {
        private readonly IPlanningPokerSessionManager _sessionManager;
        private readonly IHttpContextAccessor _httpContext;

        public ClaimsPrincipal User
        {
            get => _httpContext.HttpContext.User;
        }

        private ObservableCollection<IPlanningPokerSession> _sessions;

        public ObservableCollection<IPlanningPokerSession> Sessions
        {
            get => _sessions;
            set => Set(ref _sessions, value);
        }

        public SessionListViewModel(IPlanningPokerSessionManager sessionManager,
                                    IHttpContextAccessor httpContext)
        {
            _sessionManager = sessionManager;
            _httpContext = httpContext;
            Sessions = new ObservableCollection<IPlanningPokerSession>(sessionManager.GetSessions());
            sessionManager.SessionCreated += SessionManager_SessionCreated;
            sessionManager.SessionRemoved += SessionManager_SessionRemoved;
            sessionManager.SessionStateChanged += SessionManager_SessionStateChanged;
        }

        private void SessionManager_SessionStateChanged(object sender, EventArgs e)
        {
            var eventArgs = (SessionStateChangedEventArgs)e;
            Sessions.First(s => s.SessionId == eventArgs.SessionId).State = eventArgs.State;
        }

        private void SessionManager_SessionCreated(object sender, EventArgs e)
        {
            Sessions.Add(((SessionCreatedEventArgs)e).Session);
        }

        private void SessionManager_SessionRemoved(object sender, EventArgs e)
        {
            var session = Sessions.First(s => s.SessionId == ((SessionRemovedEventArgs)e).SessionId);
            Sessions.Remove(session);
        }

        public bool RemoveSession(string sessionId)
        {
            return _sessionManager.RemoveSession(sessionId);
        }
    }
}
