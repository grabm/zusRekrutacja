using System;

namespace ZUS_rekrutacja.Logika
{
    public class Narzedzia
    {
        public static DateTime OdejmijMaksymalnaIloscDni(int iloscDniDoOdjecia, DateTime dataDoOdjecia, DateTime dataDoPorownania)
        {
            var iloscDni = iloscDniDoOdjecia * -1;

            for (int i = iloscDni; i < 0; i++)
            {
                if (dataDoOdjecia.AddDays(i) >= dataDoPorownania)
                {
                    var wynik = dataDoOdjecia.AddDays(i);

                    return wynik;
                }
            }

            return DateTime.MinValue;
        }
    }
}