using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MvvmBlazor.Components;
using PlanningPoker.ViewModels;
using System;

namespace PlanningPoker.Views
{
    [Authorize]
    public class SessionBase : MvvmComponentBase<SessionViewModel>
    {
        [Parameter]
        public string SessionId { get; set; }
    }
}
