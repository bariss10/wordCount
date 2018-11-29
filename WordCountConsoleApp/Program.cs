using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace WordCountConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("http://www.gutenberg.org/files/2489/2489.txt");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();


            string duzelt = responseFromServer;
            while (duzelt.Contains("  ") || duzelt.Contains(".") || duzelt.Contains(",") || duzelt.Contains("?")
                || duzelt.Contains("!") || duzelt.Contains("-") || duzelt.Contains("'") || duzelt.Contains("*")
                || duzelt.Contains("=") || duzelt.Contains("(") || duzelt.Contains(")") || duzelt.Contains(":"))
            {

                duzelt = duzelt.Replace(".", " ");
                duzelt = duzelt.Replace(",", " ");
                duzelt = duzelt.Replace("?", " ");
                duzelt = duzelt.Replace("!", " ");
                duzelt = duzelt.Replace("-", "");
                duzelt = duzelt.Replace("'", "");
                duzelt = duzelt.Replace("*", "");
                duzelt = duzelt.Replace("=", "");
                duzelt = duzelt.Replace("(", "");
                duzelt = duzelt.Replace(")", "");
                duzelt = duzelt.Replace(":", " ");
                duzelt = duzelt.Replace("  ", " ");
            }

            string[] parcalar;
            parcalar = duzelt.Split(' ');

            List<string> kelimeListesi = new List<string>();

            for (int i = 0; i < parcalar.Length; i++)
            {
                string kelimekontrol = parcalar[i].ToLower().Trim();
                if (kelimekontrol.Contains("ı"))
                {
                    kelimekontrol = kelimekontrol.Replace("ı", "i");
                }

                if (kelimekontrol.Length >= 3)
                {
                    kelimeListesi.Add(kelimekontrol.ToString());
                }

            }

            using (XmlWriter writer = XmlWriter.Create("D:\\wordlist.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("words");
                foreach (var item in kelimeListesi.GroupBy(x => x)
                                                  .OrderByDescending(x => x.Count()))
                {

                    writer.WriteStartElement("word");
                    writer.WriteAttributeString("text", item.First());
                    writer.WriteAttributeString("count", item.Count().ToString());
                    writer.WriteEndElement();
                }


                writer.WriteEndElement();
                writer.WriteEndDocument();
            }


        }
    
    }
}
