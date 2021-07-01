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
            [Display(Name = "Zeer goed")] 
            ZeerGoed,
            Goed,
            Neutraal,
            Slecht,
            [Display(Name = "Zeer slecht")] 
            ZeerSlecht
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
    }
}
