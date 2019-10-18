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
    public class SessionViewModel : ViewModelBase, ISessionViewModel
    {
        private string sessionId;
        private string title;
        private int maxParticipants;
        private PlanningPokerSessionState state;
        private ClaimsPrincipal host;
        private DateTime createdDateTime;
        private DateTime? startTime;
        private DateTime? endTime;
        private ObservableCollection<ClaimsPrincipal> connectedUsers;
        private ObservableCollection<IStoryViewModel> stories;
        private ObservableCollection<ITurnViewModel> turns;
        private int currentTurnIndex;
        private bool cardsRevealed;

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

        public ObservableCollection<IStoryViewModel> Stories
        {
            get => stories;
            set => Set(ref stories, value);
        }

        public ObservableCollection<ITurnViewModel> Turns
        {
            get => turns;
            set => Set(ref turns, value);
        }

        public int CurrentTurnIndex
        {
            get => currentTurnIndex;
            set => Set(ref currentTurnIndex, value);
        }

        public IPlanningPokerSessionTurn CurrentTurn { get => Turns.ElementAt(CurrentTurnIndex); }

        HashSet<ClaimsPrincipal> IPlanningPokerSession.ConnectedUsers
        {
            get => ConnectedUsers.ToHashSet();
            set => ConnectedUsers = new ObservableCollection<ClaimsPrincipal>(value);
        }

        HashSet<IPlanningPokerStory> IPlanningPokerSession.Stories
        {
            get => Stories.ToHashSet<IPlanningPokerStory>();
            set => Stories = new ObservableCollection<IStoryViewModel>(value.Select(v => (IStoryViewModel)v));
        }

        HashSet<IPlanningPokerSessionTurn> IPlanningPokerSession.Turns
        {
            get => Turns.ToHashSet<IPlanningPokerSessionTurn>();
            set => Turns = new ObservableCollection<ITurnViewModel>(value.Select(v => new TurnViewModel 
                                                                    {
                                                                        IsRevealed = v.IsRevealed,
                                                                        Plays = new ObservableCollection<IPlanningPokerPlay>(v.Plays),
                                                                        Story = (IStoryViewModel)v.Story
                                                                    }));
        }

        public bool CardsRevealed
        {
            get => cardsRevealed;
            set => Set(ref cardsRevealed, value);
        }

        public SessionViewModel()
        {
            ConnectedUsers = new ObservableCollection<ClaimsPrincipal>();
            Stories = new ObservableCollection<IStoryViewModel>();
            Turns = new ObservableCollection<ITurnViewModel>();
        }

        public bool StartSession()
        {
            State = PlanningPokerSessionState.Active;
            StartTime = DateTime.Now;

            return true;
        }

        public bool EndSession()
        {
            State = PlanningPokerSessionState.Complete;
            EndTime = DateTime.Now;

            return true;
        }
    }
}
