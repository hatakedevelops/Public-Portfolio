using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

//add class library using statements here
using LoginService.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injector
builder.Services.Configure<Configuration>(options => builder.Configuration.GetSection("Configuration").Bind(options)); //ConnectionString Configuration
// builder.Services.AddScoped<IDbManager,DbManager>();//Database Manager
// builder.Services.AddScoped<ILogic,Logic>();//Business Logic

if(builder.Environment.IsDevelopment()) 
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", false);
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Configuration:Jwt:Issuer"],
        ValidAudience = builder.Configuration["Configuration:Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Configuration:Jwt:Key"]))
    };
});

builder.Services.AddCors(c => c.AddPolicy("EnableCors", policy => 
{
    policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
