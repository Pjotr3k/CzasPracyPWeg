using LINQtoCSV;
using System;
using System.Collections.Generic;

namespace CzasPracyPWeg.Classes
{
    internal class CsvConverter
    {
        public List<PracaPracownika> Convertion()
        {
            string filePath = @"../../Data/Praca.csv";
            List<PracaPracownika> pr = new List<PracaPracownika>();
            Rows = csvContext.Read<CsvItemDataRow>(filePath, csvDesc);
            int initialRow = 1;
            int headerRow = 2;
            int currentRow = initialRow;

            string[] header = new string[0];
                        
            foreach (CsvItemDataRow row in Rows)
            {
                
                if (currentRow == headerRow)
                {
                    int rLength = row.ToArray().Length;
                    header = new string[rLength];

                    for (int i = 0; i < rLength; i++)
                    {
                        if (row[i].Value != null) header[i] = row[i].Value.ToString();
                    }

                }

                if (currentRow > headerRow)
                {
                    PracaPracownika praca = GetPraca(header, row);
                    pr.Add(praca);                    
                }
                currentRow++;
            }
            return pr;
        }

        private string ValidatePrpZmiana (string nzm, PracaPracownika prp, DateTime dt)
        {
            while (!Zmiany.Exists(x => x.NumerZmiany == nzm))
            {
                Console.WriteLine($"\nDla pracownika o kodzie {prp.KodPracownika} \n" +
                    $"podany format zmiany {nzm} na dzień {dt} jest nieprawidłowy.\n");
                Console.WriteLine("Podaj prawidłowy format zmiany \n");

                foreach (var z in Zmiany)
                {
                    Console.WriteLine($"{z.NumerZmiany}:");
                    if (z.czyPraca) Console.WriteLine($"Dzień pracy od godz. {z.OdGodziny}, przez {z.Czas}");
                    else Console.WriteLine($"Dzień wolny");
                }

                nzm = Console.ReadLine();
            }

            return nzm;
        }

        private PracaPracownika GetPraca (string[] header, CsvItemDataRow row)
        {
            PracaPracownika praca = new PracaPracownika();

            praca.KodPracownika = Convert.ToInt32(row[0].Value);
            Dictionary<DateTime, Zmiana> zmy = new Dictionary<DateTime, Zmiana>();

            for (int i = 1; i < row.ToArray().Length; i++)
            {
                if (header[i] != null)
                {
                    DateTime dt = DateTime.Parse(header[i]);
                    string nzm = ValidatePrpZmiana(row[i].Value, praca, dt);
                    Zmiana zm = Zmiany.Find(x => x.NumerZmiany == nzm);

                    zmy.Add(dt, zm);
                }
            }
            praca.ZmianaDnia = zmy;

            return praca;
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
