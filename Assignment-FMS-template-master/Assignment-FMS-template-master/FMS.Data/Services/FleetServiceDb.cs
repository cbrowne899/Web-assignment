using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using FMS.Data.Models;
using FMS.Data.Repository;
using System.Net;
using FMS.Data.Security;

namespace FMS.Data.Services
{
    public class FleetServiceDb : IFleetService
    {
        private readonly DataContext db;

        public FleetServiceDb()
        {
            db = new DataContext();
        }

        public void Initialise()
        {
            db.Initialise(); // recreate database
        }

        // ==================== Fleet Management ==================
       
        // implement IFleetService methods here

        //public IList<Vehicle> vehicle {get; set;}
        
        // public List<Vehicle> GetVehicles(string order=null)
        // {
        //     List<Vehicle> vehicle = new List<Vehicle>();
        //    switch (order)
        //     {
        //         case "regOrder":
        //          vehicle = db.Vehicles.OrderBy( v=> v.Reg).ToList();
        //          break;
        //          case "makeOrder":
        //          vehicle = db.Vehicles.OrderBy(v=> v.Make).ToList();
        //          break;
        //          case "yearOrder":
        //          vehicle = db.Vehicles.OrderBy(v=> v.Year).ToList();
        //          break;
                
        //         default:
        //          vehicle = db.Vehicles.ToList();
        //          break;
        //     }
        //          return vehicle;

        //    }

        public List<Vehicle> GetVehicles()
        {
            
             var vehicle= db.Vehicles.ToList();

             return vehicle;

           }
        

    
        //retrieve vehicle by registration
        public Vehicle GetVehicle(int id)
        {
            return db.Vehicles
                            .Include(v=> v.Mots)
                            .FirstOrDefault(v => v.VehicleId == id);
        }

        public Vehicle GetVehicleByReg(string reg)
        {
            return db.Vehicles.FirstOrDefault(v => v.Reg == reg);
        }

        public Vehicle AddVehicle(string reg, string make, string model, int year, string fueltype, string bodytype, string transmissiontype, int doors, DateTime mot)
        {
            var exists = GetVehicleByReg(reg);
            if (exists !=null)
            {
                return null;
            }

            var vehicle = new Vehicle
            {
                Reg = reg,
                Make=make,
                Model = model,
                Year= year,
                FuelType= fueltype,
                BodyType= bodytype,
                TransmissionType= transmissiontype, 
                Doors= doors, 
                MotDue = mot
            };
            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            return vehicle;

        }
        public Vehicle AddVehicle(Vehicle v1)
        
        {
            var exists = GetVehicleByReg(v1.Reg);

            if (exists !=null)
            {
            return null;
            }

            var vehicle = new Vehicle 
            {
                Reg=v1.Reg,
                Make=v1.Make,
                Model=v1.Model,
                Year=v1.Year,
                FuelType=v1.FuelType,
                BodyType = v1.BodyType,
                TransmissionType=v1.TransmissionType,
                Doors=v1.Doors,
                MotDue =v1.MotDue
                
            };
            db.Vehicles.Add(vehicle); 
            //add vehicle to list 
            db.SaveChanges(); // save changes to database
            return vehicle;
        }

        public Vehicle UpdateVehicle (Vehicle updated)
        {
            var vehicle = GetVehicle(updated.VehicleId);

            if (vehicle==null)
            {
                return null;
            }
            vehicle.Reg= updated.Reg;
            vehicle.Make=updated.Make;
            vehicle.Model=updated.Model;
            vehicle.Year=updated.Year;
            vehicle.FuelType=updated.FuelType;
            vehicle.BodyType=updated.BodyType;
            vehicle.Doors=updated.Doors;
            vehicle.MotDue=updated.MotDue;

            db.SaveChanges();
            return vehicle;
    
        }

        public bool DeleteVehicle(int id)
        {
            var v = GetVehicle(id);
            if (v ==null)
            {
                return false;
            }
            db.Vehicles.Remove(v);
            db.SaveChanges();
            return true;   
        }

        public List<Vehicle> GetVehiclesQuery (Func<Vehicle,bool> q)
        {
            return db.Vehicles.Where(q).ToList();
        }


    
        // ==================== MOT Management ==================

        public Mot GetMotById(int id)
        {
            return db.Vehicles
                            .SelectMany(v=>v.Mots)
                            .FirstOrDefault(m=> m.Id==id);

        }
        
        public Mot GetMotByVehicleId(int id)
        {
            return db.Vehicles
                            .SelectMany(v=>v.Mots)
                            .FirstOrDefault(m=> m.VehicleId==id);

        }


        public Mot AddMot(int vehicleId, string name, DateTime motdate, int Mileage, string status, string report)
        {
            var vehicle = GetVehicle(vehicleId);
            if (vehicle == null) 
            return null;

            var mot = new Mot {
            //Id created by Database
            Name = name,        
            MotDate = DateTime.Now,
            mileage= Mileage,
            Status= status,
            Report = report
            };
            db.Mots.Add(mot);
            db.SaveChanges(); // write to database
            return mot;
        }

        public bool DeleteMot(int id)
        {
            // find mot
            var mot = GetMotById(id);
            if (mot == null)
            {
                 return false;
            }
            
            // remove mot 
            db.Mots.Remove(mot);
            db.SaveChanges();
            return true;
        }

        public List<Mot> GetAllMots()
        {
            return db.Mots
                    .Include(m=> m.Vehicle)
                    .ToList();
        }


        // ==================== User Authentication/Registration Management ==================
        public User Authenticate(string email, string password)
        {
            // retrieve the user based on the EmailAddress (assumes EmailAddress is unique)
            var user = GetUserByEmail(email);

            // Verify the user exists and Hashed User password matches the password provided
            return (user != null && Hasher.ValidateHash(user.Password, password)) ? user : null;
        }

        public User Register(string name, string email, string password, Role role)
        {
            // check that the user does not already exist (unique user name)
            var exists = GetUserByEmail(email);
            if (exists != null)
            {
                return null;
            }

            // Custom Hasher used to encrypt the password before storing in database
            var user = new User 
            {
                Name = name,
                Email = email,
                Password = Hasher.CalculateHash(password),
                Role = role   
            };
   
            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }

    }
}


