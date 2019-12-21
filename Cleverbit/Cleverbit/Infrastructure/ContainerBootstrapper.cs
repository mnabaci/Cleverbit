using Castle.Windsor;
using Castle.Windsor.Installer;

using System;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;

namespace Cleverbit.Infrastructure
{
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        readonly IWindsorContainer container;
        readonly IDependencyResolver resolver;

        ContainerBootstrapper(IWindsorContainer container, IDependencyResolver resolver)
        {
            this.container = container;
            this.resolver = resolver;
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }
        public IDependencyResolver Resolver
        {
            get { return resolver; }
        }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer().
                Install(FromAssembly.This());

            var resolver = new DependencyResolver(container);

            var activator = new HttpControllerActivator(resolver, container);

            System.Web.Mvc.ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(
                new MvcDependencyResolver(container)));

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),
                                            new HttpControllerActivator(resolver, container));

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            return new ContainerBootstrapper(container, resolver);
        }

        public void Dispose()
        {
            if (Container != null)
                Container.Dispose();
        }
    }
}