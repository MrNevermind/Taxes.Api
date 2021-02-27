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
            Settings.ConnectionString = "Server=tcp:edvinas.database.windows.net,1433;Initial Catalog=Taxes;Persist Security Info=False;User ID=edvinas;Password=N+,j7\"4rdTTY3~~a; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            TaxesContext context = TaxesContext.CreateContext();
            IValidator validator = new TaxValidator();
            manager = new TaxesManager(context, validator);
        }

        [Test]
        public void TestTaskTaxRowOne()
        {
            decimal tax = manager.GetTaxes("Copenhagen", DateTime.Parse("2020-01-01"));
            Assert.IsTrue(tax == 0.1m);
        }
        [Test]
        public void TestTaskTaxRowTwo()
        {
            decimal tax = manager.GetTaxes("Copenhagen", DateTime.Parse("2020-05-02"));
            Assert.IsTrue(tax == 0.4m);
        }
        [Test]
        public void TestTaskTaxRowThree()
        {
            decimal tax = manager.GetTaxes("Copenhagen", DateTime.Parse("2020-07-10"));
            Assert.IsTrue(tax == 0.2m);
        }
        [Test]
        public void TestTaskTaxRowFour()
        {
            decimal tax = manager.GetTaxes("Copenhagen", DateTime.Parse("2020-03-16"));
            Assert.IsTrue(tax == 0.2m);
        }

        [Test]
        public void TestTaxCreateAndGet()
        {
            string municName = Guid.NewGuid().ToString();
            manager.AddTax(new Library.Tax() { Municipality = municName, TaxStart = DateTime.Parse("1991-07-01"), TaxEnd = DateTime.Parse("1991-07-31"), Value = 0.6m});
            decimal tax = manager.GetTaxes(municName, DateTime.Parse("1991-07-06"));
            Assert.IsTrue(tax == 0.6m);
        }

    }
}