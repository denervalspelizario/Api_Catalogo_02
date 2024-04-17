using System.Text.Json;

namespace APICatalogo.Models
{
    public class ErrorDetails
    {
        // PROPRIEDADES
        public int StatusCode { get; set; } // TRATAR STATUS CODE
        public string Message { get; set; } // A MENSAGEM
        public string Trace { get; set; }  // RASTREAMENTO DA PILHA DE ERRO

        // METODO QUE SOBREESCREVE O METODO TOSTRING SERIALIZANDO
        // AS INFORMACOES DO ERRO NO FORMATO JSON
        public override string ToString()
        {
            return JsonSerializer.Serialize(this); 
        }
    }
}
