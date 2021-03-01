using System;
using System.Collections.Generic;
using System.Linq;
using Taxes.Core.Tables;
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

        public decimal? GetTaxes(string municipality, DateTime date)
        {
            var taxes = context.Taxes
                .Join(
                    context.Municipalities,
                    tax => tax.MunicipalityId,
                    munic => munic.Id,
                    (tax, munic) => new { 
                        MunicipalityName = munic.Name,
                        TaxValue = tax.Value,
                        StartDate = tax.StartDate,
                        EndDate = tax.EndDate
                    }
                )
                .Where(t => t.MunicipalityName == municipality && t.StartDate <= date && ((t.EndDate.HasValue && date <= t.EndDate.Value) || (!t.EndDate.HasValue && date == t.StartDate)))
                .ToArray();

            if (taxes.Any(t => !t.EndDate.HasValue))
                return taxes.Where(t => !t.EndDate.HasValue).Max(t => t.TaxValue);
            else if (taxes.Any())
                return taxes.Max(t => t.TaxValue);
            else
                return null;
        }
        public string AddTax(Tax tax)
        {
            var errors = tax.Validate();

            if(!errors.Any())
            {
                MunicipalityTable municipalityTable = context.Municipalities.FirstOrDefault(m => m.Name == tax.Municipality);
                if(municipalityTable == null)
                {
                    municipalityTable = new MunicipalityTable() { Name = tax.Municipality };
                    context.Municipalities.Add(municipalityTable);
                    context.SaveChanges();
                }

                context.Taxes.Add(new TaxTable()
                {
                    MunicipalityId = municipalityTable.Id,
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
