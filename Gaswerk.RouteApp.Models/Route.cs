// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | Route.cs

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Models
{

    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Bewertung RoutenBauerBewertung { get; set; }

        public List<Bewertung> KundenBewertungen { get; set; }

        public bool BereitsBewertet([NotNull] Kunde kunde)
        {
            if (kunde == null) throw new ArgumentNullException(nameof(kunde));

            return KundenBewertungen != null && KundenBewertungen.Any(k => k.Kunde?.Id == kunde.Id);
        }
    }

}