using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Data
{
    public enum PlanningPokerSessionState
    {
        Lobby,
        Active,
        Suspended,
        Complete,
        Error
    }
}
