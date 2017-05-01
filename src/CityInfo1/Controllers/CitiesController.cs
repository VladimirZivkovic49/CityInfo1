using CityInfo1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
            /*      {
                      new {id=1,Name="New York City"},
                      new {id=2,Name="Antwerpen"}
                  });  uklonjeno kada je formirana klasa "CitiesDatastre sa svojstvima  traženih podataka kao hard copi */

        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            //find city
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);


        }
    }
}
