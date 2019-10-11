using PlanningPoker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Events
{
    public class SessionStateChangedEventArgs : EventArgs
    {
        public string SessionId { get; set; }
        public PlanningPokerSessionState State { get; set; }

        public SessionStateChangedEventArgs(string sessionId, PlanningPokerSessionState state)
        {
            SessionId = sessionId;
            State = state;
        }
    }
}
