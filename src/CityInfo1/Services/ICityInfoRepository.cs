using CityInfo1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1.Services
{
   public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointOfInterest);

       IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
       PointOfInterest GetPointsOfInterestForCity(int cityId, int pointOfInterestId);

    }
}
