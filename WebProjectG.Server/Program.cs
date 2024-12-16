using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebProjectG.Server.domain;
using WebProjectG.Server.domain.GebruikerFiles;
using WebProjectG.Server.domain.GebruikerFiles.Controllers;
using WebProjectG.Server.domain.GebruikerFiles.RoleFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Voeg de database context samen met de SQL server
builder.Services.AddDbContext<HuurContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<GebruikerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GebruikerDbConnection")));
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<Gebruiker>()
    .AddEntityFrameworkStores<GebruikerDbContext>();

//Localstorage, sessionId en cookies
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(30);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});

//koppel backend aan frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allowvite",
        builder => builder.WithOrigins("https://localhost:5173").AllowAnyMethod().AllowAnyHeader()); 
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("Allowvite");
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapIdentityApi<Gebruiker>();
app.UseSession();

//Delete cookies, end session.
app.MapPost("/logout", async (SignInManager<Gebruiker> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();


//if logged in, who's logged in? can be used for identifying roles.
app.MapGet("/pingauth", (ClaimsPrincipal user) =>
{
    var email = user.FindFirstValue(ClaimTypes.Email);
    return Results.Json(new { Email = email }); ;
}).RequireAuthorization();


// Configure the HTTP request pipeline.
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

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");
    app.Run();
