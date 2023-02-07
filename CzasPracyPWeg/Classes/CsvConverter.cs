using LINQtoCSV;
using System;
using System.Collections.Generic;

namespace CzasPracyPWeg.Classes
{
    internal class CsvConverter
    {
        public List<PracaPracownika> Convertion()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            List<PracaPracownika> pr = new List<PracaPracownika>();
            Rows = csvContext.Read<CsvItemDataRow>("D:\\Users\\User\\source\\repos\\CzasPracyPWeg\\CzasPracyPWeg\\Data\\Praca.csv", csvDesc);

            int cr = 1; // currentRow
            CsvItemDataRow head;
            string[] header = new string[0];
                        
            foreach (CsvItemDataRow row in Rows)
            {
                
                if (cr == 2)
                {
                    int rLength = row.ToArray().Length;
                    header = new string[rLength];

                    for (int i = 0; i < rLength; i++)
                    {
                        if (row[i].Value != null) header[i] = row[i].Value.ToString();
                    }

                }

                if (cr > 2)
                {
                    PracaPracownika praca = new PracaPracownika();

                    praca.KodPracownika = Convert.ToInt32(row[0].Value);
                    Dictionary<DateTime, Zmiana> zmy = new Dictionary<DateTime, Zmiana>();

                    for (int i = 1; i < row.ToArray().Length; i++)
                    {
                        if (header[i] != null)
                        {
                            DateTime dt = DateTime.Parse(header[i]);
                            string nzm = row[i].Value;
                            //Zmiana zm = Zmiany.Find(x => x.NumerZmiany == row[i].Value)
                            while (!Zmiany.Exists(x => x.NumerZmiany == nzm))
                            {
                                Console.WriteLine();
                                Console.WriteLine($"Dla pracownika o kodzie {praca.KodPracownika} " +
                                    $"podany format zmiany {nzm} na dzień {dt} jest nieprawidłowy.");
                                Console.WriteLine();
                                Console.WriteLine("Podaj prawidłowy format zmiany");
                                Console.WriteLine();
                                foreach(var z in Zmiany)
                                {
                                    Console.WriteLine($"{z.NumerZmiany}:");
                                    if (z.czyPraca) Console.WriteLine($"Dzień pracy od godz. {z.OdGodziny}, przez {z.Czas}");
                                    else Console.WriteLine($"Dzień wolny");                                    
                                }

                                nzm = Console.ReadLine();
                            }

                            Zmiana zm = Zmiany.Find(x => x.NumerZmiany == nzm);

                            

                            zmy.Add(dt, zm);                            
                        }
                        
                    }
                    praca.ZmianaDnia = zmy;
                    pr.Add(praca);                    
                }

                cr++;
            }

            return pr;
        }

        private CsvFileDescription csvDesc = new CsvFileDescription
        {
            SeparatorChar = ';'        
        };

        private CsvContext csvContext = new CsvContext();

        private List<Zmiana> Zmiany = new List<Zmiana>() {
                    new Zmiana("1", new TimeSpan(6, 0, 0), new TimeSpan(8, 0, 0)),
                    new Zmiana("2", new TimeSpan(14, 0, 0), new TimeSpan(8, 0, 0)),
                    new Zmiana("3", new TimeSpan(22, 0, 0), new TimeSpan(8, 0, 0)),
                    new Zmiana("X", new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), false)
            };

        public IEnumerable<CsvItemDataRow> Rows { get; set; }


    }
}
