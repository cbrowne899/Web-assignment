using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
//using FMS.Data.Validators;

namespace FMS.Data.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; } //forgein key
        
        //setting Vehicle Properties
        [Required]
        public string Reg {get; set;}
        [Required]
        public string Make {get; set;}
        [Required]
        public string Model {get; set;}
        [Required]
        [Range(1900,2023)]
        public int Year {get; set;}
        [Required]
        public string FuelType {get; set;}
        [Required]
        public string BodyType {get; set;}
        [Required]
        public string TransmissionType{get; set;}
        [Required]
        [Range(0,10)]
        public int Doors {get; set;}
        public DateTime MotDue {get; set;} = DateTime.Now;
       
        //public string PhotoUrl { get; set; }  
       
        //EF Relationship - A vehicle can have many Mots
        public IList<Mot> Mots {get; set;} = new List<Mot>();
        
    }

}
