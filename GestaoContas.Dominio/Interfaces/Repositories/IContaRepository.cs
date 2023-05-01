using GestaoContas.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContas.Dominio.Interfaces.Repositories
{
    public interface IContaRepository
    {
        //atributos para o gerenciamento de uma conta ----------------------------------
        void CreateConta(Conta conta);
        void UpdateConta(Conta conta);
        void DeleteConta(Conta conta);
        List<Conta> GetAllTipo(DateTime dataMin, DateTime dataMax, TipoConta operacao);
        List<Conta> GetAllContas(DateTime dataMin, DateTime dataMax);
        Conta GetById(Guid idConta);
        //atributos para o gerenciamento de uma conta ----------------------------------

        //historico de pesquisa da tela de consulta ------------------------------------
        void CreatePesquisa(Pesquisa pesquisa);
        void UpdatePesquisa(Pesquisa pesquisa);
        void DeletePesquisa(Pesquisa pesquisa);
        Pesquisa GetByPesquisa();
        //historico de pesquisa da tela de consulta ------------------------------------
    }
}
