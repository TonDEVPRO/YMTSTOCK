using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBR.Web.Models
{
    [Serializable]
    public class User
    {
        public string En { get; set; }
        public string Name { get; set; }
        public List<string> RoleNames { get; set; }

        public User()
        {
            RoleNames = new List<string>();
        }
    }
}