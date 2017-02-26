// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | SchwierigkeitEditController.cs

using System;
using System.Web.Mvc;

using Gaswerk.RouteApp.Code;
using Gaswerk.RouteApp.Models;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Controllers
{
    [Authorize]
    public class SchwierigkeitEditController : Controller
    {
        public PartialViewResult Render([NotNull] EditingSchwierigkeitModel m)
        {
            if (m == null) throw new ArgumentNullException(nameof(m));

            EditingSchwierigkeitModel = m;
         
            return PartialView("Render", m);
        }

    
        public PartialViewResult Previous()
        {
            var m = EditingSchwierigkeitModel;

            m.Current = m.Current.Previous() ?? throw new InvalidOperationException();
            return Render(m);
        }

        public PartialViewResult Next()
        {
            var m = EditingSchwierigkeitModel;

            m.Current = m.Current.Next() ?? throw new InvalidOperationException();
            return Render(m);
        }



        private EditingSchwierigkeitModel EditingSchwierigkeitModel
        {
            get
            {
                var m = this.SessionData().EditingSchwierigkeit ?? throw new InvalidOperationException();
                return m;
            }
            set { this.SessionData().EditingSchwierigkeit = value; }
        }

      

       
    }

}