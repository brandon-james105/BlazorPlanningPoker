using System;

namespace PlanningPoker.Events
{
    internal class RevealCardsArgs : EventArgs
    {
        public string SessionId { get; set; }
        public int CurrentTurnIndex { get; set; }
        public bool CardsRevealed { get; set; }

        public RevealCardsArgs(string id, int currentTurnIndex, bool cardsRevealed)
        {
            SessionId = id;
            CurrentTurnIndex = currentTurnIndex;
            CardsRevealed = cardsRevealed;
        }
    }
}