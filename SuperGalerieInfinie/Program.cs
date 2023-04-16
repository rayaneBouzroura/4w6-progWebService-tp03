using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SuperGalerieInfinie.Data;
using SuperGalerieInfinie.Data.Services;
using SuperGalerieInfinie.Models;
using System.Text;

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
    options.Password.RequireLowercase = false;
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
//gerer l'authentification
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;//duringe dev
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = "https://localhost:4200/",
        ValidIssuer = "https://localhost:7008",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("phrase suppppeeRR Long woooHoo Heck yeaaah je deprime."))
    };
});

//service utilisateur
builder.Services.AddScoped<UtilisateurService>();

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