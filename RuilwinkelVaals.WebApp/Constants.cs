using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp
{
    public static class Constants
    {
        public enum Roles
        { 
            Admin,
            Medewerker,
            Klant,
            Bedrijf
        };

        public enum Conditions
        {
            Zeer_Goed,
            Goed,
            Neutraal,
            Slecht,
            Zeer_Slecht
        };

        public enum Statuses
        { 
            Uitgeleend,
            Reparatie,
            Voorradig,
            Kapot
        };

        public enum Categories
        { 
            Kleding,
            Boeken,
            Meubilair,
            Huishoudelijk,
            Spellen,
            Decoratie,
            Electronica,
            Antiek,
            Kunst,
            Huisdieren
        };

        public enum ImageModels
        {
            Products,
            Users
        }
    }
}
