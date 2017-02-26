// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | Bewertung.cs

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using JetBrains.Annotations;

namespace Gaswerk.RouteApp.Models
{

    public class Bewertung
    {
        [Range(1, 6)]
        public int Schönheit { get; set; }
        public Schwierigkeit Schwierigkeit { get; set; }
        public string Kommentar { get; set; }
        [CanBeNull] public Kunde Kunde { get; set; }
    }

    public class Schwierigkeit : IEquatable<Schwierigkeit>
    {
        public int Grad { get; set; }
        public SubGradEnum SubGrad { get; set; }
        public bool Plus { get; set; }

        public override string ToString() => $"{Grad}{SubGrad}{(Plus ? "+" : "")}";

        public static IEnumerable<Schwierigkeit> GetAllSchwierigkeiten()
        {
            for (int i = 1; i <= 9; i++)
            {
                yield return new Schwierigkeit { Grad = i, SubGrad = SubGradEnum.A };
                if (i >= 6)
                    yield return new Schwierigkeit { Grad = i, SubGrad = SubGradEnum.A, Plus = true };
                yield return new Schwierigkeit { Grad = i, SubGrad = SubGradEnum.B };
                if (i >= 6)
                    yield return new Schwierigkeit { Grad = i, SubGrad = SubGradEnum.B, Plus = true };
                yield return new Schwierigkeit { Grad = i, SubGrad = SubGradEnum.C };
                if (i >= 6)
                    yield return new Schwierigkeit { Grad = i, SubGrad = SubGradEnum.C, Plus = true };
            }


        }

        [NotNull]
        public static Schwierigkeit Parse([NotNull] string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(s));

            var r = new Schwierigkeit();
            r.Grad = int.Parse(s[0].ToString());
            r.SubGrad = (SubGradEnum)Enum.Parse(typeof(SubGradEnum), s[1].ToString());
            if (s.Length > 2 && s[2] == '+')
            {
                r.Plus = true;
            }
            return r;
        }

        /// <inheritdoc />
        public bool Equals(Schwierigkeit other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Grad == other.Grad && SubGrad == other.SubGrad && Plus == other.Plus;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((Schwierigkeit)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Grad;
                hashCode = (hashCode * 397) ^ (int)SubGrad;
                hashCode = (hashCode * 397) ^ Plus.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc />
        public static bool operator ==(Schwierigkeit left, Schwierigkeit right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc />
        public static bool operator !=(Schwierigkeit left, Schwierigkeit right)
        {
            return !Equals(left, right);
        }

        private int GetIndex(Schwierigkeit s)
        {
            var ix = 0;
            foreach (var schwierigkeit in Schwierigkeit.GetSchwierigkeitenArray())
            {
                if (schwierigkeit.Equals(s)) return ix;

                ix++;
            }

            throw new InvalidOperationException();
        }

        private static Schwierigkeit[] GetSchwierigkeitenArray()
        {
            return Schwierigkeit.GetAllSchwierigkeiten().ToArray();
        }

        public bool CanPrevious() => GetIndex(this) > 0;

        public bool CanNext() => GetIndex(this) < Schwierigkeit.GetAllSchwierigkeiten().Count() - 1;

        public Schwierigkeit Previous()
        {
            if(!CanPrevious()) throw new InvalidOperationException();

            return GetSchwierigkeitenArray()[GetIndex(this) - 1];

        }

        public Schwierigkeit Next()
        {
            if (!CanNext()) throw new InvalidOperationException();

            return GetSchwierigkeitenArray()[GetIndex(this) + 1];

        }
    }

    public enum SubGradEnum
    {
        A,
        B,
        C
    }

}