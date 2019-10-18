using Microsoft.AspNetCore.Components;
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

namespace PlanningPoker.ViewModels
{
    public class SessionDetailViewModel : ViewModelBase
    {
        private string sessionId;
        private ISessionViewModel sessionViewModel;
        private int currentTurnIndex;
        private PlanningPokerCard selectedCard;
        private IPlanningPokerPlay selectedPlay;
        private PlanningPokerSessionState state;
        private readonly IPlanningPokerSessionManager _sessionManager;
        private readonly IHttpContextAccessor _httpContext;

        [Parameter]
        public string SessionId
        {
            get => sessionId;
            set => Set(ref sessionId, value);
        }

        public ISessionViewModel Session
        {
            get => sessionViewModel;
            set => Set(ref sessionViewModel, value);
        }

        public int CurrentTurnIndex
        {
            get => currentTurnIndex;
            private set => Set(ref currentTurnIndex, value);
        }

        public PlanningPokerCard SelectedCard
        {
            get => selectedCard;
            set => Set(ref selectedCard, value);
        }

        public IPlanningPokerPlay SelectedPlay
        {
            get => selectedPlay;
            set => Set(ref selectedPlay, value);
        }

        public PlanningPokerSessionState State
        {
            get => state;
            set => Set(ref state, value);
        }

        public ClaimsPrincipal User { get => _httpContext.HttpContext.User; }

        public IList<PlanningPokerCard> Cards { get; set; }

        public ObservableCollection<ClaimsPrincipal> ConnectedUsers { get; set; }
        public ObservableCollection<ITurnViewModel> Turns { get; set; }
        public ObservableCollection<IStoryViewModel> Stories { get; set; }

        public ITurnViewModel CurrentTurn { get => Turns.ElementAt(CurrentTurnIndex); }

        public SessionDetailViewModel(IPlanningPokerSessionManager sessionManager,
                                        IHttpContextAccessor httpContext)
        {
            _sessionManager = sessionManager;
            _httpContext = httpContext;
            _sessionManager.UserConnectedToSession += UserConnectedToSession;
            _sessionManager.UserDisconnectedFromSession += UserDisconnectedFromSession;
            _sessionManager.TurnChanged += TurnChanged;
            _sessionManager.CardPlayed += CardPlayed;
            _sessionManager.RevealCardsChanged += RevealCardsChanged;
            _sessionManager.StoryValuePicked += StoryValuePicked;
            _sessionManager.SessionStateChanged += SessionStateChanged;
            Cards = new List<PlanningPokerCard>();
        }

        public override void OnInitialized()
        {
            // Some error happens here when the page is refreshed
            if (Cards?.Count == 0)
            {
                Cards = Constants.PlanningPokerFaces.Values.ToList();
            }

            Session = _sessionManager.GetSession(SessionId) as ISessionViewModel;
            ConnectedUsers = Session.ConnectedUsers;
            Stories = Session.Stories;
            Turns = Session.Turns;
            State = Session.State;

            _sessionManager.ConnectUserToSession(User, SessionId);
        }

        private void SessionStateChanged(object sender, EventArgs e)
        {
            var evt = (SessionStateChangedEventArgs)e;

            if (evt.SessionId == SessionId)
            {
                State = evt.State;
            }
        }

        private void StoryValuePicked(object sender, EventArgs e)
        {
            var evt = (StoryPointsPickedEventArgs)e;

            if (evt.SessionId == SessionId)
            {
                Turns.ElementAt(evt.StoryIndex).Story.Points = evt.StoryPoints;
            }
        }

        private void RevealCardsChanged(object sender, EventArgs e)
        {
            var evt = (RevealCardsArgs)e;

            if (evt.SessionId == SessionId)
            {
                Turns.ElementAt(evt.CurrentTurnIndex).IsRevealed = evt.CardsRevealed;
            }
        }

        private void CardPlayed(object sender, EventArgs e)
        {
            var evt = (CardPlayedEventArgs)e;
            if (evt.SessionId == SessionId)
            {
                Session.Turns.ElementAt(CurrentTurnIndex).Plays.Add(evt.Play);
            }
        }

        private void TurnChanged(object sender, EventArgs e)
        {
            var evt = (SessionTurnChangedEventArgs)e;
            if (evt.SessionId == SessionId)
            {
                CurrentTurnIndex = evt.CurrentTurn;
                SelectedPlay = CurrentTurn.Plays.FirstOrDefault(p => p.Card.Value == CurrentTurn.Story.Points);
            }
        }

        private void UserDisconnectedFromSession(object sender, EventArgs e)
        {
            var evt = (UserDisconnectedFromSessionEventArgs)e;

            if (evt.SessionId == SessionId)
            {
                ConnectedUsers.Remove(evt.User);
            }
        }

        private void UserConnectedToSession(object sender, EventArgs e)
        {
            var evt = (UserConnectedToSessionEventArgs)e;

            if (evt.SessionId == SessionId)
            {
                if (!ConnectedUsers.Contains(evt.User))
                {
                    ConnectedUsers.Add(evt.User);
                }
            }
        }

        public void StartSession()
        {
            _sessionManager.StartSession(SessionId);
        }

        public void EndSession()
        {
            _sessionManager.EndSession(SessionId);
        }

        public void PickStoryValue()
        {
            _sessionManager.PickStoryPoints(SessionId, CurrentTurnIndex, SelectedPlay.Card.Value);
        }

        public void PlayCard()
        {
            if (selectedCard != null)
            {
                var usersPlayedThisTurn = CurrentTurn.Plays.Select(t => t.User);

                // Only play the card if the user hasn't played a card for this turn
                if (!usersPlayedThisTurn.Contains(User))
                {
                    CurrentTurn.Plays.Add(new PlanningPokerPlay
                    {
                        Card = SelectedCard,
                        CreatedDateTime = DateTime.Now,
                        User = User
                    });
                }
            }
        }

        public void ToggleCardsRevealed()
        {
            if (CurrentTurn.IsRevealed)
            {
                _sessionManager.HideCards(SessionId);
            }
            else
            {
                _sessionManager.RevealCards(SessionId);
            }
        }

        public void SetTurn(int index)
        {
            if (index >= 0 && index < Turns.Count)
            {
                _sessionManager.SetTurn(index, SessionId);
            }
        }

        public void PrevTurn()
        {
            if (CurrentTurnIndex > 0)
            {
                _sessionManager.PrevTurn(SessionId);
            }
        }

        public void NextTurn()
        {
            if (CurrentTurnIndex < Turns.Count - 1)
            {
                _sessionManager.NextTurn(SessionId);
            }
        }

        protected override void Dispose(bool disposing)
        {
            SelectedCard = null;
            SelectedPlay = null;
            _sessionManager.DisconnectUserFromSession(User, SessionId);
            base.Dispose(disposing);
        }
    }
}
