using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SqlScript.Models;
using System.Data.SqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using Microsoft.Extensions.FileProviders;


namespace SqlScript.Controllers
{
    public class RunScript : Controller
    {
        DatabaseContext _databaseContext = new DatabaseContext();
        private readonly HttpContextAccessor Accessor;
        private readonly IToastNotification _toastNotification;

        public RunScript(HttpContextAccessor accessor , IToastNotification toastNotification)
        {
            Accessor = accessor;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            ViewBag.Connection = this._databaseContext.DbConnectionString.ToList();
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> UploadFileViaModel(FileInputModel model)
        {
            if (model == null ||
                model.FileToUploadList == null || model.FileToUploadList.Count == 0)
                return Content("file not selected");
            var result = string.Empty;
            try
            {
                foreach (var file in model.FileToUploadList)
                {

                    var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Script's",
                            DateTime.Now.ToString("yyyy-MM-dd-hh-mm") + "---" + file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
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
                            var returnData = RunScriptData(data, dataSource, userID, password, initialCatalog);
                            result += returnData + "\r\n";

                            fileData.Close();

                            if (result.Contains("Exception"))
                            {

                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                }
                                path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot/Script's",
                                DateTime.Now.ToString("yyyy-MM-dd-hh-mm") + "---" + "Error" + "---" + file.FileName);

                                using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                                fs.Seek(0, SeekOrigin.Begin);

                                StreamWriter sw = new StreamWriter(fs);
                                await sw.WriteLineAsync(result);
                                //sw.WriteLine(result);

                                sw.Close();
                                fs.Close();

                                _toastNotification.AddErrorToastMessage(file.FileName + result);

                            }
                            else
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                }
                                path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot/Script's",
                                DateTime.Now.ToString("yyyy-MM-dd-hh-mm") + "---" + file.FileName);

                                using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                                fs.Seek(0, SeekOrigin.Begin);

                                StreamWriter sw = new StreamWriter(fs);
                                await sw.WriteLineAsync(result);
                                //sw.WriteLine(result);

                                sw.Close();
                                fs.Close();

                                _toastNotification.AddSuccessToastMessage(file.FileName + result);
                            }
                        }
                    }
                }
                

            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.ToString());
            }
            return RedirectToAction("Index");
        }

        public  string RunScriptData(string data, string dataSource, string userID, string password, string initialCatalog)
        {
            var result = string.Empty;
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = dataSource;
                builder.UserID = userID;
                builder.Password = password;
                builder.InitialCatalog = initialCatalog;
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(data, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int inc = 0; inc < reader.FieldCount; inc++)
                                {
                                    result += reader[inc] + "\r\n";
                                }
                            }
                            reader.Close();
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                return e.ToString();
            }
            return result;
        }



    }
}
