using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FMS.Data.Models

{
     public enum MotRange {PASS, FAIL, ALL} //enum class 
    public class Mot
    {
        
        public int Id { get; set; }
        //motID

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
        public int VehicleId {get; set;} //foreign key

        //navigation property
        [JsonIgnore]
        public Vehicle Vehicle {get; set;}

        




    }
}
