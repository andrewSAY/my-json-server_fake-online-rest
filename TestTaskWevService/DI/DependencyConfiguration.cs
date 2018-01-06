using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTaskWevService.Interfaces;
using TestTaskWevService.Providers;
using TestTaskWevService.Services;

namespace TestTaskWevService.DI
{
    public static class DependencyConfiguration
    {
        public static void ConfigurateDependicies(DependencyResolver resolver)
        {
            resolver.Register<IDataProvider, RestDataProvider>();
            resolver.Register<IMainService, MainService>();
        }
    }
}