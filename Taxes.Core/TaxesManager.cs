using System;
using System.Collections.Generic;
using System.Linq;
using Taxes.Library;

namespace Taxes.Core
{
    public class TaxesManager
    {
        TaxesContext context;

        public TaxesManager(TaxesContext context)
        {
            this.context = context;
        }

        public IEnumerable<decimal> GetTaxes(string municipality, DateTime date)
        {
            return context.Taxes
                .Where(t => t.Municipality == municipality && t.StartDate <= date && (!t.EndDate.HasValue || date <= t.EndDate.Value))
                .Select(t => t.Value);
        }
        public string AddTax(Tax tax)
        {
            List<string> errors = new List<string>();
            if (String.IsNullOrWhiteSpace(tax.Municipality))
                errors.Add("Tax.Municipality value is missing");
            if (tax.Value <= 0)
                errors.Add("Tax.Value value must be greater than zero");
            if (tax.TaxStart == DateTime.MinValue)
                errors.Add("Tax.TaxStart value is missing");

            if(!errors.Any())
            {
                context.Taxes.Add(new Tables.TaxTable()
                {
                    Municipality = tax.Municipality,
                    Value = tax.Value,
                    StartDate = tax.TaxStart,
                    EndDate = tax.TaxEnd
                });
                context.SaveChanges();

                return "OK";
            }
            else
            {
                return String.Join(Environment.NewLine, errors);
            }
        }
    }
}
