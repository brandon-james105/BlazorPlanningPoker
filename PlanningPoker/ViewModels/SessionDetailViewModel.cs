using Microsoft.AspNetCore.Components;
using MvvmBlazor.ViewModel;
using PlanningPoker.Data;
using PlanningPoker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.ViewModels
{
    public class SessionDetailViewModel : ViewModelBase
    {
        private string sessionId;
        private IPlanningPokerSession sessionViewModel;
        private readonly IPlanningPokerSessionManager _sessionManager;

        [Parameter]
        public string SessionId
        {
            get => sessionId;
            set => Set(ref sessionId, value);
        }

        public IPlanningPokerSession Session
        {
            get => sessionViewModel;
            set => Set(ref sessionViewModel, value);
        }

        public SessionDetailViewModel(IPlanningPokerSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public override Task OnInitializedAsync()
        {
            Session = _sessionManager.GetSession(SessionId);
            return base.OnInitializedAsync();
        }
    }
}
