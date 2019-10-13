using Microsoft.AspNetCore.Components;
using MvvmBlazor.ViewModel;
using PlanningPoker.Data;
using PlanningPoker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlanningPoker.ViewModels
{
    public class SessionViewModel : ViewModelBase, IPlanningPokerSession
    {
        private string sessionId;
        private string title;
        private PlanningPokerSessionState state;
        private ClaimsPrincipal host;
        private DateTime createdDateTime;
        private DateTime? startTime;
        private DateTime? endTime;
        private ObservableCollection<PlanningPokerPlay> plays;
        private ObservableCollection<string> stories;
        private int maxParticipants;
        private ObservableCollection<ClaimsPrincipal> connectedUsers;

        public string SessionId
        {
            get => sessionId;
            set => Set(ref sessionId, value);
        }

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public PlanningPokerSessionState State
        {
            get => state;
            set => Set(ref state, value);
        }

        public ClaimsPrincipal Host
        {
            get => host;
            set => Set(ref host, value);
        }

        public DateTime CreatedDateTime
        {
            get => createdDateTime;
            set => Set(ref createdDateTime, value);
        }

        public DateTime? StartTime
        {
            get => startTime;
            set => Set(ref startTime, value);
        }

        public DateTime? EndTime
        {
            get => endTime;
            set => Set(ref endTime, value);
        }

        public int MaxParticipants
        {
            get => maxParticipants;
            set => Set(ref maxParticipants, value);
        }

        public ObservableCollection<ClaimsPrincipal> ConnectedUsers
        {
            get => connectedUsers;
            set => Set(ref connectedUsers, value);
        }

        public ObservableCollection<PlanningPokerPlay> Plays
        {
            get => plays;
            set => Set(ref plays, value);
        }

        public ObservableCollection<string> Stories
        {
            get => stories;
            set => Set(ref stories, value);
        }

        public SessionViewModel()
        {
            ConnectedUsers = new ObservableCollection<ClaimsPrincipal>();
            Plays = new ObservableCollection<PlanningPokerPlay>();
            Stories = new ObservableCollection<string>();
        }

        public bool Start()
        {
            State = PlanningPokerSessionState.Active;

            return true;
        }

        public bool End()
        {
            State = PlanningPokerSessionState.Complete;

            return true;
        }
    }
}
