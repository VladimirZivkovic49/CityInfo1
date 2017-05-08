using CityInfo1.Models;
using CityInfo1.Services;
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
        private ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }


        [HttpGet()]
        public IActionResult GetCities()
        {
            //   return Ok(CitiesDataStore.Current.Cities);
            /*      {
                      new {id=1,Name="New York City"},
                      new {id=2,Name="Antwerpen"}
                  });  uklonjeno kada je formirana klasa "CitiesDatastre sa svojstvima  traženih podataka kao hard copi */
            var cityEntities = _cityInfoRepository.GetCities();

            var results = new List<CityWithoutPointsOfInterestDto>();

            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterestDto
                {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name
                });
            }

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            //find city
            /*  var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
              if (cityToReturn == null)
              {
                  return NotFound();
              }
              return Ok(cityToReturn);*/
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var cityResult = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };

                foreach (var poi in city.PointsOfInterest)
                {
                    cityResult.PointsOfInterest.Add(
                        new PointOfInterestDto()
                    {
                        Id = poi.Id,
                        Name = poi.Name,
                        Description = poi.Description

                    });
                }

                return Ok(cityResult);
            }

            var cityWithoutPointsOfInterestResult =
                new CityWithoutPointsOfInterestDto()
                {
                    Id = city.Id,
                    Description = city.Description,
                    Name = city.Name

                };
            return Ok(cityWithoutPointsOfInterestResult);
        }
    }
}
