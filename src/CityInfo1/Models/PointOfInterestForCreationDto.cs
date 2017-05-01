using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "Mora ime biti upisano")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Description { get; set; }
    }
}
