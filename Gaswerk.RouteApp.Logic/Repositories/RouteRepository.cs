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
        public void AddBewertung(Route route, Bewertung bewertung)
        {
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (bewertung == null) throw new ArgumentNullException(nameof(bewertung));

            if (((bewertung.Kunde?.Id) ?? 0)==0) throw new InvalidOperationException("Kein Kunde zugeordnet.");


            if (route.KundenBewertungen!=null && route.KundenBewertungen.Any(b => b.Kunde?.Id == bewertung.Kunde?.Id))
            {
                throw new InvalidOperationException("Kunde hat die Route bereits bewertet.");
            }
            if(route.KundenBewertungen==null)
                route.KundenBewertungen=new List<Bewertung>();
            route.KundenBewertungen.Add(bewertung);
        }
    }

}