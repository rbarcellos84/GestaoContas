namespace GestaoContas.Aplicacao.Models
{
    public class ContasExcluirModel
    {
        public Guid IdConta { get; set; }
        public string Nome { get; set; }
        public string Data { get; set; }
        public string Valor { get; set; }
        public string Tipo { get; set; }
    }
}
