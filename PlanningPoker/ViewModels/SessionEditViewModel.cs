using Microsoft.AspNetCore.Components;
using MvvmBlazor.ViewModel;
using PlanningPoker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.ViewModels
{
    public class SessionEditViewModel : ViewModelBase
    {
        private string sessionId;
        private ISessionViewModel sessionViewModel;
        private IStoryViewModel newStory;
        private readonly IPlanningPokerSessionManager _sessionManager;

        [Parameter]
        public string SessionId
        {
            get => sessionId;
            set => Set(ref sessionId, value);
        }

        public ISessionViewModel Session
        {
            get => sessionViewModel;
            set => Set(ref sessionViewModel, value);
        }
        public IStoryViewModel NewStory
        {
            get => newStory;
            set => Set(ref newStory, value);
        }

        private bool storyIsFocused;

        public bool StoryIsFocused
        {
            get => storyIsFocused;
            set => Set(ref storyIsFocused, value);
        }

        public ObservableCollection<IStoryViewModel> Stories { get => Session.Stories; }

        public SessionEditViewModel(IPlanningPokerSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            NewStory = new StoryViewModel { Id = Guid.NewGuid().ToString() };
        }

        public void FocusStoryInput()
        {
            StoryIsFocused = true;
        }

        public void BlurStoryInput()
        {
            StoryIsFocused = false;
        }

        public void AddStory()
        {
            if (!string.IsNullOrEmpty(NewStory.Text))
            {
                Session.Stories.Add(NewStory);
            }

            NewStory = new StoryViewModel { Id = Guid.NewGuid().ToString() };
        }

        public void EditSession()
        {
            // TODO: Find out how to apply changes to stories
            var session = _sessionManager.GetSession(SessionId);
            session.Title = Session.Title;
        }

        public override void OnInitialized()
        {
            var session = _sessionManager.GetSession(SessionId);
            Session = new SessionViewModel
            {
                Title = session.Title,
                Stories = new ObservableCollection<IStoryViewModel>(session.Stories.Select(s => (IStoryViewModel)s))
            };
            base.OnInitialized();
        }
    }
}
