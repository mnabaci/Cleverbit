using Castle.Windsor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;

namespace Cleverbit.Infrastructure
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;
        private static readonly Type WebApiControllerType = typeof(IHttpController);

        public DependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }
        public IDependencyScope BeginScope()
        {
            return new DependencyScope(_container);
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ?
                _container.Kernel.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ?
                _container.Kernel.ResolveAll(serviceType).Cast<object>() : new object[] { };
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}