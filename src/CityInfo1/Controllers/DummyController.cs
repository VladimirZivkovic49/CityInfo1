using CityInfo1.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1.Controllers
{
    public class DummyController:Controller
    {
        private CityInfoContext _ctx;
        public DummyController(CityInfoContext ctx)
        {
            _ctx = ctx;
        }

[HttpGet]
[Route("api/testdatabase")]
public IActionResult TestDatase()
        {
            return Ok();
        }

    }
}
