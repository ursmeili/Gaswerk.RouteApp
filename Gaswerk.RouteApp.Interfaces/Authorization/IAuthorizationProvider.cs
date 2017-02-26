// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | IAuthorizationProvider.cs

using Gaswerk.RouteApp.Models;

namespace Gaswerk.RouteApp.Interfaces.Authorization
{

    public interface IAuthorizationProvider
    {
        Kunde Authenticate(Login login);
    }

}