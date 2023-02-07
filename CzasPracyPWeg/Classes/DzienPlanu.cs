using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CzasPracyPWeg.Classes
{
    [XmlRoot("DniPlanu")]
    [Serializable]
    public class DzienPlanu
    {
        public void SetDzien (int idPracownik, DateTime dzien, Zmiana zm)
        {
            Pracownik = idPracownik;
            Data = dzien.ToString("yyyy/MM/dd");
            if (zm.czyPraca)
            {
                Definicja = "Pracy";
                OdGodziny = zm.OdGodziny.ToString(@"hh\:mm");
                Czas = zm.Czas.ToString(@"hh\:mm");
            }            
            else Definicja = "Wolny";

        }

        public List<DzienPlanu> GetDziens(List<PracaPracownika> PrpList)
        {
            List<DzienPlanu> dziens = new List<DzienPlanu>();

            foreach(PracaPracownika prc in PrpList)
            {
                foreach(var trm in prc.ZmianaDnia)
                {
                    DzienPlanu dzien = new DzienPlanu();
                    dzien.SetDzien(prc.KodPracownika, trm.Key, trm.Value);
                    dziens.Add(dzien);
                }
            }

            return dziens;
        }

        public int Pracownik { get; set; }
        public string Data { get; set; }
        public string Definicja { get; set; }
        public String OdGodziny { get; set; }
        public String Czas { get; set; }

    }
}
