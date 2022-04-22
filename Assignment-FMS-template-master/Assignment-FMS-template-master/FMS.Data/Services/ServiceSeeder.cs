using System;
using System.Text;
using System.Collections.Generic;
using FMS.Data.Models;

namespace FMS.Data.Services
{
    public static class FleetServiceSeeder
    {
        // use this lass to seed the database with dummy test data using an IFleetService
        public static void Seed(IFleetService svc)
        {
            svc.Initialise();
            
            //adding vehicles
            var v1 = svc.AddVehicle("P98 222","Toyta", "Yaris", 2005, "Petrol", "Pickup", "Manual", 2, DateTime.Now);
            var v2 = svc.AddVehicle("LP07 AON","Citron", "C3", 2016, "Petrol", "Sport", "Manual", 3, DateTime.Now);
            var v3 = svc.AddVehicle("SB07 EPL","Merceedes", "C-Class", 2011, "Diesel", "HatchBack", "Manual", 4, DateTime.Now);
            
            //adding MOTS
            var m1 = svc.CreateMot(1, "Jeff", 17000, "Fail", "Faulty Breaks");
            var m2= svc.CreateMot(2, "Mary", 174508, "Pass", "Vehicle was unclean, however passed 9/10 tests");
            var m3 = svc.CreateMot(3, "Sean", 190983, "Fail", "Clutch was stiff and failed safety tests");
            
            //adding user accounts
            var u1 = svc.Register("Chloe Browne", "admin@mail.com", "admin", Role.admin);
            var u2 = svc.Register("Kate Toland", "manager@mail.com", "manager", Role.manager);
            var u3 = svc.Register("Lisa Browne", "guest@mail.com", "guest", Role.guest);
        }

        
   
            

    }
}
