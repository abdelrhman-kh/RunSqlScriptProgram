using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.Common;
using SqlScript.Models;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Server = Microsoft.SqlServer.Management.Smo.Server;

namespace SqlScript.Controllers
{
    public class RunScript : Controller
    {
        DatabaseContext _databaseContext = new DatabaseContext();
        private readonly HttpContextAccessor Accessor;

        public RunScript(HttpContextAccessor accessor)
        {
            Accessor = accessor;
        }

        public IActionResult Index()
        {
            ViewBag.Connection = this._databaseContext.DbConnectionString.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            //string text = File.ReadAllText(model.FileToUpload.);
            //Console.WriteLine(text);
            //string[] lines = File.ReadAllLines(model.FileToUpload.FileName);
            using (StreamReader fileData = new StreamReader(path))
            {
                var data = await fileData.ReadToEndAsync();
                string dataSource = "";
                string userID = "";
                string password = "";
                string initialCatalog = "";
                RunScriptData(data, dataSource, userID, password, initialCatalog);
            }

            return View();

            //return RedirectToAction("Files");
        }






        public async Task<IActionResult> UploadFileViaModel(FileInputModel model)
        {
            if (model == null ||
                model.FileToUpload == null || model.FileToUpload.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        model.FileToUpload.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.FileToUpload.CopyToAsync(stream);

            }


            var connectionStringData = _databaseContext.DbConnectionString.FirstOrDefault(c => c.ConnectionStringID == model.ConnectionStringID);

            if (connectionStringData != null)
            {
                using (StreamReader fileData = new StreamReader(path))
                {
                    var data = await fileData.ReadToEndAsync();
                    string dataSource = connectionStringData.ConnectionStringDataSource;
                    string userID = connectionStringData.ConnectionStringUserID;
                    string password = connectionStringData.ConnectionStringPassword;
                    string initialCatalog = connectionStringData.ConnectionStringInitialCatalog;
                    RunScriptData(data, dataSource, userID, password, initialCatalog);
                }
                
            }

            return View();
        }

        public void RunScriptData(string data, string dataSource, string userID, string password, string initialCatalog)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = dataSource;
                builder.UserID = userID;
                builder.Password = password;
               // builder.InitialCatalog = initialCatalog;
                builder.InitialCatalog = "ManualScriptDB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(data, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine();
            
        }
    }
}
