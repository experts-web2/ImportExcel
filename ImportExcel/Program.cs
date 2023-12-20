using DAL.Factory;
using DAL.Interface;
using DAL.Repository;
using ImportExcel.DAL;
using ImportExcel.ExceptionHandlers;
using ImportExcel.Interface;
using ImportExcel.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using Serilog;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
builder.Services.AddScoped<IFileImportDBRepository, FileImportDBRepository>();
builder.Services.AddScoped<IExportFactory,ExportFactory>();
builder.Services.AddScoped<IFileImportJsonRepository, FileImportJsonRepository>();
builder.Services.AddScoped<IUtility, UtitlityRepository>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseMiddleware<ExceptionHandler>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Order}/{action=ImportData}/{id?}");

app.Run();
