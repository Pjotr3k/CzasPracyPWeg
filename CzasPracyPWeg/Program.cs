using CzasPracyPWeg.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using FluentFTP;
using System.Threading;

namespace CzasPracyPWeg
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            CsvConverter conv = new CsvConverter();

            DzienPlanu dpl = new DzienPlanu();
            List<PracaPracownika> Prpy = conv.Convertion();
            List<DzienPlanu> Dply = dpl.GetDziens(Prpy);

            SerializeDzienPlanuToXmlFile(Dply);

            Console.WriteLine("Przekonwertowano do formatu XML.");
            
            UploadXML();
            Console.WriteLine("Plik przesłany. Wciśnij dowolny klawisz, aby zakończyć działanie programu...");
            Console.ReadKey();

        }
        private static void SerializeDzienPlanuToXmlFile(List<DzienPlanu> DplList)
        {
            FileStream fs = new FileStream("../../Data/WynikPracy.xml", FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<DzienPlanu>), new XmlRootAttribute("DniPlanu"));
            xmlSerializer.Serialize(fs, DplList);
            fs.Close();                  
            
        }


        private static void UploadXML()
        {
            FtpClient client = new FtpClient("127.0.0.1", "praca", "praca", 21);
            //FtpClient client = new FtpClient("testpio.x10.mx", "testpiox", "Qwerty123!@#", 21);

            client.Connect();
            client.UploadFile("../../Data/WynikPracy.xml", "WynikPracy.xml", FtpRemoteExists.Overwrite, true);
 
        }
        
    }
}
