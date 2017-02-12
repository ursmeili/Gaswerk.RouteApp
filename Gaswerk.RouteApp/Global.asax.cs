using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Gaswerk.RouteApp.Models;

namespace Gaswerk.RouteApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders[typeof(Schwierigkeit)] = new SchwierigkeitModelBinder();
        }
    }

    public class SchwierigkeitModelBinder : IModelBinder
    {
        /// <inheritdoc />
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            try
            {
                Schwierigkeit s=Schwierigkeit.Parse(value.AttemptedValue);
                return s;
            }
            catch (Exception)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "invalid Schwierigkeit");
                return null;
            }
          
        }
    }

}
