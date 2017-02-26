// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | AuthorizationProvider.cs

using System;
using System.Web.Security;

using Gaswerk.RouteApp.Interfaces.Authorization;
using Gaswerk.RouteApp.Logic.Repositories;
using Gaswerk.RouteApp.Models;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Logic.Authorization
{

    public class AuthorizationProvider : IAuthorizationProvider
    {
        /// <inheritdoc />
        public Kunde Authenticate([NotNull] Login login)
        {
            if (login == null) throw new ArgumentNullException(nameof(login));

            var kundeRepository = Resolver.Get<KundeRepository>();
            var kunde = kundeRepository.Get(login);
            if (kunde != null)
            {
                FormsAuthentication.SetAuthCookie(kunde.Id.ToString(),false);
            }

            return kunde;
        }
    }

}