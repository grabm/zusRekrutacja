using System;

namespace ZUS_rekrutacja.Modele
{
    public class DaneWyjsciowe
    {
        public DateTime NiezdolnoscDoPracyOd { get; set; }
        public DateTime NiezdolnoscDoPracyDo { get; set; }
        public DateTime? PobytWSzpitaluOd { get; set; }
        public DateTime? PobytWSzpitaluDo { get; set; }
        public bool ZaswiadczenieWsteczne { get; set; }
        public bool ZaswiadczenieBiezace { get; set; }
    }
}