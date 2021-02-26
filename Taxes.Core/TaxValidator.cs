using System;
using System.Collections.Generic;
using System.Text;

namespace Taxes.Core
{
    public interface IValidator
    {
        List<string> Validate(Library.Tax tax);
    }
    public class TaxValidator : IValidator
    {
        public List<string> Validate(Library.Tax tax)
        {
            List<string> errors = new List<string>();
            if (String.IsNullOrWhiteSpace(tax.Municipality))
                errors.Add("Tax.Municipality value is missing");
            if (tax.Value <= 0)
                errors.Add("Tax.Value value must be greater than zero");
            if (tax.TaxStart == DateTime.MinValue)
                errors.Add("Tax.TaxStart value is missing");
            return errors;
        }
    }
}
