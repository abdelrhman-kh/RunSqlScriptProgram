using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.EntityFrameworkCore;

namespace SqlScript.Controllers
{
    public class RunScript : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
