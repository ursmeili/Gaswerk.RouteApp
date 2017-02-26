// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | RouteController.cs

using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using Gaswerk.RouteApp.Code;
using Gaswerk.RouteApp.Interfaces.Repositories;
using Gaswerk.RouteApp.Models;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Controllers
{

    public class RouteController : Controller
    {
        [NotNull]
        private readonly IRouteRepository _RouteRepository;

        /// <inheritdoc />
        public RouteController([NotNull] IRouteRepository routeRepository)
        {
            _RouteRepository = routeRepository ?? throw new ArgumentNullException(nameof(routeRepository));
        }

        /// <summary>
        /// Zeigt eine Liste der Bewertungen für Route <paramref name="id"/> an.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BewertungsListe(int id)
        {
            var route = _RouteRepository.Get(id);
            return View("BewertungsListe",Tuple.Create(route, (Bewertung)null));
        }

        /// <summary>
        /// Deletes a bewertung 
        /// </summary>
        /// <param name="id">id der Route</param>
        /// <returns></returns>
        public ActionResult DeleteBewertung(int id)
        {
            _RouteRepository.DeleteBewertung(id, this.CurrentKunde().Id);
            return GoToHome();
        }

        /// <summary>
        /// Löscht oder Ändert eine Bewertung für Route <paramref name="id"/>
        /// </summary>
        /// <param name="id">id der Route</param>
        /// <param name="bewertung"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Bewerten(int id, Bewertung bewertung)
        {
            var route = _RouteRepository.Get(id);
            if (!ModelState.IsValid)
            {
                return View("BewertungsListe", Tuple.Create(route, bewertung));
            }
            else
            {
                bewertung.Kunde = this.CurrentKunde();
                _RouteRepository.AddOrChangeBewertung(route, bewertung);
                return BewertungsListe(id);
            }
        }

        public ActionResult GoToHome()
        {
            return this.RedirectToAction("MainMenu", "Home");
        }

    }

}