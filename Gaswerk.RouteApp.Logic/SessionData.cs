// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | SessionData.cs

using System;
using System.Web;
using System.Web.Mvc;

using Gaswerk.RouteApp.Interfaces.Repositories;
using Gaswerk.RouteApp.Models;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Logic
{
    [Serializable]
    public class SessionData
    {
        [CanBeNull]
        public Kunde Kunde { get; set; }

        public int IdUser => (Kunde?.Id)??throw new InvalidOperationException("Not logged in");

        public static SessionData GetCurrent(Controller c) => SessionData.GetCurrent(c.Session);

        public static SessionData GetCurrent(HttpSessionStateBase c)
        {
            var d = (SessionData)c[nameof(SessionData)];
            if (d == null)
            {
                d = new SessionData();
                c[nameof(SessionData)] = d;
            }
            return d;
        }

        public static void RegisterLogin([NotNull] Login l, Controller c)
        {
            var kunde = Resolver.Get<IKundeRepository>().Get(l);

            GetCurrent(c).Kunde = kunde;
        }
    }

}