using System;
using PlanningPoker.Data;

namespace PlanningPoker.Events
{
    internal class CardPlayedEventArgs : EventArgs
    {
        public string SessionId { get; set; }
        public PlanningPokerPlay Play { get; set; }

        public CardPlayedEventArgs(string id, PlanningPokerPlay play)
        {
            SessionId = id;
            Play = play;
        }
    }
}