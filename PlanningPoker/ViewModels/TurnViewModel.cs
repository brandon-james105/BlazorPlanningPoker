using MvvmBlazor.ViewModel;
using PlanningPoker.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.ViewModels
{
    public class TurnViewModel : ViewModelBase, ITurnViewModel
    {
        private ObservableCollection<IPlanningPokerPlay> plays;
        private IStoryViewModel story;
        private bool isRevealed;
        private string id;

        public string Id
        {
            get => id;
            set => Set(ref id, value);
        }

        public ObservableCollection<IPlanningPokerPlay> Plays
        {
            get => plays;
            set => Set(ref plays, value);
        }

        public IStoryViewModel Story
        {
            get => story;
            set => Set(ref story, value);
        }

        public bool IsRevealed
        {
            get => isRevealed;
            set => Set(ref isRevealed, value);
        }

        public TurnViewModel()
        {
            Plays = new ObservableCollection<IPlanningPokerPlay>();
        }

        IPlanningPokerStory IPlanningPokerSessionTurn.Story { get => Story; set => Story = (IStoryViewModel)value; }

        HashSet<IPlanningPokerPlay> IPlanningPokerSessionTurn.Plays
        {
            get => Plays.ToHashSet();
            set => Plays = new ObservableCollection<IPlanningPokerPlay>(value.Select(v => v));
        }
    }
}
