using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebProjectG.Server.domain;
using WebProjectG.Server.domain.GebruikerFiles;
using WebProjectG.Server.domain.GebruikerFiles.Controllers;
using WebProjectG.Server.domain.GebruikerFiles.RoleFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Database contexts
builder.Services.AddDbContext<GebruikerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GebruikerDbConnection")));
builder.Services.AddDbContext<HuurContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// add identity services
builder.Services.AddIdentity<Gebruiker, IdentityRole>()
    .AddEntityFrameworkStores<GebruikerDbContext>()
    .AddDefaultTokenProviders();

// add cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict; 
    options.ExpireTimeSpan = TimeSpan.FromHours(6); // cookie expiration time
    options.SlidingExpiration = true; // extend cookie every time the user interacts
    options.LoginPath = "/account/login"; // Redirect to login page when unauthorized
    options.AccessDeniedPath = "/account/access-denied"; // Redirect when access is denied
});

// Add CORS policy for frontend-backend communication
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allowvite", policyBuilder =>
    {
        policyBuilder.WithOrigins("https://localhost:5173")
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials();
    });
});

// Add controllers and API endpoints
builder.Services.AddControllers();

// Add Swagger/OpenAPI for API testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable role service
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<Gebruiker>>();
    await SeedRoles.Initialize(services, userManager);
}

// Enable CORS policy
app.UseCors("Allowvite");

// Enable HTTPS redirection and serve static files for SPA
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map API routes
app.MapControllers();

// Ensure the SPA serves on fallback routes
app.MapFallbackToFile("/index.html");


// Run the application
app.Run();