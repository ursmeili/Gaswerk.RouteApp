// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | IRouteRepository.cs

using System.Collections.Generic;

using Gaswerk.RouteApp.Models;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Interfaces.Repositories
{

    public interface IRouteRepository
    {
        IEnumerable<Route> GetAll();

        [NotNull]
        Route Get(int idRoute);

        void AddBewertung([NotNull] Route route, [NotNull] Bewertung bewertung);
    }

}