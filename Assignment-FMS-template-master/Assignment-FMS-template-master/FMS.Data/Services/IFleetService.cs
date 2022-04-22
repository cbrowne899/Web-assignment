using System;
using System.Collections.Generic;
	
using FMS.Data.Models;
	
namespace FMS.Data.Services
{
    // This interface describes the operations that a FleetService class should implement
    public interface IFleetService
    {
        void Initialise();

         // ------------- Vehicle Management -------------------
        List<Vehicle> GetVehicles(string order =null);
        Vehicle GetVehicle(int id);
        Vehicle GetVehicleByReg(string reg);
        Vehicle AddVehicle(Vehicle v1);
        Vehicle AddVehicle(string reg, string make, string model, int year, string fueltype, string bodytype, string transmissiontype, int doors, DateTime mot);
        //Vehicle AddVehicle(Vehicle v);
        Vehicle UpdateVehicle (Vehicle updated);
        //bool IsDuplicateReg (string reg, int vehicleId);
        bool DeleteVehicle (int id);

        public List<Vehicle> GetVehiclesQuery (Func<Vehicle,bool> q);

        // ------------- MOT Management -------------------
        List<Mot> GetAllMots();
        List <Mot> GetFailMots();
        List <Mot> SearchMots(MotRange range, string query);
        Mot GetMotById(int id);
        Mot GetMotByVehicleId(int id);
        Mot CreateMot(int Vehicleid, string name, int mileage, string status, string report);
    
        public bool DeleteMot(int id);

    
        // ------------- User Management -------------------
        User Authenticate(string email, string password);
        User Register(string name, string email, string password, Role role);
        User GetUserByEmail(string email);
    
    }
    
}