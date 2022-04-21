using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models
{
    public class Mot
    {
        public int Id { get; set; }
        
        // suitable mot attributes / relationships

        [Required]
        public string Name {get; set; }
        //name of Person taking MOT

        public DateTime MotDate {get; set;} = DateTime.Now;
        //date of MOT
        [Required]
        public int mileage {get; set;}

        [Required]
        public string Status {get; set;} 
        
        [Required]
        [StringLength(500, MinimumLength =5)]
        public String Report {get; set;}

        //EF Dependant Relationship - Mot belongs to Vehicle
        public int VehicleId {get; set;}

        //[JsonIgnore]
        public Vehicle Vehicle {get; set;}

        




    }
}
