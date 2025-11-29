using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVCAllProjects.Controllers
{
    public class ShareMarketGraphController : Controller
    {
        // GET: ShareMarketGraph
        public FileResult Index()
        {
            string[] xAxis = { "Amazon", "Ajio", "Myntra", "Flipkart", "Meesho" };
            string[] yAxis = { "1000", "1200", "600", "900", "1100" };
            var Bytes = new Chart(width: 500, height: 500)
                .AddTitle("Share Market Growth")
                .AddLegend("Online Shopping")
                .AddSeries
                (
                chartType: "pie",
                //chartType:"Waterfall",
                xValue: xAxis,
                yValues: yAxis)
                .GetBytes("jpeg");
            return File(Bytes,"image/jpeg");
        }
    }
}