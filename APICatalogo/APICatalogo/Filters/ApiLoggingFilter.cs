using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{

    // ESTA CLASSE SERA USADA PARA FAZER AÇÕES ESPECIFICAS ANTES E DEPOIS DE UM MÉTODO ACTION
    public class ApiLoggingFilter : IActionFilter // IMPLEMENTANDO A INTERAFCE IACTIONFILTER
    {                                             // POIS SERÁ UMA ABORDAGEM SINCRONA

        // VARIAVEL APENAS DE LEITURA CHAMADA _logger QUE SERA TIPO ApiLoggingFilter
        // A INTERFACE ILOGGER FAZ PARTE DO SISTEM DE REGISTRO DA PLATAFORMA .NET
        private readonly ILogger <ApiLoggingFilter>_logger;

        // CONSTRUTOR QUE FARA A INJEÇÃO DE DEPENDENCIA
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        // METODO QUE EXECUTA ANTES DO METODO ACTION
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // USANDO LOGINFORMATION MOSTRA MSG 
            _logger.LogInformation("## Executando -> OnActionExecuted"); // STRING
            _logger.LogInformation("##################"); // STRING
            _logger.LogInformation($"{DateTime.Now.ToLongDateString()}"); // DATA ATUAL
            _logger.LogInformation($"MOdelState : {context.ModelState.IsValid}"); // SE MODELSTATE É VALIDO OU NÃO 
            _logger.LogInformation("##################"); // STRING
        }

        // METODO QUE EXECUTA DEPOIS DO METODO ACTION
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // USANDO LOGINFORMATION MOSTRA MSG 
            _logger.LogInformation("## Executando -> OnActionExecuted"); // STRING
            _logger.LogInformation("##################"); // STRING
            _logger.LogInformation($"{DateTime.Now.ToLongDateString()}"); // DATA ATUAL
            _logger.LogInformation($"MOdelState : {context.ModelState.IsValid}"); // SE MODELSTATE É VALIDO OU NÃO 
            _logger.LogInformation("##################"); // STRING
        }
    }
}
