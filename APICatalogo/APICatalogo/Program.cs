using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* Resolvendo o problema de serializacao de categoria/produtos 
 * (o primeiro get de CategoriaControllers) que retorna todos as categorias e produtos
   foi mudado 
   
   builder.Services.AddControllers()

   para
   
   builder.Services.AddControllers().AddJsonOptions( options => 
   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
  );    
 */

builder.Services.AddControllers().AddJsonOptions( options => 
 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// obtendo a string de conexão
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// definindo ao services o context, e o provedor do  bd(MySql) 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection,
    ServerVersion.AutoDetect(mySqlConnection)));

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
