using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Michael_Trimble_Challenge.Code;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Michael_Trimble_Challenge.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    public class HomeController : Controller
    {
        [Route("/solveMaze")]
        [HttpPost]
        public JsonResult solveMaze(string map)
        {
            return Json(SolveMaze.Solve(map));
        }
    }
}
