using Castle.Windsor;

using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;

namespace Cleverbit.Infrastructure
{
    public class HttpControllerActivator : IHttpControllerActivator
    {
        private IDependencyResolver _resolver;
        private IWindsorContainer _container;

        /// <summary>
        /// TODO: Buraya yorum eklenecek
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="container"></param>
        public HttpControllerActivator(IDependencyResolver resolver, IWindsorContainer container)
        {
            _resolver = resolver;
            _container = container;
        }

        /// <summary>
        /// TODO: Buraya yorum eklenecek
        /// </summary>
        /// <param name="request"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = (IHttpController)this._resolver.GetService(controllerType);
            request.RegisterForDispose(new Release(() => this._container.Release(controller)));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action release;

            public Release(Action release)
            {
                this.release = release;
            }

            public void Dispose()
            {
                this.release();
            }
        }
    }
}