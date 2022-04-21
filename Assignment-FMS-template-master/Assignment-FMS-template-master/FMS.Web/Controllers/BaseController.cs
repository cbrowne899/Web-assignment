using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FMS.Web.Controllers
{
    public enum AlertType { success, danger, warning, info }

    public class BaseController : Controller
    {

        // Store Alert in TempData Storage 
        // Where alert will only be accessible in next Request
        public void Alert(string message, AlertType type = AlertType.info)
        {
            TempData["Alert.Message"] = message;
            TempData["Alert.Type"] = type.ToString();
        }

    }

  
}
