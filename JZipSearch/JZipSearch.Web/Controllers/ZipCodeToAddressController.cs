﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JZipSearch.Web.Controllers
{
    [Route("api/[controller]")]
    public class ZipCodeToAddressController : Controller
    {
        [HttpGet]
        public object Get()
        {
            var query = HttpContext.Request.Query["q"];
            var addressList = JZipSearch.Core.JZipSearchClient.ZipToAddress(query).Result;
            return addressList;
        }
    }
}
