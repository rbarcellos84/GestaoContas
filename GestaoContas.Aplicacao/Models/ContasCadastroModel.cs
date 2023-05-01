using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Aplicacao.Models
{
    public class ContasCadastroModel
    {
        [Required(ErrorMessage = "Por favor, informe o nome da conta.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data da conta.")]
        public string Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe o valor da conta.")]
        public string Valor { get; set; }

        [Required(ErrorMessage = "Por favor, informe o tipo da conta.")]
        public string Tipo { get; set; }
    }
}
