// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | WebViewPageExtensions.cs

using System;
using System.Web;
using System.Web.Mvc;

using Gaswerk.RouteApp.Logic;
using Gaswerk.RouteApp.Models;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Code
{

    public static class WebViewPageExtensions
    {
        [NotNull]
        public static SessionData SessionData([NotNull] this WebViewPage page)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            return Logic.SessionData.GetCurrent(page.Context.Session);
        }

        [ContractAnnotation("throwIfNotLoggedIn:true => notnull")]
        [ContractAnnotation("throwIfNotLoggedIn:false => canbenull")]
        public static Kunde CurrentKunde([NotNull] this WebViewPage page, bool throwIfNotLoggedIn = true)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            var kunde = page.SessionData().Kunde;
            if (throwIfNotLoggedIn && kunde == null)
            {
                throw new HttpException(401, "not logged in");
            }

            return kunde;
        }

        [ContractAnnotation("throwIfNotLoggedIn:true => notnull")]
        [ContractAnnotation("throwIfNotLoggedIn:false => canbenull")]
        public static Kunde CurrentKunde([NotNull] this Controller c, bool throwIfNotLoggedIn = true)
        {
            var kunde = Logic.SessionData.GetCurrent(c).Kunde;
            if (throwIfNotLoggedIn && kunde == null)
            {
                throw new HttpException(401, "not logged in");
            }

            return kunde;
        }
    }

}