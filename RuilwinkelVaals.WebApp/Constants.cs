using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp
{
    public static class Constants
    {
        public static ImmutableArray<string> Roles => ImmutableArray.Create
        (
            "Admin",
            "Medewerker",
            "Klant",
            "Bedrijf"
        );

        public static ImmutableArray<string> Conditions => ImmutableArray.Create
        (
            "Zeer goed",
            "Goed",
            "Neutraal",
            "Slecht",
            "Zeer slecht"
        );

        public static ImmutableArray<string> Statuses => ImmutableArray.Create
        (
            "Uitgeleend",
            "Reparatie",
            "Voorradig",
            "Kapot"
        );

        public static ImmutableArray<string> Categories => ImmutableArray.Create
        (
            "Kleding",
            "Boeken",
            "Meubilair",
            "Huishoudelijk",
            "Spellen",
            "Decoratie",
            "Electronica",
            "Antiek",
            "Kunst",
            "Huisdieren"
        );
    }
}
