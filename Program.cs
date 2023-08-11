using Microsoft.EntityFrameworkCore;
using SSquaredApplication.DataAccessLayer;
using SSquaredApplication.Processor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/*builder.Services.AddDbContext<SSEnterpriseContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SSEnterprisesContext"));
});
*/

builder.Services.AddDbContext<SSEnterpriseContext>(cfg => cfg.UseSqlite(builder.Configuration.GetConnectionString("SSEnterprisesContext")), ServiceLifetime.Singleton);
builder.Services.AddTransient<IEmployeeDAL, EmployeeDAL>();
builder.Services.AddTransient<IEmployeeProcessor, EmployeeProcessor>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Employee/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
