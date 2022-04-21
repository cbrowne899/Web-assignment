using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using FMS.Data.Models;
using System.Collections.Generic;

namespace FMS.Web.Models
{
    public class MotViewModel
    {
        // selectlist of movies (id, title)       
       
        public IList<Mot> Mots { get; set; } = new List<Mot>();
        // Collecting MovieId and Review in Form
        public int id {get; set;}
        public int VehicleId { get; set; }
        [Required]
        [StringLength(35, MinimumLength = 2)]
        public string Name { get; set; }

        public DateTime MotDate {get; set;}
        [Required]
        [Range(1, 10,
        ErrorMessage = "Value must be between 1 and 10.")]
        public string Status {get; set;}
        public int mileage { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Report { get; set; }
    }
}
