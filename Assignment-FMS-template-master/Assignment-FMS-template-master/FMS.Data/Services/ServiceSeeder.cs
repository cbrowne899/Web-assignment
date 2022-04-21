using System;
using System.Text;
using System.Collections.Generic;
using FMS.Data.Models;

namespace FMS.Data.Services
{
    public static class FleetServiceSeeder
    {
        // use this class to seed the database with dummy test data using an IFleetService
        public static void Seed(IFleetService svc)
        {
            svc.Initialise();

            // add seed data
            
            //adding vehicles
            var s1 = svc.AddVehicle("P98 222","Toyta", "Yaris", 2005, "Petrol", "Pickup", "Manual", 2, DateTime.Now);

            //adding MOT
            var m1 = svc.AddMot( s1.VehicleId, "Chloe", new DateTime(2023,11,20), 16000, "Fail", "Breaks failed testing");
            

            var u1 = svc.Register("Chloe Browne", "admin@mail.com", "admin", Role.admin);
            var u2 = svc.Register("Kate Toland", "manager@mail.com", "manager", Role.manager);
            var u3 = svc.Register("Lisa Browne", "guest@mail.com", "guest", Role.guest);
        }

        
   
            

    }
}
