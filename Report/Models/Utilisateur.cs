using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Report.Models
{
    public class Utilisateur
    {
        public int id { get; set; }
        public string Pseudo { get; set; }
        public string Pwd { get; set; }
        public int prof { get; set; }
    }
}