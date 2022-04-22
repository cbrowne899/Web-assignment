using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using FMS.Data.Models;


namespace FMS.Web.Models
{   
    public class MotSearchViewModel
    {
        // result set
        public IList<Mot> Mots { get; set;} = new List<Mot>();

        // search options        
        public string Query { get; set; } = "";
        public MotRange Range { get; set; } = MotRange.ALL;
    }
}
