using JasperServer.Client.Core;
using Report.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Report.Controllers
{
    public class RecController : Controller
    {
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Produces("text/html")]
        public async Task<ActionResult> DisplayWebPage()
        {
            string date = "";
            date = HttpContext.Request.RawUrl;
            string[] argum = date.Split('?');
            string[] varia = argum[1].Split('&');
            string[] loulou = varia[0].Split('=');
            Stream stream = null;
         
            string apireq = "/reports/SOFINANCE/Reports/"+loulou[1]+".html?" +argum[1].Replace(varia[0]+"&", "")+ "&page=1";
            JasperserverRestClient jasperserverRestClient = new JasperserverRestClient("jasperadmin", "jasperadmin", "http://172.16.6.180:8080/jasperserver/rest_v2/reports");
            stream = jasperserverRestClient.Get(apireq);
            stream.Seek(0, SeekOrigin.Begin);
            string xtmp = "";
            try
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        xtmp = xtmp + line;
                    }
                }
            }
            catch (Exception e)
            {
                xtmp = e.Message;
            }
            ViewData["rap"] = xtmp;
            return View();
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Produces("text/html")]
        public async Task<ActionResult> edition( string rap ,string drap, string ext)
        {
            Stream stream = null;
            string apireq = "/reports/SOFINANCE/Reports/" + rap+ "." + ext + "?" + drap;
            JasperserverRestClient jasperserverRestClient = new JasperserverRestClient("jasperadmin", "jasperadmin", "http://172.16.6.180:8080/jasperserver/rest_v2/reports");
            stream = jasperserverRestClient.Get(apireq);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream,"rien",rap+"."+ext);
        }
    }
}