using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1.Models
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }
        public CitiesDataStore()
        {
            // init dummy data
            Cities = new List<CityDto>()


           {
           new CityDto()

           {
            Id=1,
            Name="New York City",
            Description="The one with that big park",
            PointsOfInterest=new List<PointOfInterestDto>()
            {
                new PointOfInterestDto() {

                    Id=1,
                    Name="Central Park",
                   Description = "The most visited..." },

                new PointOfInterestDto() {
                    Id=2,

                    Name="Empire State building",
                    Description = "The highest building in NY..." },
            }
             },
           new CityDto()

           {
            Id=2,
            Name="Antwerpen",
            Description="The one with cathedral that was never finished"
             },

                new CityDto()
                {
            Id=3,
            Name="Paris",
            Description="The one with  that big tower"
           }

            };
        }

    }
}
