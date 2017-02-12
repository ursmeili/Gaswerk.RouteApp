// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | KundeRepository.cs

using System;

using Gaswerk.RouteApp.Interfaces.Repositories;
using Gaswerk.RouteApp.Models;

namespace Gaswerk.RouteApp.Logic.Repositories
{

    public class KundeRepository : IKundeRepository
    {
        /// <inheritdoc />
        public Kunde Get(Login l)
        {
            if (l == null) throw new ArgumentNullException(nameof(l));

            if(l.Id==null) throw new InvalidOperationException("l.Id==null");

            var k = new Kunde(l.Id.Value);
            switch (l.Id.Value)
            {
                case 1:
                    k.Name = "Meili";
                    break;
                case 2:
                    k.Name = "Urs";
                    break;
                default:
                    k = null;
                    break;
            }

            return k;
        }
    }

}