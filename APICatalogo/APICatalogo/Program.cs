using APICatalogo.Context;
using APICatalogo.Services;
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

// ESTOU FAZENDO UM TEMPO DE VIDA TRANSIENT E INFORMO QUE NESSE 
// CADA VEZ QUE ALGUEM SOLICITAR A DEPENDENCIA MeuServico ela tb chamara IMeuServico 
builder.Services.AddTransient<IMeuServico, MeuServico>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// obtendo a string de conexão
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// OBTENDO INFORMACAO DA CHAVE1, CHAVE2 E SECAO1
var valor1 = builder.Configuration["chave1"];
var valor2 = builder.Configuration["secao1:chave2"];

// definindo ao services o context, e o provedor do  bd(MySql) 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection,
    ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

// A PARTIR DO ASP.NET6 OS MIDDLEWARE SÃO DEFINIDOS NA CLASSE PROGRAM
// AS ORDENS DO MIDDLEWARE SÃO IMPORTANTES CUIDADO


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // MIDDLEWARE DO SWAGGER
    app.UseSwaggerUI(); // MIDDLEWARE DO SWAGGER QUE O USUARIO INTERAGE
    
    // MIDDEWARE QUE ACESSA AS PAGINAS DE EXCESSÃO DAS PAGINAS DO DESENVOLVEDOR
    app.UseDeveloperExceptionPage();


}

app.UseHttpsRedirection();

app.UseAuthorization(); // MIDDLEWARE QUE FAZ AS AUTORIZACOES DE PERMISSÃO DE ACESSO

app.Use(async (context, next) =>
{
    // adicionar um codigo antes da request
    await next(context);

    // adicionar o codigo depois do request
});

app.MapControllers(); // MIDDLEWARE QUE MAPEIA OS CONTROLLER DA APLICAÇÃO


// USADO COMO MIDDLEWARE TERMINAL
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Middleware final");
});

app.Run();
