﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JZipSearch.Web
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]

        // GET: /<controller>/
        public IActionResult Index()
        {
            return new ContentResult() {  Content = "test" };
        }
    }
}
