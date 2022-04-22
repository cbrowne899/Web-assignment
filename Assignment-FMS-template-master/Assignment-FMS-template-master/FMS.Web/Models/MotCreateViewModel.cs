using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using FMS.Data.Models;

namespace FMS.Web.Models
{
    public class MotCreateViewModel
    {
        //selectlist of Vehicles
        public SelectList Vehicles {get; set;}
        [Required(ErrorMessage= "Please select a vehicle")]
        [Display(Name= "Select Vehicle")]
        public int VehicleId { get; set; }

        [Required]
        [StringLength(500, MinimumLength=5)]
        public string Report { get; set; }

        public string Name {get; set;}

        public int mileage {get; set;}
        
        // [Required]
        [StringLength(35, MinimumLength = 2)]
        public string Status { get; set; }
       
        //collecting mot properties in form
    }
}


