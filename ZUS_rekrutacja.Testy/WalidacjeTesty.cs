using NUnit.Framework;
using System;
using ZUS_rekrutacja.Logika;
using ZUS_rekrutacja.Modele;

namespace ZUS_rekrutacja.Testy
{
    public class WalidacjeTesty
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPobytWSzpitaluOdMniejszyNiezdolno��DoPracyOd_PowinienZwrocicFalse()
        {
            var daneWejsciowe = new DaneWejsciowe()
            {
                DataWystawienia = new DateTime(2014, 11, 1),
                NiezdolnoscDoPracyOd = new DateTime(2014, 11, 10),
                NiezdolnoscDoPracyDo = new DateTime(2014, 11, 10),
                PobytWSzpitaluOd = new DateTime(2014, 10, 20),
                PobytWSzpitaluDo = new DateTime(2014, 11, 05)
            };

            var walidacje = new Walidacje(daneWejsciowe);
            var rezultat = walidacje.WalidacjaDanychWejsciowych();
            
            Assert.AreEqual(false, rezultat);
        }

        [Test]
        public void TestPobytWSzpitaluOdWiekszyNiezdolno��DoPracyOd_PowinienZwrocicTrue()
        {
            var daneWejsciowe = new DaneWejsciowe()
            {
                DataWystawienia = new DateTime(2014, 11, 1),
                NiezdolnoscDoPracyOd = new DateTime(2014, 10, 10),
                NiezdolnoscDoPracyDo = new DateTime(2014, 11, 10),
                PobytWSzpitaluOd = new DateTime(2014, 10, 20),
                PobytWSzpitaluDo = new DateTime(2014, 11, 05)
            };

            var walidacje = new Walidacje(daneWejsciowe);
            var rezultat = walidacje.WalidacjaDanychWejsciowych();

            Assert.AreEqual(true, rezultat);
        }

        [Test]
        public void TestPobytWSzpitaluDoWiekszyNiezdolno��DoPracyDo_PowinienZwrocicFalse()
        {
            var daneWejsciowe = new DaneWejsciowe()
            {
                DataWystawienia = new DateTime(2014, 11, 1),
                NiezdolnoscDoPracyOd = new DateTime(2014, 10, 10),
                NiezdolnoscDoPracyDo = new DateTime(2014, 11, 10),
                PobytWSzpitaluOd = new DateTime(2014, 10, 20),
                PobytWSzpitaluDo = new DateTime(2014, 11, 20)
            };

            var walidacje = new Walidacje(daneWejsciowe);
            var rezultat = walidacje.WalidacjaDanychWejsciowych();

            Assert.AreEqual(false, rezultat);
        }

        [Test]
        public void TestPobytWSzpitaluDoMniejszyNiezdolno��DoPracyDo_PowinienZwrocicTrue()
        {
            var daneWejsciowe = new DaneWejsciowe()
            {
                DataWystawienia = new DateTime(2014, 11, 1),
                NiezdolnoscDoPracyOd = new DateTime(2014, 10, 10),
                NiezdolnoscDoPracyDo = new DateTime(2014, 11, 10),
                PobytWSzpitaluOd = new DateTime(2014, 10, 20),
                PobytWSzpitaluDo = new DateTime(2014, 11, 05)
            };

            var walidacje = new Walidacje(daneWejsciowe);
            var rezultat = walidacje.WalidacjaDanychWejsciowych();

            Assert.AreEqual(true, rezultat);
        }
    }
}