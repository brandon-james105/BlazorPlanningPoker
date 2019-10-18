using MvvmBlazor.ViewModel;
using PlanningPoker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.ViewModels
{
    public class StoryViewModel : ViewModelBase, IStoryViewModel
    {
        private double points;
        private string text;
        private string id;

        public double Points
        {
            get => points;
            set => Set(ref points, value);
        }

        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        public string Id
        {
            get => id;
            set => Set(ref id, value);
        }
    }
}
