using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab2.Pages
{
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPostCreate()
        {
            var name = Request.Form["pName"].ToString();
            var tactics = new List<string>();
            var shortName = name.ToLower().Replace(" ", "-");
            var description = Request.Form["Description"].ToString();

            foreach(var tacticID in JSONTypes.TacticNames.Keys)
            {
                if (Request.Form.ContainsKey(tacticID))
                {
                    var tactic = JSONTypes.TacticNames.Where(x => x.Key == tacticID).Select(y => y.Value.Key).First();
                    JSONTypes.PatternsUsed[tactic].Add(name);
                    tactics.Add(tactic);
                    
                }
            }
            JSONTypes.PatternTactics.Add(name, tactics);
            JSONTypes.Descriptions.Add(name, description);


        }
    }
}
