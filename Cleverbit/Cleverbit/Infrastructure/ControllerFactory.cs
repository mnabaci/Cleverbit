
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cleverbit.Infrastructure
{
    public class ControllerFactory : DefaultControllerFactory
    {
        IDependencyResolver _resolver;

        public ControllerFactory(IDependencyResolver resolver)
        {
            this._resolver = resolver;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404,
                   String.Format("The controller for path '{0}' could not be found.",
                      requestContext.HttpContext.Request.Path));
            }

            return (IController)_resolver.GetService(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
        }
    }
}