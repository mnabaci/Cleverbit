using Castle.Windsor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cleverbit.Infrastructure
{
    internal class MvcDependencyResolver : IDependencyResolver
    {
        IWindsorContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">The castle.</param>
        public MvcDependencyResolver(IWindsorContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        /// <summary>
        /// Resolves single registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <returns>
        /// The requested service or object.
        /// </returns>
        public object GetService(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
            {
                return _container.Resolve(serviceType);
            }

            return null;
        }

        /// <summary>
        /// Resolves multiple registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>
        /// The requested services.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
            {
                return _container.ResolveAll(serviceType).Cast<object>();
            }

            return new object[] { };
        }

        /// <summary>
        /// Releases the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Release(object instance)
        {
            _container.Release(instance);
        }
    }
}