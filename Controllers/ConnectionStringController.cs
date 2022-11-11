using Microsoft.AspNetCore.Mvc;
using SqlScript.Models;
using System.Linq;

namespace SqlScript.Controllers
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class ConnectionStringController : Controller
    {
        DatabaseContext _databaseContext = new DatabaseContext();
        public IActionResult Index(string SortField, string currentSortField, SortDirection SortDirection, string SearchByName)
        {
            var connectionstring = GetConnectionString();
            if (!string.IsNullOrEmpty(SearchByName))
                connectionstring = connectionstring.Where(option => option.ConnectionStringName.ToLower().Contains(SearchByName.ToLower())).ToList();
            return View(this.SortConnectionString(connectionstring, SortField, currentSortField, SortDirection));
        }
        
        public List<ConnectionString> GetConnectionString()
        {
            var connectionstring = (from ConnectionString in _databaseContext.DbConnectionString
                                    join Connections in _databaseContext.DbConnections on ConnectionString.ConnectionsId equals Connections.ConnectionsId
                                    select new ConnectionString
                                    {
                                        ConnectionStringID = ConnectionString.ConnectionStringID,
                                        ConnectionStringName = ConnectionString.ConnectionStringName,
                                        ConnectionStringDataSource = ConnectionString.ConnectionStringDataSource,
                                        ConnectionStringUserID = ConnectionString.ConnectionStringUserID,
                                        ConnectionStringPassword = ConnectionString.ConnectionStringPassword,
                                        ConnectionStringInitialCatalog = ConnectionString.ConnectionStringInitialCatalog,
                                        ConnectionsId = ConnectionString.ConnectionsId,
                                        ConnectionsName = Connections.ConnectionsName
                                    }).ToList();

            return connectionstring;
        }

        public IActionResult Create()
        {
            ViewBag.Connection = this._databaseContext.DbConnections.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ConnectionString model)
        {
            ModelState.Remove("ConnectionStringID");
            ModelState.Remove("ConnectionsId");
            ModelState.Remove("ConnectionsName");
            ModelState.Remove("Connections");
            if (ModelState.IsValid)
            {
                _databaseContext.DbConnectionString.Add(model);
                _databaseContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Connection = this._databaseContext.DbConnections.ToList();
            return View();
        }

        public IActionResult Edit(int ID)
        {
            ConnectionString data = this._databaseContext.DbConnectionString.Where(option => option.ConnectionStringID == ID).FirstOrDefault();
            ViewBag.Connection = this._databaseContext.DbConnections.ToList();
            return View("Create",data);
        }

        [HttpPost]
        public IActionResult Edit(ConnectionString model)
        {
            ModelState.Remove("ConnectionStringID");
            ModelState.Remove("ConnectionsId");
            ModelState.Remove("ConnectionsName");
            ModelState.Remove("Connections");
            if (ModelState.IsValid)
            {
                _databaseContext.DbConnectionString.Update(model);
                _databaseContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Connection = this._databaseContext.DbConnections.ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int ID)
        {
            ConnectionString data = this._databaseContext.DbConnectionString.Where(option => option.ConnectionStringID == ID).FirstOrDefault();
            if(data != null)
            {
                _databaseContext.DbConnectionString.Remove(data);
                _databaseContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private List<ConnectionString> SortConnectionString( List<ConnectionString> ConnectionStrings, string SortField, string currentSortField, SortDirection SortDirection)
        {
            if (string.IsNullOrEmpty(SortField))
            {
                ViewBag.SortField = "ConnectionStringID";
                ViewBag.SortDirection = SortDirection.Ascending;
            }
            else
            {
                if (currentSortField == SortField)
                    ViewBag.SortDirection = SortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
                else
                    ViewBag.SortDirection = SortDirection.Ascending;
                    ViewBag.SortField = SortField;
            }

            var propertyInfo = typeof(ConnectionString).GetProperty(ViewBag.SortField);
            if (ViewBag.SortDirection == SortDirection.Ascending)
                ConnectionStrings = ConnectionStrings.OrderBy(option => propertyInfo.GetValue(option, null)).ToList();
            else
                ConnectionStrings = ConnectionStrings.OrderByDescending(option => propertyInfo.GetValue(option, null)).ToList();
            return ConnectionStrings;
        }
    }
}
