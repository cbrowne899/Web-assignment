using System;
using Microsoft.AspNetCore.Mvc;
using FMS.Web.Models;
using FMS.Data.Models;
using FMS.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace MMS.Web.Controllers
{
    //[Authorize]
    public class MotController : Controller 
    {
        private readonly IFleetService svc;
        
        public MotController()
        {
            svc = new FleetServiceDb();
        }


        // GET /review/index
        public IActionResult Index()
        {
            // get all reviews
            var mot =svc.GetAllMots();


            // pass reviews to view
            return View(mot);
       }

       //Get/ review/delete/{id}
       [Authorize(Roles="admin,manager")]
       public IActionResult Delete(int id)
       {
           var m = svc.GetMotById(id);

           if (m == null)
           {
               return NotFound();
    
           }
           return View(m);
       }
      
        //  POST /reviews/delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="admin,manager")]
        public IActionResult DeleteConfirm(int id)
        {
            //retrieving the review using the service method
            var m1 = svc.GetMotById(id);
            //deleting the review using the service method
            svc.DeleteMot(id);
            //alerting the user that the review has been deleted
            //Alert("Review {m1.Id} Was Deleted", AlertType.danger);
            
            // redirect to the index view
            return RedirectToAction(nameof(Index));
            
        }
       
        // GET /review/create
        [Authorize(Roles="admin,manager")]
        public IActionResult Create()
        {
            // // retrieve all movies
            // var v = svc.GetAllMots();

            // if (v !=null)
            // {
            //     return null;
            // }

            //  var mot = new Mot{
            //   VehicleId = v.VehicleId,
            //   Name = v.Name,
            //   MotDate = v.MotDate,
            //   mileage = v.mileage,
            //   Status=mvm.Status,
            //   Report=mvm.Report
            //   };
            
            // create a ReviewViewModel and set the Movie property
            // to new SelectList(movies,"Id","Name")
            
            
            // render blank form
            return View();
        }
       
        // POST /review/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="admin,manager")]
        public IActionResult Create([Bind("MovieId, Name, Comment, Rating")] MotViewModel mvm)
        {
            //checking if the review model is valid
            if(ModelState.IsValid)
            {
              var mot = new Mot{
              VehicleId = mvm.VehicleId,
              Name = mvm.Name,
              MotDate = mvm.MotDate,
              mileage = mvm.mileage,
              Status=mvm.Status,
              Report=mvm.Report
              };
              //adding the review using the service method
              svc.AddMot(mvm.VehicleId, mvm.Name, mvm.MotDate, mvm.mileage, mvm.Status, mvm.Report);
              //Alert("New MOT Record Was Created", AlertType.info);
              return RedirectToAction(nameof(Index));
            }
            
            // redisplay the form for editing as validation failed 
            return View(mvm);
        }
    }
}
