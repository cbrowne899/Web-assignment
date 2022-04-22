using System;
using Microsoft.AspNetCore.Mvc;
using FMS.Web.Models;
using FMS.Data.Models;
using FMS.Data.Services;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Net;


namespace MMS.Web.Controllers
{

    public class MotController : Controller 
    {
        private readonly IFleetService svc;
        
        public MotController(IFleetService ss)
        {
            svc = ss;
        }

    
        //GET/mot/index
        public IActionResult Index (MotSearchViewModel m )
    {       
            // set the viewmodel Mot property by calling service method 
            // using the range and query values from the viewmodel 
             m.Mots = svc.SearchMots(m.Range, m.Query);

             return View (m);
    }


        //display page containing JS query
       public IActionResult Query()
        {
            return View();

        }
        // display page containing Vue query
        public IActionResult VQuery()
        {
            return View();
        }
    
        //
         public IActionResult Details(int id)
        {
            var mot = svc.GetMotById(id);
            if (mot == null)
            {
                
                //Alert("Mot Not Found", AlertType.warning);  
                return RedirectToAction(nameof(Index));             
            }

            return View(mot);
        }

       //Get/ mot/delete/{id}
      
       public IActionResult Delete(int id)
       {
           var m = svc.GetMotById(id);

           if (m == null)
           {
               //Alert("Mot Not Found", AlertType.warning);  
                return RedirectToAction(nameof(Index));  
    
           }
           return View(m);
       }
      
        // POST /mot/delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            //retrieving the MOT using the service method
            var m1 = svc.GetMotById(id);
            //deleting the MOT using the service method
            svc.DeleteMot(id);
            //alerting the user that the review has been deleted
            //Alert("Review {m1.Id} Was Deleted", AlertType.danger);
            
            // redirect to the index view
            return RedirectToAction(nameof(Index));
            
        }
       
        // GET /mot/create
        public IActionResult Create()
        {
            // retrieve all vehicles
            //retrieve all mots

            var v=svc.GetVehicles();
            var m= svc.GetAllMots();

        

            var mvm= new MotCreateViewModel {
             Vehicles = new SelectList(m, "Id", "Report")
            };
            
            // render blank form
            return View(mvm);
        }
       
        // POST /mot/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MotCreateViewModel mvm)
        {
            //checking if the mot model is valid
            if(ModelState.IsValid)
            {
              svc.CreateMot(mvm.VehicleId, mvm.Name, mvm.mileage, mvm.Status, mvm.Report);
              //Alert("New MOT Record Was Created", AlertType.info);
              return RedirectToAction(nameof(Index));
            }
            
            // redisplay the form for editing as validation failed 
            return View(mvm);
        }
    }
}
