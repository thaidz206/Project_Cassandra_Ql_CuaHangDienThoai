using Microsoft.AspNetCore.Mvc;
using Project_Cassandra.Data;
using Project_Cassandra.Models; // Đảm bảo dòng này có trong CassandraTestController.cs

namespace Project_Cassandra.Controllers
{
    public class CassandraTestController : Controller
    {
        private readonly CassandraConnection _cassandraConnection;

        public CassandraTestController(CassandraConnection cassandraConnection)
        {
            _cassandraConnection = cassandraConnection;
        }

        public IActionResult Index()
        {
            var session = _cassandraConnection.GetSession();
            var rowSet = session.Execute("SELECT * FROM brands");

            var brands = new List<Brand>();
            foreach (var row in rowSet)
            {
                var brand = new Brand
                {
                    BrandId = row.GetValue<Guid>("brand_id"),
                    BrandName = row.GetValue<string>("brand_name")
                };
                brands.Add(brand);
            }

            return View(brands);
        }
    }
}
