using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Gaswerk.RouteApp.Interfaces.Authorization;
using Gaswerk.RouteApp.Interfaces.Repositories;
using Gaswerk.RouteApp.Logic;
using Gaswerk.RouteApp.Models;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationProvider _AuthorizationProvider;

        [NotNull]
        private readonly IRouteRepository _RouteRepository;

        [NotNull]
        private readonly IKundeRepository _KundeRepository;

        /// <inheritdoc />
        public HomeController(
            [NotNull] IKundeRepository kundeRepository, 
            [NotNull] IRouteRepository routeRepository,
            [NotNull] IAuthorizationProvider authorizationProvider)
        {
            _AuthorizationProvider = authorizationProvider ?? throw new ArgumentNullException(nameof(authorizationProvider));
            _RouteRepository = routeRepository ?? throw new ArgumentNullException(nameof(routeRepository));
            _KundeRepository = kundeRepository ?? throw new ArgumentNullException(nameof(kundeRepository));
        }

        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var kunde=_AuthorizationProvider.Authenticate(model);
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

        [Authorize]
        public ActionResult MainMenu()
        {
            var routen = _RouteRepository.GetAll().ToArray();
            return View(routen);
        }
    }
}