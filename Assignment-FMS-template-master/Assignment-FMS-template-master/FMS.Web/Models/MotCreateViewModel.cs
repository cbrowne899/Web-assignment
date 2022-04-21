using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using FMS.Data.Models;

namespace FMS.Web.Models
{
    public class MotCreateViewModel
    {
        // selectlist of movies (id, name)       
        public IList<Vehicle> Vehicles { get; set; }
        public IList<Mot> Mots { get; set; }
        // Collecting movie Id
        [Required]
        public int id {get;set;}
        public int VehicleId { get; set; }
        public string Name {get; set;}
        //movie title to display to user
        public string reg { get; set; }
        //movie year to display to user

        public int mileage {get; set;}
        public int Year { get; set; }
        //Comment for user to fill out required with a max and min length
        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Report { get; set; }
        //Reviewer name is required
        [Required]
        [StringLength(35, MinimumLength = 2)]
        public string Status { get; set; }
        //Rating between one and ten must be provided
        
    }

}
