using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CzasPracyPWeg.Classes
{
    [Serializable]
    public class PracaPracownika
    {
        public int KodPracownika { get; set; }
        [XmlIgnore]
        public Dictionary<DateTime, Zmiana> ZmianaDnia { get; set; }
    }
}
