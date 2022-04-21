using System;
using System.Collections.Generic;
	
using FMS.Data.Models;
	
namespace FMS.Data.Services
{
    // This interface describes the operations that a FleetService class should implement
    public interface IFleetService
    {
        void Initialise();
        
        // add suitable method definitions to implement assignment requirements            
        //IList<Vehicle> vehicles {get; set;}

         // ------------- Vehicle Management -------------------
        // List<Vehicle> GetVehicles(string order =null);
        List<Vehicle> GetVehicles();
        Vehicle GetVehicle(int id);
        Vehicle GetVehicleByReg(string reg);
        Vehicle AddVehicle(string reg, string make, string model, int year, string fueltype, string bodytype, string transmissiontype, int doors, DateTime mot);
        //Vehicle AddVehicle(Vehicle v);
        Vehicle UpdateVehicle (Vehicle updated);
        //bool IsDuplicateReg (string reg, int vehicleId);
        bool DeleteVehicle (int id);

        // ------------- MOT Management -------------------
        List<Mot> GetAllMots();
        Mot GetMotById(int id);
        Mot AddMot(int vehicleId, string name, DateTime motdate, int mileage, string status, string report);
        public bool DeleteMot(int id);
       // public IList<Mot> GetAllMots();


        
        // ------------- User Management -------------------
        User Authenticate(string email, string password);
        User Register(string name, string email, string password, Role role);
        User GetUserByEmail(string email);
    
    }
    
}