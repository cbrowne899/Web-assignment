using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FMS.Data.Models;
using FMS.Data.Services;

using FMS.Web.Models;

namespace FMS.Web.Controllers;
//[Authorize]
public class VehicleController : BaseController
{
    // provide suitable controller actions

    private IFleetService svc;

    public VehicleController()
    {
        svc = new FleetServiceDb();
    }

   //  public IActionResult Index(string order)
   //  {
   //      var vehicles = svc.GetVehicles(order);
        
   //      return View (vehicles);
   //  }

    public IActionResult Index()
    {
        var vehicles = svc.GetVehicles();
        
        return View (vehicles);
    }

// GET /vehicle/details/{id}
    public IActionResult Details(int id)
    {
    // retrieve the vehicle with specifed id from the service
    var v = svc.GetVehicle(id);
    
 // TBC check if v is null and return NotFound()
    if (v == null)
    {
        //Alert ("Vehicle was not found!", AlertType.warning);
        return RedirectToAction(nameof(Index));
    }
 // pass vehicle as parameter to the view
    return View(v);
 }
   [Authorize(Roles = "admin")]
 // GET: /vehicle/create
     public IActionResult Create()
 {
    // display blank form to create a vehicle

    var v = new Vehicle();
    return View(v);
 }


    // POST /student/create
   [HttpPost]
   [ValidateAntiForgeryToken]
   [Authorize(Roles = "admin")]
    public IActionResult Create(Vehicle v)
      {
//  // complete POST action to add student

    if (ModelState.IsValid)
    {
//  // TBC pass data to service to store
     svc.AddVehicle(v.Reg, v.Make, v.Model, v.Year, v.FuelType, v.BodyType,v.TransmissionType, v.Doors, v.MotDue);
      }
//  // redisplay the form for editing as there are validation errors
     return View(v);
 }


    //GET /student/edit/{id}
     public IActionResult Edit(int id)
  {
  // load the student using the service
     var v = svc.GetVehicle(id);
  // TBC check if s is null and return NotFound()
     if (v == null)
  {   
     //Alert("Vehicle was not found!", AlertType.warning);
      return NotFound();
  }
  // pass student to view for editing
     return View(v);
 }
    

      

 // POST /student/edit/{id}
     [HttpPost]
     public IActionResult Edit(int id, Vehicle v)
  {
 // complete POST action to save student changes
  if (ModelState.IsValid)
  {
  // TBC pass data to service to update
    svc.UpdateVehicle(v);
    //Alert("The Vehicle has been updated!", AlertType.info);
    return RedirectToAction(nameof(Index));
     }
 // redisplay the form for editing as validation errors
    return View(v);
  }
 
     //GET / student/delete/{id}
     [Authorize(Roles = "admin")]
    public IActionResult Delete(int id)
    {
 // load the student using the service
      var v = svc.GetVehicle(id);
 // check the returned student is not null and if so return   NotFound 
    
    if (v == null)
    {
       //Alert("Vehicle not found!", AlertType.warning);
        return NotFound();
    }
 // pass student to view for deletion confirmation
        return View(v);
    }


 
 // POST /student/delete/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
   [Authorize(Roles = "admin")]
    public IActionResult DeleteConfirm(int id)
    {
 // TBC delete student via service
    var v1 = svc.GetVehicle(id);
    svc.DeleteVehicle(id);
    //Alert ($"Vehicle {v1.VehicleId} was deleted!", AlertType.danger);
 // redirect to the index view
    return RedirectToAction(nameof(Index));
    }



// ============== Vehicle MOT management ==============

[Authorize(Roles = "admin, manager")]
public IActionResult MotCreate(int id)
        {
            var v = svc.GetVehicle(id);
            if (v == null)
            {
               //Alert("No vehicle was found", AlertType.warning);
                return NotFound();
            }

            // create a mot view model and set foreign key
            var mot = new MotCreateViewModel
            {
               VehicleId= v.VehicleId,
               reg=v.Reg,
               Year =v.Year
            };
            // render blank form
            return View( "Create Mot", v);
        }

// POST /student/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public IActionResult MotCreate(Mot m)
        {
            if (ModelState.IsValid)
            {                
                var mot = svc.AddMot(m.VehicleId, m.Name, m.MotDate, m.mileage,m.Status,m.Report);

               if (mot ==null)
               {
                  //Alert("Vehicle does not exist", AlertType.warning);
                  return RedirectToAction("Index");
               }

               var mot1= new Mot
               {
                  VehicleId=m.VehicleId,
                  Name= m.Name,
                  MotDate=m.MotDate,
                  mileage=m.mileage,
                  Status=m.Status,
                  Report=m.Report
               };
               svc.AddMot(m.VehicleId, m.Name, m.MotDate,m.mileage, m.Status,m.Report);
               //Alert ("New Mot History for Vehicle {m.VehicleId} has been added", AlertType.success);
               

               return RedirectToAction(nameof(Details), new { Id = mot.VehicleId });
            }
            // redisplay the form for editing
            return View(m);
        }

         [Authorize(Roles = "admin,manager")]
        public IActionResult MotDelete(int id)
        {
            // load the ticket using the service
            var mot = svc.GetMotById(id);
            // check the returned mot is not null and if so return NotFound()
            if (mot == null)
            {
                return NotFound();
            }     
            
            // pass ticket to view for deletion confirmation
            return View(mot);
        }

        // POST /student/ticketdeleteconfirm/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,manager")]

        public IActionResult MotDeleteConfirm(int id, int vehicleId)
        {
            // TBC delete mot via service
            var vehicle = svc.GetVehicle(vehicleId);
            var mot = svc.GetMotById(id);

            svc.DeleteMot(id);
            
            // TBC update to redirect to the vehicle details page
            return RedirectToAction(nameof(Details), new {Id = vehicleId});
        }











 }









