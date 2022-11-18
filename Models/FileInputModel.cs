using Microsoft.AspNetCore.Mvc;

namespace SqlScript.Models
{
    public class FileInputModel
    {
        public List<IFormFile> FileToUploadList { get; set; }
        public int? ConnectionStringID { get; set; }





        

    }
}
