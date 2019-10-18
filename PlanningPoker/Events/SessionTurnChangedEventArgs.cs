using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Events
{
    public class SessionTurnChangedEventArgs : EventArgs
    {
        public string SessionId { get; set; }

        public int CurrentTurn { get; set; }

        public SessionTurnChangedEventArgs(string sessionId, int currentTurn)
        {
            SessionId = sessionId;
            CurrentTurn = currentTurn;
        }
    }
}
