using System;

namespace CzasPracyPWeg.Classes
{
    public class Zmiana
    {
        public Zmiana(string numerZmiany, TimeSpan odGodziny, TimeSpan czas, bool czyPraca = true)
        {
            NumerZmiany = numerZmiany;
            OdGodziny = odGodziny;
            Czas = czas;
            this.czyPraca = czyPraca;
        }

        //Numer zmiany nie będzie służył do obliczeń, lepiej zaoszczędzić sobie konwersji przy pobieraniu danych
        public string NumerZmiany { get; set; }
        public TimeSpan OdGodziny { get; set; }
        public TimeSpan Czas { get; set; }
        public bool czyPraca { get; set; }
    }
}
