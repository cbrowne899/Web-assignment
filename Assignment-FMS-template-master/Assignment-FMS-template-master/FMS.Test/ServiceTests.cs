
using Xunit;
using FMS.Data.Models;
using FMS.Data.Services;

namespace FMS.Test
{

    public class ServiceTests
    {
        private readonly IFleetService svc;


        public ServiceTests()
        {
            // general arrangement
            svc = new FleetServiceDb();
          
            // ensure data source is empty before each test
            svc.Initialise();
        }

        // ========================== Fleet Tests =========================
   

        // write suitable tests to verify operation of the fleet service

        [Fact]//Test to get all the movies
        public void Vehicle_GetVehiclesWhenNone_ShouldReturnNone()
        {

            //act
            var vehicles = svc.GetVehicles();
            var count = vehicles.Count;

            //assert
            Assert.Equal(0, count);

        }


        [Fact]
        public void Vehicle_AddVehicle_WhenNone_ShouldSetAllProperties()
        {
            // act 
            
            var vehicletest= new Vehicle
            {
            Reg="SB16 ERG", 
            Make="Audi", 
            Model="Sport", 
            Year= 2015, 
            FuelType="Petrol", 
            BodyType= "Hatchback", 
            TransmissionType="Automatic", 
            Doors= 4 
            };
            svc.AddVehicle(vehicletest.Reg, vehicletest.Make, vehicletest.Model, vehicletest.Year, vehicletest.FuelType, vehicletest.BodyType, vehicletest.TransmissionType, vehicletest.Doors, vehicletest.MotDue);
            
            // retrieve vehicle just added by using the Id returned by EF
            var v = svc.GetVehicle(vehicletest.VehicleId);

            // assert - that vehicle is not null
            Assert.NotNull(v);
            
            // now assert that the properties were set properly
            Assert.Equal(vehicletest.VehicleId, v.VehicleId);
            Assert.Equal(vehicletest.Make, v.Make);
            Assert.Equal(vehicletest.Model, v.Model);
            Assert.Equal(vehicletest.Year, v.Year);
            Assert.Equal(vehicletest.FuelType, v.FuelType);
            Assert.Equal(vehicletest.BodyType, v.BodyType);
            Assert.Equal(vehicletest.TransmissionType, v.TransmissionType);
            Assert.Equal(vehicletest.Doors, v.Doors);
            //Assert.Equal(DateTime(2022, 06, 05)), v.MotDue);
        }
        
        [Fact]//test to update an exisiting movie
        public void Vehicle_UpdateWhenExists_ShouldSetAllProperties()
        {

            //arrane - create test vehicle
            var vehicletest= new Vehicle
            {
            Reg="SB16 ERG", 
            Make="Audi", 
            Model="Sport", 
            Year= 2015, 
            FuelType="Petrol", 
            BodyType= "Hatchback", 
            TransmissionType="Automatic", 
            Doors= 4 
            };
            
            svc.AddVehicle(vehicletest.Reg, vehicletest.Make, vehicletest.Model, vehicletest.Year, vehicletest.FuelType, vehicletest.BodyType, vehicletest.TransmissionType, vehicletest.Doors, vehicletest.MotDue);

            //act - update test vehicle 

            vehicletest.Reg= "NM1 0YU";
            vehicletest.Make = "Mercedes";
            vehicletest.Model= "A-Class";
            vehicletest.Year = 2021;
            vehicletest.FuelType= "Diesel";
            vehicletest.BodyType="Sedan";
            vehicletest.TransmissionType= "Manual";
            vehicletest.Doors= 5;

            var updated = svc.UpdateVehicle(vehicletest);
            var vehicle = svc.GetVehicle(vehicletest.VehicleId);

            //assert- will return true if vehicle has been updated and Registration is equal


            Assert.Equal(vehicle.Reg, vehicletest.Reg);
        }

        [Fact]//test to add a vehicle which has a duplicate reg
        public void Vehicle_AddVehicleWhenDuplicateReg_ShouldReturnNull()
        {
             //arrane - create test vehicle
            var vehicletest= new Vehicle
            {
            Reg="SB16 ERG", 
            Make="Audi", 
            Model="Sport", 
            Year= 2015, 
            FuelType="Petrol", 
            BodyType= "Hatchback", 
            TransmissionType="Automatic", 
            Doors= 4 
            };
            var test1= svc.AddVehicle(vehicletest.Reg, vehicletest.Make, vehicletest.Model, vehicletest.Year, vehicletest.FuelType, vehicletest.BodyType, vehicletest.TransmissionType, vehicletest.Doors, vehicletest.MotDue);

            //act - create and add duplicate vehicle
            var vehicletest2 = new Vehicle
             {
            Reg="SB16 ERG", 
            Make="Audi", 
            Model="Sport", 
            Year= 2015, 
            FuelType="Petrol", 
            BodyType= "Hatchback", 
            TransmissionType="Automatic", 
            Doors= 4 
            };
            var test2 = svc.AddVehicle(vehicletest.Reg, vehicletest.Make, vehicletest.Model, vehicletest.Year, vehicletest.FuelType, vehicletest.BodyType, vehicletest.TransmissionType, vehicletest.Doors, vehicletest.MotDue);

            //assert
            Assert.NotNull(test1);
            Assert.Null(test2);
        }

        // [Fact]//Test to delete a vehicle that does not exist
        // public void Vehicle_DeleteVehicle_ThatDoeNotExist_ShouldReturnFalse()
        // {
        //     // act 	
        //     var deleted = svc.Delete(0);

        //     // assert
        //     Assert.False(deleted);
        // }

}
}