using CityInfo1.Models;
using CityInfo1.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ICityInfoRepository _cityInforepository;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService,
            ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInforepository = cityInfoRepository;
        }


        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                if (! _cityInforepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id{cityId} wasn´t found when accesing pointsof interest");
                    return NotFound();
                }
                //throw new Exception("Exception sample");
                //   var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                var pointsOfInterestForCity = _cityInforepository.GetPointsOfInterestForCity(cityId);

                /*   if (city == null)
                   {
                       _logger.LogInformation($"City with id{cityId} wasn´t found when accesing pointsof interest");
                       return NotFound();
                   }
                   return Ok(city.PointsOfInterest);*/
                var pointsOfInterestForCityResults = new List<PointOfInterestDto>();
                foreach (var poi in pointsOfInterestForCity)
                {
                    pointsOfInterestForCityResults.Add(new PointOfInterestDto()
                    {
                        Id = poi.Id,
                        Name = poi.Name,
                        Description = poi.Description
                    });

                }
                return Ok(pointsOfInterestForCityResults);

            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}", ex);
                return StatusCode(500, "A problem happened while handeling request");
            }
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            /*    var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    return NotFound();
                }
                var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
                if (pointOfInterest == null)
                {
                    return NotFound();
                }

                return Ok(pointOfInterest);*/

            if (!_cityInforepository.CityExists(cityId))
            {
                return NotFound();
            }
            var pointOfInterest = _cityInforepository.GetPointsOfInterestForCity(cityId, id);

            if (pointOfInterest== null)
            {
                return NotFound();
            }

            var pointOfInterestResult = new PointOfInterestDto()
            {
                Id = pointOfInterest.Id,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description

            };
            return Ok(pointOfInterestResult);
        }
        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId,
                   [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }
               if (pointOfInterest.Description == pointOfInterest.Name)
               {
                   ModelState.AddModelError("Description", "The provided description should be differnt from name");
               }
               if (!ModelState.IsValid)
               {
                   return BadRequest(ModelState);
               }
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var maxPointOfinterestId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointsOfInterest).Max(p => p.Id);
            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfinterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);

        }
        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfinterest(int cityId, int id,
          [FromBody]PointOfInterestForUpdateDto pointOfInterest)

        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be differnt from name");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }
        [HttpPatch("{cityId}/pointsofinterest/{id}")]

        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
             [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)

        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            var pointOfInterestToPatch =
                            new PointOfInterestForUpdateDto()
                            {
                                Name = pointOfInterestFromStore.Name,
                                Description = pointOfInterestFromStore.Description
                            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();

        }
        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DelitePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            city.PointsOfInterest.Remove(pointOfInterestFromStore);

            _mailService.Send("Points of interest delited",
                $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was delited");

            return NoContent();

        }
        }
    }