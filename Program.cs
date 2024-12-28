using Dapper;
using Microsoft.Data.SqlClient;
using static Dapper.SimpleCRUD;
using System.Data;
using Sample.Services.IServices;
using Sample.Services;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure your database connection string
var connectionString = builder.Configuration.GetConnectionString("Connection");

// Register the connection string for dependency injection
builder.Services.AddScoped<IDbConnection>((sp) => new SqlConnection(connectionString));

// Initialize Dapper.SimpleCRUD to use SQL Server dialect
SimpleCRUD.SetDialect(Dialect.SQLServer);

//register the service
builder.Services.AddScoped<IEmployeeService, EmployeeService>();



var app = builder.Build();





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
