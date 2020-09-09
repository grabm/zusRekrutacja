using System;
using System.IO;
using System.Net;
using System.Xml;

namespace ZUS_rekrutacja.Logika
{
    public class ApiPolaczenie
    {
        private string _url = "https://193.105.143.152:8001/ws/zus.channel.gabinetoweV2:zla";
        private string _akcja = "http://zus.pl/b2b/zus/channel/gabinetowe";

        public string CallWebService()
        {
            var soapEnvelopeXml = TworzXML();
            var zadanie = UtworzZadanieWeb(_url, _akcja);

            zadanie.Credentials = new NetworkCredential("ezla_ag", "ezla_ag");

            using (var stream = zadanie.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            var asyncRezultat = zadanie.BeginGetResponse(null, null);

            asyncRezultat.AsyncWaitHandle.WaitOne();

            string rezultat = null;

            using (var odpowiedz = zadanie.EndGetResponse(asyncRezultat))
            {
                using (var streamReader = new StreamReader(odpowiedz.GetResponseStream()))
                {
                    rezultat = streamReader.ReadToEnd();
                }

                Console.Write(rezultat);
            }

            return rezultat;
        }


        private HttpWebRequest UtworzZadanieWeb(string url, string action)
        {
            HttpWebRequest webZadanie = (HttpWebRequest)WebRequest.Create(url);
            webZadanie.Headers.Add("SOAPAction", action);
            webZadanie.Accept = "text/xml";
            webZadanie.ContentType = "application/x-www-form-urlencoded";
            webZadanie.Method = "POST";

            return webZadanie;
        }

        private void DodajSoapEnvelopeDoZadaniaWeb(XmlDocument soapEnvelopeXml, HttpWebRequest zadanieWeb)
        {
            using (Stream stream = zadanieWeb.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        private XmlDocument TworzXML()
        {
        //XNamespace soap = "http://www.w3.org/2003/05/soap-envelope";
        //XNamespace ns = "http://CIS/BIR/PUBL/2014/07";
        //XNamespace wsa = "http://www.w3.org/2005/08/addressing";
        //string wsaAction = "http://CIS/BIR/PUBL/2014/07/IUslugaBIRzewnPubl/Zaloguj";
        //string wsaTo = "https://wyszukiwarkaregontest.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc";
        //string klucz = "abcde12345abcde12345";


        //var xDocument = new XDocument(
        //    new XElement(soap + "Envelope", new XAttribute(XNamespace.Xmlns + "soap", soap), new XAttribute(XNamespace.Xmlns + "ns", ns),
        //        new XElement(soap + "Header", new XAttribute(XNamespace.Xmlns + "wsa", wsa),
        //            new XElement(wsa + "Action", wsaAction),
        //            new XElement(wsa + "To", wsaTo)),
        //        new XElement(soap + "Body",
        //        new XElement(ns + "Zaloguj",
        //            new XElement(ns + "pKluczUzytkownika", klucz))))
        //    );

        //string xmlDocument = xDocument.ToString();

        //var xDocument1 = new XDocument(
        //    new XElement(soap + "Envelope", new XAttribute(XNamespace.Xmlns + "soap", soap), new XAttribute(XNamespace.Xmlns + "ns", ns),
        //        new XElement(soap + "Header", new XAttribute(XNamespace.Xmlns + "wsa", wsa),
        //            new XElement(wsa + "Action", wsaAction),
        //            new XElement(wsa + "To", wsaTo)),
        //        new XElement(soap + "Body",
        //        new XElement(ns + "Zaloguj",
        //            new XElement(ns + "pKluczUzytkownika", klucz))))
        //    );

        //string xmlDocument1 = xDocument.ToString();
        
            string document = "<s11:Envelope xmlns:s11='http://schemas.xmlsoap.org/soap/envelope/'>\n" +
                                "   <s11:Body>\n" +
                                "       <ns1:pobierzOswiadczenie xmlns:ns1='http://zus.pl/b2b/zus/channel/gabinetowe' />\n" +
                                "   </s11:Body>\n" +
                                "</s11:Envelope>";

            XmlDocument soapEnvelopeDocument = new XmlDocument();

            soapEnvelopeDocument.LoadXml(document);

            return soapEnvelopeDocument;
        }
    }
}
