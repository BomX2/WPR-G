using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebProjectG.Server.domain;
using WebProjectG.Server.domain.Gebruiker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// Configure the GebruikerDbContext for your identity database
builder.Services.AddDbContext<GebruikerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GebruikerDbConnection")));

// Configure CarAndAll DbContext
builder.Services.AddDbContext<HuurContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity for the Gebruiker entity
builder.Services.AddIdentity<Gebruiker, IdentityRole>()
    .AddEntityFrameworkStores<GebruikerDbContext>()
    .AddDefaultTokenProviders();

// Add CORS policy for frontend-backend communication
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allowvite", policyBuilder =>
    {
        policyBuilder.WithOrigins("https://localhost:5173")
                     .AllowAnyMethod()
                     .AllowAnyHeader();
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
app.MapControllers(); // Maps `[ApiController]` routes from your controllers
// Map custom API endpoints
app.MapPost("/logout", async (SignInManager<Gebruiker> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

app.MapGet("/pingauth", (ClaimsPrincipal user) =>
{
    var email = user.FindFirstValue(ClaimTypes.Email);
    return Results.Json(new { Email = email });
}).RequireAuthorization();

// Ensure the SPA serves on fallback routes
app.MapFallbackToFile("/index.html");

// Example database API endpoint
app.MapGet("/", (HuurContext db) => db.klanten.ToList());

// Run the application
app.Run();
