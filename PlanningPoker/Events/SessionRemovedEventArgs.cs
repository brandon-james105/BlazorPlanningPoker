using PlanningPoker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Events
{

    public class SessionRemovedEventArgs : EventArgs
    {
        public string SessionId { get; set; }

        public SessionRemovedEventArgs(string sessionId)
        {
            SessionId = sessionId;
        }
    }
}
