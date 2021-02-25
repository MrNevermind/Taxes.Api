using NUnit.Framework;
using System;
using System.Linq;
using Taxes.Core;

namespace Taxes.Tests
{
    public class Tests
    {
        TaxesManager manager;
        [SetUp]
        public void Setup()
        {
            TaxesContext context = TaxesContext.CreateContext();
            manager = new TaxesManager(context);
        }

        [Test]
        public void GetTaxesTestExists()
        {
            var taxes = manager.GetTaxes("Vilnius", DateTime.Parse("2021-11-21"));
            Assert.IsTrue(taxes.Count() > 0);
        }

        [Test]
        public void GetTaxesTestDoesntExist()
        {
            var taxes = manager.GetTaxes(Guid.NewGuid().ToString(), DateTime.Parse("2021-11-21"));
            Assert.IsTrue(taxes.Count() == 0);
        }

        [Test]
        public void AddTaxForADay()
        {
            int taxesBefore = manager.GetTaxes("FROM_TEST", DateTime.Parse("1991-07-06")).Count();
            manager.AddTax(new Library.Tax() {
                Municipality = "FROM_TEST",
                Value = 0.6m,
                TaxStart = DateTime.Parse("1991-07-06")
            });
            int taxesAfter = manager.GetTaxes("FROM_TEST", DateTime.Parse("1991-07-06")).Count();
            Assert.IsTrue(taxesBefore == taxesAfter - 1);
        }

        [Test]
        public void AddTaxForARange()
        {
            int taxesBefore = manager.GetTaxes("FROM_TEST", DateTime.Parse("1991-07-06")).Count();
            manager.AddTax(new Library.Tax()
            {
                Municipality = "FROM_TEST",
                Value = 0.6m,
                TaxStart = DateTime.Parse("1991-07-01"),
                TaxEnd = DateTime.Parse("1991-07-31")
            });
            int taxesAfter = manager.GetTaxes("FROM_TEST", DateTime.Parse("1991-07-06")).Count();
            Assert.IsTrue(taxesBefore == taxesAfter - 1);
        }
    }
}