using AccountService.Business;
using AccountService.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependecy Injector
builder.Services.AddScoped<IDbManager,DbManager>();//Database Manager
builder.Services.AddScoped<ILogic,Logic>();//Business Logic

    if(builder.Environment.IsDevelopment())
    {
        builder.Configuration.AddJsonFile("appsettings.Development.json", false);
    }
builder.Services.Configure<Configuration>(builder.Configuration.GetSection("Configuration"));//Connection String


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
