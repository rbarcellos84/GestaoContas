using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Aplicacao.Models
{
    public class ContasDashboardModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de fim.")]
        public string DataFim { get; set; }
    }
}
