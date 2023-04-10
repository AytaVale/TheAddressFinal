using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TheAddress.BLL.Mapping;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.BLL.Services;
using TheAddress.BLL.Validations;
using TheAddress.DAL.Data;
using TheAddress.DAL.DBModel;
using TheAddress.DAL.Repository.Interfaces;
using TheAddress.DAL.Repository;
using TheAddress.WebAdmin.Helpers;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration().MinimumLevel.Error().MinimumLevel.Information().WriteTo.Console()
.WriteTo.File("Log/log-.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();

builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentity<AppUser, AppRole>(opts =>
{

    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    opts.Password.RequiredLength = 4;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;

}).AddEntityFrameworkStores<AppDBContext>()
.AddDefaultTokenProviders();

CookieBuilder cookieBuilder = new CookieBuilder();
cookieBuilder.Name = "TheAddress";
cookieBuilder.HttpOnly = false;
cookieBuilder.SameSite = SameSiteMode.Lax;
cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

builder.Services.ConfigureApplicationCookie(opts =>
{

    opts.LoginPath = new PathString("/Home/LogIn");
    opts.LogoutPath = new PathString("/Home/LogOut");
    opts.Cookie = cookieBuilder;
    opts.SlidingExpiration = true;
    opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
    opts.AccessDeniedPath = new PathString("/Home/AccessDenied");
});

builder.Services.AddAutoMapper(typeof(Mapping));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(PropertyValidation).Assembly);


// Add services to the container.
builder.Services.AddControllersWithViews(cfg =>
{
    cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());
});



// Configure Kestrel server limits
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 314572800; // 300 MB in bytes
});

// Configure FormOptions for file uploads
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 314572800; // 300 MB in bytes
});




var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();