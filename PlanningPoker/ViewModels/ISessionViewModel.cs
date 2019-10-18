using PlanningPoker.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlanningPoker.ViewModels
{
    public interface ISessionViewModel : IPlanningPokerSession
    {
        new ObservableCollection<ClaimsPrincipal> ConnectedUsers { get; set; }

        new ObservableCollection<IStoryViewModel> Stories { get; set; }

        new ObservableCollection<ITurnViewModel> Turns { get; set; }
    }
}
