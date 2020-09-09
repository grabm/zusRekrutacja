using System;
using ZUS_rekrutacja.Logika;
using ZUS_rekrutacja.Modele;

namespace ZUS_rekrutacja
{
    class Program
    {
        static void Main(string[] args)
        {

            var apiPolaczenie = new ApiPolaczenie();

            apiPolaczenie.CallWebService();

            var daneWejsciowe = new DaneWejsciowe()
            {
                DataWystawienia = new DateTime(2014, 11, 1),
                NiezdolnoscDoPracyOd = new DateTime(2014, 10, 10),
                NiezdolnoscDoPracyDo = new DateTime(2014, 11, 10),
                PobytWSzpitaluOd = new DateTime(2014, 10, 20),
                PobytWSzpitaluDo = new DateTime(2014, 11, 05)
            };

            var walidacjeZla = new Walidacje(daneWejsciowe);

            var walidacjaZla = walidacjeZla.WalidacjaDanychWejsciowych();

            if (walidacjaZla)
            {
                var oswiadczenia = new Oswiadczenia(daneWejsciowe);

                var zlaWTrakciePobytu = oswiadczenia.PobierzOswiadczenie(0);

                var zlaPrzedPobytem = oswiadczenia.PobierzOswiadczenie(1);
            }

            Console.ReadLine();
        }
    }
}