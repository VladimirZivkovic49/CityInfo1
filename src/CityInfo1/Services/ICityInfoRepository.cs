﻿using CityInfo1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1.Services
{
   public interface ICityInfoRepository
    {
        bool CityExists(int cityId);
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointsOfInterest);

       IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
       PointOfInterest GetPointsOfInterestForCity(int cityId, int pointOfInterestId);
        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        void DelitePointOfInterest(PointOfInterest pointOfInterest);
        bool Save();
    }
}
