// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | IKundeRepository.cs

using Gaswerk.RouteApp.Models;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Interfaces.Repositories
{

    public interface IKundeRepository
    {
        [CanBeNull]
        Kunde Get([NotNull] Login l);
    }

}