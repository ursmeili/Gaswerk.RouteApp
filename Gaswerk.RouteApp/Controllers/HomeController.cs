using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
using Gaswerk.RouteApp.Interfaces.Repositories;
using Gaswerk.RouteApp.Logic;
using Gaswerk.RouteApp.Models;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Controllers
{
    public class HomeController : Controller
    {
        [NotNull]
        private readonly IRouteRepository _RouteRepository;

        [NotNull]
        private readonly IKundeRepository _KundeRepository;

        /// <inheritdoc />
        public HomeController([NotNull] IKundeRepository kundeRepository, [NotNull] IRouteRepository routeRepository)
        {
            _RouteRepository = routeRepository ?? throw new ArgumentNullException(nameof(routeRepository));
            _KundeRepository = kundeRepository ?? throw new ArgumentNullException(nameof(kundeRepository));
        }

        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var kunde = _KundeRepository.Get(model);
                if (kunde == null)
                {
                    ModelState.AddModelError("BadLogin", "Id not known.");
                }
                else
                {
                    SessionData.RegisterLogin(model, this);

                    return RedirectToAction("MainMenu");
                }
            }

            return View(model);
        }

        public ActionResult MainMenu()
        {
            var routen = _RouteRepository.GetAll().ToArray();
            return View(routen);
        }
    }
}