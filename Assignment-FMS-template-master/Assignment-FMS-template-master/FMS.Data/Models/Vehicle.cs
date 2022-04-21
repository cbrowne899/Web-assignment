using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        
        // suitable vehicle properties/relationships
        public string Reg {get; set;}
        public string Make {get; set;}
        public string Model {get; set;}
        public int Year {get; set;}
        public string FuelType {get; set;}
        public string BodyType {get; set;}
        public string TransmissionType{get; set;}
        public int Doors {get; set;}
        public DateTime MotDue {get; set;} = DateTime.Now;
       // public string carUrl {get; set;}

        public IList<Mot> Mots {get; set;} = new List<Mot>();
        
    }

}
