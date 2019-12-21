using Cleverbit.Infrastructure;

using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethod(typeof(WindsorActivator), "Shutdown")]
namespace Cleverbit.Infrastructure
{
    public static class WindsorActivator
    {
        static ContainerBootstrapper bootstrapper;

        public static void PreStart()
        {
            bootstrapper = ContainerBootstrapper.Bootstrap();
        }

        public static void Shutdown()
        {
            if (bootstrapper != null)
                bootstrapper.Dispose();
        }
    }
}