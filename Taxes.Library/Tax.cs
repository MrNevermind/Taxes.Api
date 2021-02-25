using System;

namespace Taxes.Library
{
    public class Tax
    {
        public string Municipality { get; set; }
        public decimal Value { get; set; }
        public DateTime TaxStart { get; set; }
        public DateTime? TaxEnd { get; set; }
    }
}
