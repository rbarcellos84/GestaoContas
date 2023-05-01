using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContas.Dominio.Entities
{
    public class Pesquisa
    {
        public Guid IdPesquisa { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string Conta { get; set; }
        public string Formato { get; set; }
        public string TipoConsulta { get; set; }
    }
}
