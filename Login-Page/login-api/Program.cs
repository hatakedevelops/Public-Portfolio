using login_api.data;
using login_api.logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injector
builder.Services.AddScoped<IDbManager,DbManager>();//Data Access
builder.Services.AddScoped<ILogic, Logic>();//Business Logic


if(builder.Environment.IsDevelopment()) 
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", false);
}

builder.Services.Configure<Configuration>(builder.Configuration.GetSection("Configuration"));//Connection String

builder.Services.AddCors(c => c.AddPolicy("EnableCors", policy => 
{
    policy
        .WithOrigins("http://localhost:4200")
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

app.UseCors("EnableCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
