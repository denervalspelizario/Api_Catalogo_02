using APICatalogo.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace APICatalogo.Extensions
{
    // CLASSE ESTATICA PARA CRIAR O MIDDLEWARE DE EXCEÇÃO
    // ESTA CLASSE VAI DETERMINAR A FORMA QUE SERÁ MOSTRADA O ERRO
    public static class ApiExceptionMiddlewareExtensions
    {
        // METODO ESTATICO QUE É UMA EXTENSÃO PARA INTERFACE IAaplicationBuilder
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json"; // RESPOSTA VAI SER EM JSON

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        // A ESTRUTURA DA EXCEÇÃO NO FORMATO JSON
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Trace = contextFeature.Error.StackTrace
                        }.ToString());
                    }
                });
            });
        }
    }
}
