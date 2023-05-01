using GestaoContas.Dominio.Entities;
using GestaoContas.Dominio.Interfaces.Reports;
using GestaoContas.Dominio.Interfaces.Repositories;
using GestaoContas.Dominio.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GestaoContas.Dominio.Services
{
    public class ContaDominioService : IContaDominioService
    {
        //atributo
        private readonly IContaRepository _contaRepository;
        private readonly IContaReports _contaReport;

        //metodo construtor para iniciar os atributos (injeção de dependencia)
        public ContaDominioService(IContaRepository contaRepository, IContaReports contaReport)
        {
            _contaRepository = contaRepository;
            _contaReport = contaReport;
        }

        public void AtualizarConta(Conta conta)
        {
            if (_contaRepository.GetById(conta.IdConta) == null)
            {
                throw new Exception("Conta não encontrada.");
            }
            _contaRepository.UpdateConta(conta);
        }

        public void CadastrarConta(Conta conta)
        {
            var dataAtual = DateTime.Now;
            if (conta.Data < new DateTime(dataAtual.Year,dataAtual.Month,dataAtual.Day,0,0,0))
            {
                throw new Exception("O sistema não pode cadastrar contas com data retroativa.");
            }
            _contaRepository.CreateConta(conta);
        }

        public List<Conta> ConsultarContas(DateTime dataMin, DateTime dataMax, TipoConta operacao)
        {
            if (dataMin > dataMax)
            {
                throw new Exception("A data inicial tem que ser menor ou igual a data final.");
            }

            return _contaRepository.GetAllTipo(dataMin, dataMax, operacao);
        }

        public List<Conta> ConsultarTodasContas(DateTime dataMin, DateTime dataMax)
        {
            if (dataMin > dataMax)
            {
                throw new Exception("A data inicial tem que ser menor ou igual a data final.");
            }

            return _contaRepository.GetAllContas(dataMin, dataMax);
        }

        public void ExcluirConta(Guid idConta)
        {
            var conta = _contaRepository.GetById(idConta);
            if (conta == null)
            {
                throw new Exception("Conta não encontrada.");
            }
            _contaRepository.DeleteConta(conta);
        }

        public Conta ObterConta(Guid idConta)
        {
            var conta = _contaRepository.GetById(idConta);
            if (conta == null)
            {
                throw new Exception("Conta não encontrada.");
            }
            return conta;
        }

        public byte[] GerarReatorioExcel(List<Conta> conta)
        {
            if (conta.Count == 0 || conta == null)
            {
                throw new Exception("Não ha dados para geração de relatorio excel.");
            }
            return _contaReport.CreateExcel(conta);
        }

        public byte[] GerarReatorioPdf(List<Conta> conta)
        {
            if (conta.Count == 0 || conta == null)
            {
                throw new Exception("Não ha dados para geração de relatorio pdf.");
            }
            return _contaReport.CreatePdf(conta);
        }

        public void AtualizarPesquisa(Pesquisa dados)
        {
            var pesquisa = _contaRepository.GetByPesquisa();
            if (pesquisa == null)
            {
                throw new Exception("Sem historico de consulta.");
            }
            pesquisa.DataInicio = dados.DataInicio;
            pesquisa.DataFim = dados.DataFim;
            pesquisa.Formato = dados.Formato;
            pesquisa.TipoConsulta = dados.TipoConsulta;
            pesquisa.Conta = dados.Conta;
            _contaRepository.UpdatePesquisa(pesquisa);
        }

        public void CadastrarPesquisa(Pesquisa dados)
        {
            var pesquisa = _contaRepository.GetByPesquisa();
            if (pesquisa == null)
            {
                _contaRepository.CreatePesquisa(dados);
            }
            else
            {
                pesquisa.DataInicio = dados.DataInicio;
                pesquisa.DataFim = dados.DataFim;
                pesquisa.Formato = dados.Formato;
                pesquisa.TipoConsulta = dados.TipoConsulta;
                pesquisa.Conta = dados.Conta;
                _contaRepository.UpdatePesquisa(pesquisa);
            }
        }

        public void ExcluirPesquisa()
        {
            var pesquisa = _contaRepository.GetByPesquisa();
            if (pesquisa == null)
            {
                throw new Exception("Sem historico de consulta.");
            }
            _contaRepository.DeletePesquisa(pesquisa);
        }

        public Pesquisa ObterPesquisa()
        {
            var pesquisa = _contaRepository.GetByPesquisa();
            if (pesquisa == null)
            {
                throw new Exception("Sem historico de consulta.");
            }
            return pesquisa;
        }
    }
}
