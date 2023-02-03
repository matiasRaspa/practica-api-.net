using APIAutos.Data;
using APIAutos.Data.Repositories;
//Import de la segunda forma comentar para usar la primera
//using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//1.Crear conexion a MySql leyendo la cadena de coneccion que colocamos en appsettings.json
//Dependencias necesarias: Dapper - MySql.Data 
//Primera forma para testear app, se ejecuta conexion cada vez que llamamos al repositorio: Con dependecia dapper y mysql.data en api.autos.data
//Singleton:
var mySqlConfiguration = new MySqlConfiguration(builder.Configuration.GetConnectionString("MySqlConnection"));
builder.Services.AddSingleton(mySqlConfiguration);
//Segunda forma mas profesional como por ejemplo un proyecto que va a produccion, se devuelve una conexion solo cuando no existe: Con dependencia mysql.data a nivel proyecto api autos general
//builder.Services.AddSingleton(new MySqlConnection(builder.Configuration.GetConnectionString("MySqlConnection")));
//Agrego al contenedor de dependencias la instancia al repositorio
builder.Services.AddScoped<ICarRepository, CarRepository>();

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
