using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Report.Models
{
    public class Etat
    {
        public int EtatId { get; set; }
        public string NomEtat { get; set; }
        public string DesEtat { get; set; }
        public string Form { get; set; }
        public int CatId { get; set; }
        public int Commun { get; set; }
    }
}