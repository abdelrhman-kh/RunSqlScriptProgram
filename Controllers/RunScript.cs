using Microsoft.AspNetCore.Mvc;
using SqlScript.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Grpc.Core;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using SqlScript.Controllers;

namespace SqlScript.Controllers
{
    public class RunScript : Controller
    {

        DatabaseContext _databaseContext = new DatabaseContext();

        private readonly HttpContextAccessor Accessor;
        public RunScript(HttpContextAccessor accessor)
        {
            this.Accessor = accessor;

        }


        public IActionResult Index()
        {
            //HttpContext context = this.Accessor.HttpContext;
            ViewBag.Connection = this._databaseContext.DbConnections.ToList();
            ViewBag.ConnectionString = this._databaseContext.DbConnectionString.ToList();

            return View();
        }

        
        public IActionResult RunScripts(ConnectionString model)
        {


            ViewBag.Connection = this._databaseContext.DbConnections.ToList();
            ViewBag.ConnectionString = this._databaseContext.DbConnectionString.ToList();

            return View();
        }

    }
}
