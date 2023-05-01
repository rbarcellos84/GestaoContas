
using GestaoContas.Dominio.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Aplicacao.Models
{
    public class ContasConsultaModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de fim.")]
        public string DataFim { get; set; }

        [Required(ErrorMessage = "Por favor, informe o tipo de conta.")]
        public string Conta { get; set; }

        [Required(ErrorMessage = "Por favor, informe o tipo de relatório.")]
        public string Formato { get; set; }

        [Required(ErrorMessage = "Por favor, informe o tipo de extração.")]
        public string TipoConsulta { get; set; }

        public List<Conta>? Itens { get; set; }

        public void ContasRelatorioModel(List<Conta>? lista)
        {
            Itens = new List<Conta>();

            if (lista != null)
            {
                foreach (var i in lista)
                {
                    Itens.Add(i);
                }
            }
            else
            {
                Itens = null;
            }
        }
    }
}
