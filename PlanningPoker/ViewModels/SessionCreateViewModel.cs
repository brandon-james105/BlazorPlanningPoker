using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using MvvmBlazor.ViewModel;
using PlanningPoker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.ViewModels
{
    public class SessionCreateViewModel : ViewModelBase
    {
        private SessionViewModel newSession;
        private IStoryViewModel newStory;

        private readonly IPlanningPokerSessionManager _sessionManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly NavigationManager _navigationManager;

        public SessionViewModel NewSession
        {
            get => newSession;
            set => Set(ref newSession, value);
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

        public ObservableCollection<IStoryViewModel> Stories { get => NewSession.Stories; }

        public SessionCreateViewModel(IPlanningPokerSessionManager sessionManager,
                                        IHttpContextAccessor httpContext,
                                        NavigationManager navigationManager)
        {
            _sessionManager = sessionManager;
            _httpContext = httpContext;
            _navigationManager = navigationManager;
            NewSession = new SessionViewModel();
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
                NewSession.Stories.Add(NewStory);
            }

            NewStory = new StoryViewModel { Id = Guid.NewGuid().ToString() };
        }

        public void RemoveStory(int index)
        {
            var story = NewSession.Stories.ElementAt(index);
            NewSession.Stories.Remove(story);
        }

        public void CreateSession()
        {
            if (StoryIsFocused)
            {
                AddStory();
            }
            else
            {
                if (Stories.Count > 0)
                {
                    var session = _sessionManager.CreateSession(_httpContext.HttpContext.User, NewSession);
                    NewSession = new SessionViewModel();
                    _navigationManager.NavigateTo($"Sessions/{session.SessionId}");
                }
            }
        }
    }
}
