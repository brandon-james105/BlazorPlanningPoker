using PlanningPoker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Events
{
    public class SessionCreatedEventArgs : EventArgs
    {
        public IPlanningPokerSession Session { get; set; }

        public SessionCreatedEventArgs(IPlanningPokerSession session)
        {
            Session = session;
        }
    }
}
