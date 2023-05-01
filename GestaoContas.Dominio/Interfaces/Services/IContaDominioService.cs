using GestaoContas.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContas.Dominio.Interfaces.Services
{
    public interface IContaDominioService
    {
        //atributos para o gerenciamento de uma conta ----------------------------------
        void CadastrarConta(Conta conta);
        void AtualizarConta(Conta conta);
        void ExcluirConta(Guid idConta);
        List<Conta> ConsultarContas(DateTime dataMin, DateTime dataMax, TipoConta operacao);
        List<Conta> ConsultarTodasContas(DateTime dataMin, DateTime dataMax);
        Conta ObterConta(Guid idConta);
        //atributos para o gerenciamento de uma conta ----------------------------------

        //geracao de relatorio da tela de consulta -------------------------------------
        byte[] GerarReatorioExcel(List<Conta> conta);
        byte[] GerarReatorioPdf(List<Conta> conta);
        //geracao de relatorio da tela de consulta -------------------------------------

        //historico de pesquisa da tela de consulta ------------------------------------
        void CadastrarPesquisa(Pesquisa pesquisa);
        void AtualizarPesquisa(Pesquisa pesquisa);
        void ExcluirPesquisa();
        Pesquisa ObterPesquisa();
        //historico de pesquisa da tela de consulta ------------------------------------
    }
}
