using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Gaswerk.RouteApp.Interfaces.Authorization;
using Gaswerk.RouteApp.Interfaces.Repositories;
using Gaswerk.RouteApp.Logic.Authorization;
using Gaswerk.RouteApp.Logic.Repositories;
using Gaswerk.RouteApp.Models;

using Ninject;
using Ninject.Web.Common;

namespace Gaswerk.RouteApp.Logic
{
    public class Resolver : IDependencyResolver
    {
        private static readonly Resolver _Instance = new Resolver(new StandardKernel());

        private readonly IKernel _Kernel;

        public Resolver(IKernel kernel)
        {
            _Kernel = kernel;
            ResolveBindings();
        }

        private void ResolveBindings()
        {
            _Kernel.Bind<IKundeRepository>().To<KundeRepository>().InSingletonScope();
            _Kernel.Bind<IRouteRepository>().To<RouteRepository>().InSingletonScope();
            _Kernel.Bind<IAuthorizationProvider>().To<AuthorizationProvider>().InSingletonScope();
        }

        public static T Get<T>()
        {
            return Resolver._Instance._Kernel.Get<T>();
        }

        /// <inheritdoc />
        public object GetService(Type serviceType)
        {
            return _Kernel.TryGet(serviceType);
        }

        /// <inheritdoc />
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Kernel.GetAll(serviceType);
        }
    }
}