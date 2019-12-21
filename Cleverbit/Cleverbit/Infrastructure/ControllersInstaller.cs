using AutoMapper;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Cleverbit.Entity;
using Cleverbit.Framework.Context;
using Cleverbit.Infrastructure.Resolvers;
using Cleverbit.Service.User;

using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Cleverbit.Infrastructure
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
               Classes.From(typeof(WorkContext))
                   .BasedOn<IWorkContext>()
                   .WithServiceDefaultInterfaces()
                   .Configure(x => x.LifestylePerWebRequest()));

            container.Register(
                Classes.FromAssembly(typeof(CleverbitDbContext).Assembly)
                   .Where(c => c.Name.EndsWith("DbContext"))
                   .WithServiceDefaultInterfaces()
                   .LifestylePerWebRequest());

            container.Register(
                Classes.FromAssembly(typeof(IUserService).Assembly)
                   .Where(c => c.Name.EndsWith("Service"))
                   .WithServiceDefaultInterfaces()
                   .LifestylePerWebRequest());

            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<IController>() //MVC
                    .If(c => c.Name.EndsWith("Controller"))
                    .LifestylePerWebRequest());

            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<IHttpController>() //Web API
                    .If(c => c.Name.EndsWith("Controller"))
                    .LifestylePerWebRequest());

            container.Register(
                Classes.FromAssemblyInThisApplication(GetType().Assembly)
                .BasedOn<Profile>().WithServiceBase());

            container.Register(Component.For<IConfigurationProvider>().UsingFactoryMethod(kernel =>
            {
                return new MapperConfiguration(configuration =>
                {
                    kernel.ResolveAll<Profile>().ToList().ForEach(configuration.AddProfile);
                });
            }).LifestyleSingleton());

            container.Register(Classes.FromAssembly(typeof(UserClaimsResolver).Assembly)
                .BasedOn(typeof(IValueResolver<,,>))
                .WithServiceDefaultInterfaces()
                .Configure(x => x.LifestylePerWebRequest()));

            container.Register(
                Component.For<IMapper>().UsingFactoryMethod(kernel =>
                    new Mapper(kernel.Resolve<IConfigurationProvider>(), kernel.Resolve)));

            //ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(container));
        }
    }
}