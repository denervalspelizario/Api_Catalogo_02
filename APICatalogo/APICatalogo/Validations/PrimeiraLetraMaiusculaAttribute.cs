using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validations
{
    // CRIANDO VALIDACAO PERSONALIZADA
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        // sobreescrevendo o metodo ValidationResult
        // parametro value = onde vou aplicar atributo
        // validationContext = indica contexto onde estamos fazendo a validação 
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // AQUI FICA LOGICA DE VALIDAÇÃO
            
            // SE VALOR FOR IGUAL A NULL OU STRAING FOR NULL OU VAZIO
            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                // RETORNA  O METODO SUCESS DO VALIDATION RESULT
                return ValidationResult.Success; 
            }

            // PEGANDO  A PRIMEIRA LETRA DO VALUE
            var primeiraLetra = value.ToString()[0].ToString();

            // SE A PRIMEIRA LETRA FOR DIFERENTE DE MAIUSCULA
            if( primeiraLetra != primeiraLetra.ToUpper())
            {
                // RETORNA A VALIDAÇÃO
                return new ValidationResult("A primeira letra do nome do produto deve ser maiuscula");
            }

            return ValidationResult.Success;
        }
    }
}
