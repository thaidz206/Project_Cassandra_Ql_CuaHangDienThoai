using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration; // Thêm dòng này
using Project_Cassandra.Data;
using AspNetCore.Identity.Cassandra.Extensions;
using AspNetCore.Identity.Cassandra;
using Cassandra;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ CassandraConnection
builder.Services.AddSingleton<CassandraConnection>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>(); // Lấy IConfiguration
    var contactPoint = configuration.GetConnectionString("CassandraConnection").Split(';')[0].Split('=')[1]; // Lấy Contact Point từ appsettings.json
    var port = configuration.GetConnectionString("CassandraConnection").Split(';')[1].Split('=')[1];
    var keyspace = configuration.GetConnectionString("CassandraConnection").Split(';')[2].Split('=')[1];
    return new CassandraConnection(contactPoint.Trim(), int.Parse(port.Trim()), keyspace); // Trim để loại bỏ khoảng trắng
});

// Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

var app = builder.Build(); // Đảm bảo đây là dòng thứ 3

// Cấu hình middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
