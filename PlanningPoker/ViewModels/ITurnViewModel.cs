using System.Collections.ObjectModel;
using PlanningPoker.Data;

namespace PlanningPoker.ViewModels
{
    public interface ITurnViewModel : IPlanningPokerSessionTurn
    {
        new ObservableCollection<IPlanningPokerPlay> Plays { get; set; }
    }
}