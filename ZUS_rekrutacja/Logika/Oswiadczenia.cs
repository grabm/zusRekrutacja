using System.Collections.Generic;
using ZUS_rekrutacja.Modele;

namespace ZUS_rekrutacja.Logika
{
    public class Oswiadczenia
    {
        private readonly DaneWejsciowe _daneWejsciowe;

        public Oswiadczenia(DaneWejsciowe daneWejsciowe)
        {
            _daneWejsciowe = daneWejsciowe;
        }

        /// <summary>
        /// Metoda pobiera oswiadczenie. Pobyt: 0 - w trakcie pobytu, 1 - przed pobytem
        /// </summary>
        /// <param name="pobyt"></param>
        /// <returns></returns>        
        public List<DaneWyjsciowe> PobierzOswiadczenie(int pobyt)
        {
            var daneWyjsciowe = new List<DaneWyjsciowe>();

            var niezdolnoscDoPracyDo = pobyt == 0 ? Narzedzia.OdejmijMaksymalnaIloscDni(4, _daneWejsciowe.PobytWSzpitaluOd, _daneWejsciowe.NiezdolnoscDoPracyOd)
                                        : Narzedzia.OdejmijMaksymalnaIloscDni(4, _daneWejsciowe.DataWystawienia, _daneWejsciowe.NiezdolnoscDoPracyOd);

            var zaswiadczenieWsteczne = new DaneWyjsciowe()
            {
                NiezdolnoscDoPracyOd = _daneWejsciowe.NiezdolnoscDoPracyOd,
                NiezdolnoscDoPracyDo = niezdolnoscDoPracyDo,
                ZaswiadczenieWsteczne = true
            };

            var niezdolnoscDoPracyOd = pobyt == 0 ? Narzedzia.OdejmijMaksymalnaIloscDni(3, _daneWejsciowe.PobytWSzpitaluOd, _daneWejsciowe.NiezdolnoscDoPracyOd)
                                        : Narzedzia.OdejmijMaksymalnaIloscDni(3, _daneWejsciowe.DataWystawienia, _daneWejsciowe.NiezdolnoscDoPracyOd);

            var zaswiadczenieBiezace = new DaneWyjsciowe()
            {
                NiezdolnoscDoPracyOd = niezdolnoscDoPracyOd,
                NiezdolnoscDoPracyDo = _daneWejsciowe.NiezdolnoscDoPracyDo,
                PobytWSzpitaluOd = _daneWejsciowe.PobytWSzpitaluOd,
                PobytWSzpitaluDo = _daneWejsciowe.PobytWSzpitaluDo,
                ZaswiadczenieBiezace = true
            };

            daneWyjsciowe.Add(zaswiadczenieWsteczne);
            daneWyjsciowe.Add(zaswiadczenieBiezace);

            return daneWyjsciowe;
        }
    }
}