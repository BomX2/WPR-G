using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Voeg de database context samen met de SQL server
builder.Services.AddDbContext<HuurContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//koppel backend aan frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allowvite",
        builder => builder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader()); 
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("Allowvite");
app.UseDefaultFiles();
app.UseStaticFiles();



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
app.MapGet("/", (HuurContext db) => db.klanten.ToList());
app.Run();
