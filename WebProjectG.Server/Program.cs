using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebProjectG.Server.domain;
using WebProjectG.Server.domain.Gebruiker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Voeg de database context samen met de SQL server
builder.Services.AddDbContext<HuurContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<GebruikerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GebruikerDbConnection")));
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<Gebruiker>()
    .AddEntityFrameworkStores<GebruikerDbContext>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");
app.MapGet("/klanten", (HuurContext db) => db.klanten.ToList());
app.Run();
