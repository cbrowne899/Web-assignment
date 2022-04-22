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
       
        
        //retrieve list of Vehicles ordered by a certain property
        public List<Vehicle> GetVehicles(string order=null)
        {
            List<Vehicle> vehicle = new List<Vehicle>();
           switch (order)
            {
                case "regOrder":
                 vehicle = db.Vehicles.OrderBy( v=> v.Reg).ToList();
                 break;
                 case "makeOrder":
                 vehicle = db.Vehicles.OrderBy(v=> v.Make).ToList();
                 break;
                 case "yearOrder":
                 vehicle = db.Vehicles.OrderBy(v=> v.VehicleId).ToList();
                 break;
                 default:
                 vehicle = db.Vehicles.ToList();
                 break;
            }
                 return vehicle;

           }

        
        //retrieve Vehicle by Id
        public Vehicle GetVehicle(int id)
        {
            return db.Vehicles
                            .Include(v=> v.Mots)
                            .FirstOrDefault(v => v.VehicleId == id);
        }

        //retrieve Vehicle by Registration
        public Vehicle GetVehicleByReg(string reg)
        {
            return db.Vehicles.FirstOrDefault(v => v.Reg == reg);
        }

        //Add a new Vehicle - checking no vehicle with the same Registration No exists
        public Vehicle AddVehicle(string reg, string make, string model, int year, string fueltype, string bodytype, string transmissiontype, int doors, DateTime mot)
        {
            var exists = GetVehicleByReg(reg);

            if (exists !=null) //if not returned null - can not add 

            {
                return null; //reg in use so can't create Vehicle
            }

            var vehicle = new Vehicle  //reg is unique so we can create Vehicle
           
                //id automatically set by database
            {
                Reg = reg,
                Make=make,
                Model = model,
                Year= year,
                FuelType= fueltype,
                BodyType= bodytype,
                TransmissionType= transmissiontype, 
                Doors= doors, 
                MotDue = DateTime.Now

            };
            db.Vehicles.Add(vehicle);
            db.SaveChanges();
            return vehicle;

        }


        //adding vehicle through passing a Vehicle as a parament
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
                MotDue =v1.MotDue,

                
                
            };
            db.Vehicles.Add(vehicle); 
            //add vehicle to list 
            db.SaveChanges(); // save changes to database
            return vehicle;
        }

        //updating vehicle with details in updated
        public Vehicle UpdateVehicle (Vehicle updated)
        {
            var vehicle = GetVehicle(updated.VehicleId);

            if (vehicle==null)
            {
                return null;
            }

            //updating details of the vehicle retrieved 
            vehicle.Reg= updated.Reg;
            vehicle.Make=updated.Make;
            vehicle.Model=updated.Model;
            vehicle.Year=updated.Year;
            vehicle.FuelType=updated.FuelType;
            vehicle.BodyType=updated.BodyType;
            vehicle.Doors=updated.Doors;
            vehicle.MotDue=updated.MotDue;
            

            db.SaveChanges();     //save changes
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

        //retriving Mots via id 
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


        //creating new mot
        public Mot CreateMot(int VehicleId, string name, int mileage, string status, string report)
        {
            var vehicle = GetVehicle(VehicleId);
            if (vehicle == null) 
            return null;

            var mot = new Mot {
            //Id created by Database 
            VehicleId= VehicleId,
            Name = name,
            mileage = mileage,
            Status = status,      
            MotDate = DateTime.Now,
            Report = report
            };

            vehicle.MotDue= DateTime.Now.AddMonths(12);
            db.Mots.Add(mot);
            db.SaveChanges(); // write to database
            return mot;
        }


        // public Mot AddMot(int vehicleId, string name, DateTime motdate, int Mileage, string status, string report)
        // {
        //     var vehicle = GetVehicle(vehicleId);
        //     if (vehicle == null) 
        //     return null;

        //     var mot = new Mot {
        //     //Id created by Database
        //     Name = name,        
        //     MotDate = DateTime.Now,
        //     mileage= Mileage,
        //     Status= status,
        //     Report = report
        //     };
        //     db.Mots.Add(mot);
        //     db.SaveChanges(); // write to database
        //     return mot;
        // }

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

        public List<Mot> GetFailMots()
        {
            return db.Mots
                    .Include(m=> m.Vehicle)
                    .Where (m=> m.Status=="Fail")
                    .ToList();
        }

        public List<Mot> SearchMots(MotRange range, string query)
        {
            query=query ==null? "": query.ToLower();

            var results =db.Mots
                            .Include(m=> m.Vehicle)
                            .Where(m=> (m.Report.ToLower().Contains(query) ||
                            m.Vehicle.Reg.ToLower().Contains(query)
                            // )&& 
                            // (range == MotRange.ALL ||
                            // range == MotRange.PASS ||
                            // range = MotRange.FAIL
                            // 
                            )

                            ).ToList();
                            return results;
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


