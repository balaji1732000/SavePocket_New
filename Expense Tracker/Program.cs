using Expense_Tracker.Db;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Database Integration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// the AddIdentity method will configure the Identity services to use your custom user model, and the
// AddEntityFrameworkStores method will set up the Entity Framework data store for user and role persistence.
//builder.Services.AddIdentity<Account, IdentityRole<int>>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddSession();

builder.Services.AddControllersWithViews();


var app = builder.Build();
//Synfusion License Key
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqVk1mQ1BHaV1CX2BZd1lzRWlYek4BCV5EYF5SRHNeSlxjTHxQf0RnXH8=;Mgo+DSMBPh8sVXJ1S0R+X1pCaV5BQmFJfFBmTGlcf1RxcUU3HVdTRHRcQlhiQX9Tc0dgUH5YcHQ=;ORg4AjUWIQA/Gnt2VFhiQlJPcEBDXnxLflF1VWpTel56dVBWESFaRnZdQV1mSHZSdkBhWnZfd3JV;MjEwMjY2NUAzMjMxMmUzMjJlMzNkdUJhR01uL0R3T1JFdDBLYURpWHh5MjJYdmxaUnVYbnhzU0tvVndwT1pJPQ==;MjEwMjY2NkAzMjMxMmUzMjJlMzNhVWVveUtyKzJDNks2YWxvY0NHMGVXbHVJMXlGZThvT3llYmFvVktDZVhBPQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfVldXGBWfFN0RnNQdVtwflRDcC0sT3RfQF5jTH9bd0VmW3xXcnFURg==;MjEwMjY2OEAzMjMxMmUzMjJlMzNLS293STVWemE4alNMdk5UaWpVeVIwWWNKZ3Y2bTJPS3lpWUoxTVZtR3JRPQ==;MjEwMjY2OUAzMjMxMmUzMjJlMzNqYThGT2ZoYzJEMTRDOGFPWHNIblZHZ2JuZ2NERkxhWFZwZFMwVFgzR2Z3PQ==;Mgo+DSMBMAY9C3t2VFhiQlJPcEBDXnxLflF1VWpTel56dVBWESFaRnZdQV1mSHZSdkBhWnZdcnJV;MjEwMjY3MUAzMjMxMmUzMjJlMzNLcDNuUm1ZNkpLLzBSVEJ5YU5wcnByckpCZmlrUVkyMlp0SXZNamUzamlrPQ==;MjEwMjY3MkAzMjMxMmUzMjJlMzNhUkRCQUpZL0hGdlpPR3RwNnZTc0RueElndWRnV291UUVJWXJLMjZReUdJPQ==;MjEwMjY3M0AzMjMxMmUzMjJlMzNLS293STVWemE4alNMdk5UaWpVeVIwWWNKZ3Y2bTJPS3lpWUoxTVZtR3JRPQ==");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
//Uses Session Object to store the current user
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
