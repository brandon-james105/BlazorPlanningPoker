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
        private string title;
        private string id;
        private PlanningPokerSessionState state;
        private ClaimsPrincipal host;
        private DateTime createdDateTime;
        private DateTime? startTime;
        private DateTime? endTime;
        private ObservableCollection<PlanningPokerPlay> plays;

        public string Id
        {
            get => id;
            set => Set(ref id, value);
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

        public ObservableCollection<ClaimsPrincipal> ConnectedUsers { get; set; }

        public ClaimsPrincipal Host
        {
            get => host;
            set => Set(ref host, value);
        }

        public ObservableCollection<PlanningPokerPlay> Plays
        {
            get => plays; 
            set => Set(ref plays, value);
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

        public int MaxParticipants { get; set; }

        public ICollection<string> Stories { get; set; }

        ISet<ClaimsPrincipal> IPlanningPokerSession.ConnectedUsers
        {
            get => new HashSet<ClaimsPrincipal>(ConnectedUsers);
            set => ConnectedUsers = new ObservableCollection<ClaimsPrincipal>(value);
        }

        Stack<PlanningPokerPlay> IPlanningPokerSession.Plays
        {
            get => new Stack<PlanningPokerPlay>(Plays);
            set => Plays = new ObservableCollection<PlanningPokerPlay>(value);
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
