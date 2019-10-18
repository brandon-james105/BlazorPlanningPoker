using Microsoft.AspNetCore.Components;
using MvvmBlazor.Components;
using PlanningPoker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Views
{
    public class SessionEditBase : MvvmComponentBase<SessionEditViewModel>
    {
        [Parameter]
        public string SessionId { get; set; }
    }
}
