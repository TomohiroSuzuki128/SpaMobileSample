using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JZipSearch.Web.Controllers
{
    [Route("api/[controller]")]
    public class AddressToZipCodeController : Controller
    {
        [HttpGet]
        public object Get()
        {
            var queryPref = HttpContext.Request.Query["pref"];
            var queryAddr = HttpContext.Request.Query["addr"];
            var addressList = JZipSearch.Core.JZipSearchClient.AddressToZip(queryPref, queryAddr).Result;
            return addressList;
        }
    }
}
