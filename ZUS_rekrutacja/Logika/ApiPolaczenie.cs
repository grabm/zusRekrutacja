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

            webZadanie.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

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

            string document = "<s11:Envelope xmlns:s11='http://schemas.xmlsoap.org/soap/envelope/'>\n" +
                                "   <s11:Body>\n" +
                                "       <ns1:pobierzOswiadczenie xmlns:ns1='http://zus.pl/b2b/zus/channel/gabinetowe' />\n" +
                                "   </s11:Body>\n" +
                                "</s11:Envelope>";

            string document1 = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:gab=\"http://zus.gov.pl/b2b/zus/channel/gabinetowe\">\n" +
                                "   <soapenv:Header/>\n" +
                                "   <soapenv:Body>\n" +
                                "       <gab:pobierzOswiadczenie/>\n" +
                                "   </soapenv:Body>\n" +
                                "</soapenv:Envelope>";

            XmlDocument soapEnvelopeDocument = new XmlDocument();

            soapEnvelopeDocument.LoadXml(document1);

            return soapEnvelopeDocument;
        }
    }
}
