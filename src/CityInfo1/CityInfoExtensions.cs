using CityInfo1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// vlada1
namespace CityInfo1
{
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }
            // seed data
            var cities = new List<City>()
            {
                new City()
                {

                    Name="New York City",
                    Description="The one with that big park",
                    PointsOfInterest=new List<PointOfInterest>()
                    {
                        new PointOfInterest() {
                            Name="Central Park",
                            Description = "The most visited..."
                        },
                        new PointOfInterest() {
                            Name="Empire State building",
                            Description = "The highest building in NY..." },
                    }
                },
                new City()
                {
                    Name="Antwerpen",
                    Description="The one with cathedral that was never finished",
                    PointsOfInterest=new List<PointOfInterest>()
                    {
                        new PointOfInterest() {
                            Name="Katedrala",
                            Description = "Gotski stil..."
                        },
                        new PointOfInterest() {
                            Name="Glavna stanica ",
                            Description = "Barokni stil..." },
                        }
                },
                new City()
                {
                    Name="Paris",
                    Description="The one with  that big tower",
                    PointsOfInterest=new List<PointOfInterest>()
                    {
                        new PointOfInterest() {
                            Name="Ajfelov toranj",
                            Description = "visok 300 m..."
                        },
                        new PointOfInterest() {
                            Name="Luvr ",
                            Description = "Mona Liza..."
                        }
                    }
                }

            };
            context.Cities.AddRange(cities);
            context.SaveChanges();

        }

    }
}
