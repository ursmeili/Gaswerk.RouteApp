// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | RouteRepository.cs

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

using Gaswerk.RouteApp.Interfaces.Repositories;
using Gaswerk.RouteApp.Models;

namespace Gaswerk.RouteApp.Logic.Repositories
{

    public class RouteRepository : IRouteRepository
    {
        private readonly Route[] _AllRoutes = new[]
        {
            new Route
            {
                Id = 1,
                Name = "Route1",
                RoutenBauerBewertung = new Bewertung
                {
                    Schwierigkeit = new Schwierigkeit
                    {
                        Grad = 6,
                        SubGrad = SubGradEnum.A
                    }
                }
            },
            new Route
            {
                Id = 2,
                Name = "Route2",
                RoutenBauerBewertung = new Bewertung
                {
                    Schwierigkeit = new Schwierigkeit
                    {
                        Grad = 5,
                        SubGrad = SubGradEnum.C,
                    }
                }
            },
             new Route
            {
                Id = 3,
                Name = "Route3",
                RoutenBauerBewertung = new Bewertung
                {
                    Schwierigkeit = new Schwierigkeit
                    {
                        Grad = 6,
                        SubGrad = SubGradEnum.C,
                        Plus = true
                    }
                },
                KundenBewertungen=new List<Bewertung>
                {
                    new Bewertung
                    {
                        Kommentar="Schön!",
                        Schönheit=4,
                        Kunde=new Kunde(55){Name="Eichmann"},
                        Schwierigkeit = new Schwierigkeit
                    {
                        Grad = 6,
                        SubGrad = SubGradEnum.C,
                    }
                    },

                    new Bewertung
                    {
                        Kommentar="bla!",
                        Schönheit=2,
                        Schwierigkeit = new Schwierigkeit
                    {
                        Grad = 5,
                        SubGrad = SubGradEnum.B,
                    }
                    }
                }


            }
        };

        public IEnumerable<Route> GetAll()
        {
            return _AllRoutes;
        }

        /// <inheritdoc />
        public Route Get(int idRoute)
        {
            return _AllRoutes.First(i => i.Id == idRoute);
        }

        /// <inheritdoc />
        public void AddOrChangeBewertung(Route route, Bewertung bewertung)
        {
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (bewertung == null) throw new ArgumentNullException(nameof(bewertung));

            if (((bewertung.Kunde?.Id) ?? 0)==0) throw new InvalidOperationException("Kein Kunde zugeordnet.");

            var existingBewertung = route.KundenBewertungen?.FirstOrDefault(b => bewertung.Kunde != null && b.Kunde?.Id == bewertung.Kunde?.Id);
            if (existingBewertung != null)
            {
                existingBewertung.Kommentar = bewertung.Kommentar;
                existingBewertung.Schwierigkeit = bewertung.Schwierigkeit;
                existingBewertung.Schönheit = bewertung.Schönheit;
            }
            else
            {
                if (route.KundenBewertungen == null)
                    route.KundenBewertungen = new List<Bewertung>();
                route.KundenBewertungen.Add(bewertung);
            }
        }

        /// <inheritdoc />
        public void DeleteBewertung(int routeId, int kundeId)
        {
            if (kundeId <= 0) throw new ArgumentOutOfRangeException(nameof(kundeId));
            if (routeId <= 0) throw new ArgumentOutOfRangeException(nameof(routeId));

            var route = this.Get(routeId);
            var bewertung = route.KundenBewertungen?.FirstOrDefault(b => b.Kunde?.Id == kundeId);
            if (bewertung != null)
            {
                route.KundenBewertungen?.Remove(bewertung);
            }
        }
    }

}