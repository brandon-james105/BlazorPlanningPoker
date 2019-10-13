using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MvvmBlazor.Components;
using PlanningPoker.Services;
using PlanningPoker.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Views
{
    public class SessionsBase : MvvmComponentBase<SessionListViewModel>
    {
    }
}
