using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Helpers;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Repository;
using RunGroupWebApp.Sevices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Add Interface and Repository
builder.Services.AddScoped<IClubRepository,ClubRepository>();
builder.Services.AddScoped<IRaceRepository,RaceRepository>();
//Add Cloudinary Sevice
builder.Services.AddScoped<IPhoteService, PhotoService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
//Using Entity Framework create database Step 3: next step see "appsettings.json"
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

//Using Entity Framework create database Step 7: Run seeddata
//Using Entity Framework create database Step 8: In developer Powershell, use command "dotnet run seeddata"
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    //Seed.SeedUsersAndRolesAsync(app);
    Seed.SeedData(app);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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