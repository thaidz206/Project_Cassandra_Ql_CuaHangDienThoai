using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Cassandra.Data;
using Project_Cassandra.Models;

namespace Project_Cassandra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CassandraConnection _connection;
        private readonly Cassandra.ISession _session;

        public HomeController(ILogger<HomeController> logger, CassandraConnection connection)
        {
            _logger = logger;
            _connection = connection;
            _session = _connection.GetSession();
        }

        public IActionResult Index()
        {
            var result = _session.Execute("select * from brands");

            List<Brand> brands = new List<Brand>();
            foreach (var r in result)
            {
                brands.Add(new Brand()
                {
                    BrandId = r.GetValue<Guid>("brand_id"),
                    BrandName = r.GetValue<string>("brand_name")
                });
            }
            ViewData["brands"] = brands;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
