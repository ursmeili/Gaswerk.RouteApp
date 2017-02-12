
using System;

namespace Gaswerk.RouteApp.Models
{
    public class Kunde
    {
        public Kunde(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            Id = id;
        }

        public string Name { get; set; }
        public int Id { get; set; }
    }
}