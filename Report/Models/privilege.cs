using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Report.Models
{
    public class privilege
    {
        public int PrivId { get; set; }
        public int UserId { get; set; }
        public int EtatId { get; set; }
        public string NomEtat { get; set; }
        public string DesEtat { get; set; }
        public int CatId { get; set; }
    }
}