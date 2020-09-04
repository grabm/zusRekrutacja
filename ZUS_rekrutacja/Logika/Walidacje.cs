using System;
using ZUS_rekrutacja.Modele;

namespace ZUS_rekrutacja.Logika
{
    public class Walidacje
    {
        private readonly DaneWejsciowe _daneWejsciowe;

        public Walidacje(DaneWejsciowe daneWejsciowe)
        {
            _daneWejsciowe = daneWejsciowe;
        }

        /// <summary>
        /// Metoda sprawdza zgodność dat.
        /// </summary>
        /// <param name="daneWejsciowe">dane wejsciowe</param>
        /// <param name="pobyt"></param>
        /// <returns></returns>
        public bool WalidacjaDanychWejsciowych()
        {
            if (!CzyNiezdolnoscOdMniejszaNiezdolnoscDo() ||
                !CzyPobytWSzpitaluOdMniejszyPobytWSzpitaluDo() ||
                !CzyPobytWSzpitalOdWiekszyNiezdolnoscDoPracyOd() ||
                !CzyPobytWSzpitaluDoMniejszyNiezdolnoscDoPracyDo())
            {
                return false;
            }

            return true;
        }

        private bool CzyNiezdolnoscOdMniejszaNiezdolnoscDo()
        {
            bool wynik = (_daneWejsciowe.NiezdolnoscDoPracyDo - _daneWejsciowe.NiezdolnoscDoPracyOd).TotalDays > 0;

            if (!wynik)
            {
                Console.WriteLine("Data \"niezdolność do pracy od\" musi być wcześniejsza od daty \"niezdolność do pracy do\"");
            }

            return wynik;
        }

        private bool CzyPobytWSzpitaluOdMniejszyPobytWSzpitaluDo()
        {
            bool wynik = (_daneWejsciowe.PobytWSzpitaluDo - _daneWejsciowe.PobytWSzpitaluOd).TotalDays > 0;

            if (!wynik)
            {
                Console.WriteLine("Data \"pobyt w szpitalu od\" musi być wcześniejsza od daty \"pobyt w szpitalu do\"");
            }

            return wynik;
        }

        private bool CzyPobytWSzpitalOdWiekszyNiezdolnoscDoPracyOd()
        {
            bool wynik = (_daneWejsciowe.PobytWSzpitaluOd - _daneWejsciowe.NiezdolnoscDoPracyOd).TotalDays > 0;

            if (!wynik)
            {
                Console.WriteLine("Data \"pobyt w szpitalu od\" musi być późniejsza od daty \"niezdolność do pracy do\"");
            }

            return wynik;
        }

        private bool CzyPobytWSzpitaluDoMniejszyNiezdolnoscDoPracyDo()
        {
            bool wynik = (_daneWejsciowe.NiezdolnoscDoPracyDo - _daneWejsciowe.PobytWSzpitaluDo).TotalDays > 0;

            if (!wynik)
            {
                Console.WriteLine("Data \"pobyt w szpitalu do\" musi być wcześniejsza od daty \"niezdolność do pracy do\"");
            }

            return wynik;
        }
    }
}