using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperGalerieInfinie.Data;
using SuperGalerieInfinie.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SuperGalerieInfinieContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SuperGalerieInfinieContext") ??
                         throw new InvalidOperationException("Connection string 'SuperGalerieInfinieContext' not found."));
    options.UseLazyLoadingProxies();

});
builder.Services.AddIdentity<Utilisateur, IdentityRole>().AddEntityFrameworkStores<SuperGalerieInfinieContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    //pw hyper simple pour pas se faire chier a postman
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 0;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});
//gestion cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow all", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

// Ajoutez cette ligne pour ajouter les services d'autorisation
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow all");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();