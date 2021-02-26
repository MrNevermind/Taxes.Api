using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Taxes.Core;
using Taxes.Library;

namespace Taxes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxesController : ControllerBase
    {
        TaxesManager TaxesManager { get; set; }
        public TaxesController(TaxesContext context)
        {
            TaxesManager = new TaxesManager(context);
        }

        // Returns tax specified municipality and day
        [HttpGet("{municipality}/{date}")]
        public decimal GetTaxes(string municipality, DateTime date)
        {
            return TaxesManager.GetTaxes(municipality, date);
        }

        // Adds tax to database
        [HttpPost]
        public ActionResult AddTaxes([FromBody]Tax tax)
        {
            string resp = TaxesManager.AddTax(tax);

            if(resp == "OK")
                return Ok();
            else
                return StatusCode(400, resp);
        }
    }
}
