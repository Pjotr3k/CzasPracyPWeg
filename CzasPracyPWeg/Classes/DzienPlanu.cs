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
            if (zm.czyPraca) Definicja = "Pracy";
            else Definicja = "Wolny";
            if (zm.czyPraca)
            {
                OdGodziny = zm.OdGodziny.ToString();
                Czas = zm.Czas.ToString();
            }
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
        public string Definicja;
        public String OdGodziny { get; set; }
        public String Czas { get; set; }

    }
}
