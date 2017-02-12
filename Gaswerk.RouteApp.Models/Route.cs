// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | Route.cs

using System.Collections;
using System.Collections.Generic;

namespace Gaswerk.RouteApp.Models
{

    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Bewertung RoutenBauerBewertung { get; set; }

        public List<Bewertung> KundenBewertungen { get; set; }
    }

}