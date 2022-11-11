using SqlScript.Models;
using SqlScript.Controllers;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SqlScript.Models
{


    public class RunScript
    {
        
        public int ConnectionStringID { get;  }

        public string ConnectionStringName { get;  }

        public int ConnectionsId { get; }

        public string ConnectionsName { get; }

        
        public IFormFile ScriptFile { get;  }

    }


}
