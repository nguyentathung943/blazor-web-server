using Business.Repository;
using Business.Repository.IRepository;
using HiddenVilla_Server.Data;
using HiddenVilla_Server.Service;
using HiddenVilla_Server.Service.IService;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
    .AddDefaultUI();
// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
builder.Services.AddScoped<IHotelImagesRepository, HotelImagesRepository>();
builder.Services.AddScoped<IAmenityRepository, AmenityRepository>();

builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
DbInitializer.Initialize(app);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapRazorPages();
app.MapFallbackToPage("/_Host");

app.Run();
