using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab2.Pages
{
    public class TechniqueModel : PageModel
    {
        public static string Name { get; private set; }
        public static string Tactic { get; private set; }
        public static string Description { get; private set; }

        public void OnGet()
        {
            Name = HttpContext.Request.Query["name"].ToString();
            var shortName = HttpContext.Request.Query["tactic"].ToString();
            Tactic = JSONTypes.TacticNames.Where(x => x.Value.Key == shortName).Select(y => y.Value.Value).First();
            Description = JSONTypes.obj.Objects.Where(x => x.name == Name).Select(y => y.Description).First();
        }

    }
}
