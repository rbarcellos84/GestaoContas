using GestaoContas.Aplicacao.Models;
using GestaoContas.Dominio.Entities;
using GestaoContas.Dominio.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoContas.Aplicacao.Controllers
{
    public class ContasController : Controller
    {
        private readonly IContaDominioService _contaDominioService;

        public ContasController(IContaDominioService contaDominioService)
        {
            _contaDominioService = contaDominioService;
        }

        [HttpPost]
        public IActionResult Home(ContasDashboardModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contas = _contaDominioService.ConsultarTodasContas(DateTime.Parse(model.DataInicio), DateTime.Parse(model.DataFim));
                    TempData["ToatalContasReceber"] = contas.Where(c => c.Tipo == TipoConta.Receber).Sum(c => c.Valor);
                    TempData["ToatalContasPagar"] = contas.Where(c => c.Tipo == TipoConta.Pagar).Sum(c => c.Valor);
                }
                
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View("Home", model);
        }
        public IActionResult Home()
        {
            ContasDashboardModel model = new ContasDashboardModel();

            try
            {
                model.DataInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
                model.DataFim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToString("yyyy-MM-dd");
                var contas = _contaDominioService.ConsultarTodasContas(DateTime.Parse(model.DataInicio), DateTime.Parse(model.DataFim));
                TempData["ToatalContasReceber"] = contas.Where(c => c.Tipo == TipoConta.Receber).Sum(c => c.Valor);
                TempData["ToatalContasPagar"] = contas.Where(c => c.Tipo == TipoConta.Pagar).Sum(c => c.Valor);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            
            return View("Home",model);
        }

        [HttpPost]
        public IActionResult Cadastro(ContasCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Conta conta = new Conta();
                    conta.IdConta = Guid.NewGuid();
                    conta.Nome = model.Nome;
                    conta.Data = DateTime.Parse(model.Data);
                    conta.Valor = decimal.Parse(model.Valor);

                    if (model.Tipo.Equals("Receber"))
                        conta.Tipo = TipoConta.Receber;
                    else
                        conta.Tipo = TipoConta.Pagar;

                    _contaDominioService.CadastrarConta(conta);
                    TempData["MensagemSucesso"] = $"Conta {conta.Nome} cadastrada com sucesso!";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            return View();
        }
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Consulta(ContasConsultaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TipoConta operacao;
                    if (model.Conta.Equals("Receber"))
                        operacao = TipoConta.Receber;
                    else
                        operacao = TipoConta.Pagar;

                    var contas = _contaDominioService.ConsultarContas(DateTime.Parse(model.DataInicio), DateTime.Parse(model.DataFim), operacao);

                    if (contas.Count == 0)
                    {
                        TempData["MensagemErro"] = "Não foi encontrado resultado para filtro escolhido.";
                    }

                    Pesquisa pesquisa = new Pesquisa();
                    pesquisa.IdPesquisa = Guid.NewGuid();
                    pesquisa.DataInicio = model.DataInicio;
                    pesquisa.DataFim = model.DataFim;
                    pesquisa.TipoConsulta = "Tela";
                    pesquisa.Formato = model.Formato;
                    pesquisa.Conta = model.Conta;
                    _contaDominioService.CadastrarPesquisa(pesquisa);

                    if (model.TipoConsulta.Equals("Tela"))
                    {
                        ModelState.Clear();
                        model.ContasRelatorioModel(contas);
                        model.DataInicio = pesquisa.DataInicio;
                        model.DataFim = pesquisa.DataFim;
                        model.TipoConsulta = pesquisa.TipoConsulta;
                        model.Formato = pesquisa.Formato;
                        model.Conta = pesquisa.Conta;
                        return View("Consulta", model);
                    }
                    else
                    {
                        var nomeArquivo = $"Conta_{DateTime.Now.ToString("ddMMyyyyHHmmss")}";
                        var tipoArquivo = string.Empty;
                        byte[] arquivo = null;

                        if (model.Formato.Equals("Excel"))
                        {
                            nomeArquivo = nomeArquivo + ".xlsx";
                            tipoArquivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            arquivo = _contaDominioService.GerarReatorioExcel(contas);
                        }
                        else
                        {
                            nomeArquivo = nomeArquivo + ".pdf";
                            tipoArquivo = "application/pdf";
                            arquivo = _contaDominioService.GerarReatorioPdf(contas);
                        }

                        return File(arquivo, tipoArquivo, nomeArquivo);
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            return View();
        }
        public IActionResult Consulta(string? value)
        {
            if (value != null)
            {
                try
                {
                    Pesquisa pesquisa = new Pesquisa();
                    pesquisa = _contaDominioService.ObterPesquisa();
                    if (pesquisa != null)
                    {
                        TipoConta operacao;
                        if (pesquisa.Conta.Equals("Receber"))
                            operacao = TipoConta.Receber;
                        else
                            operacao = TipoConta.Pagar;

                        var dataMin = DateTime.Parse(pesquisa.DataInicio);
                        var dataMax = DateTime.Parse(pesquisa.DataFim);
                        var contas = _contaDominioService.ConsultarContas(dataMin, dataMax, operacao);
                        ContasConsultaModel model = new ContasConsultaModel();
                        model.Conta = pesquisa.Conta;
                        model.Formato = pesquisa.Formato;
                        model.TipoConsulta = pesquisa.TipoConsulta;
                        model.DataInicio = pesquisa.DataInicio;
                        model.DataFim = pesquisa.DataFim;
                        model.ContasRelatorioModel(contas);
                        //return RedirectToAction("Consulta", "Contas", model);
                        return View("Consulta", model);
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            var resposta = new ContasConsultaModel();
            resposta.ContasRelatorioModel(null);
            return View("Consulta", resposta);
        }

        [HttpPost]
        public IActionResult Edicao(ContasEdicaoModel model)
        {
            try
            {
                Conta conta = new Conta();
                conta = _contaDominioService.ObterConta(model.IdConta);
                if (conta != null)
                {
                    conta.Nome = model.Nome;
                    conta.Data = DateTime.Parse(model.Data);
                    conta.Valor = decimal.Parse(model.Valor);

                    if (model.Tipo.Equals("Receber"))
                        conta.Tipo = TipoConta.Receber;
                    else
                        conta.Tipo = TipoConta.Pagar;

                    _contaDominioService.AtualizarConta(conta);
                    TempData["MensagemSucesso"] = $"Conta {model.Nome} atualizada com sucesso!";
                    return View("Edicao", model);
                }

                TempData["MensagemSucesso"] = $"Conta {model.Nome} não encontrada!";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View("Edicao", model);
        }
        public IActionResult Edicao(Guid id)
        {
            try
            {
                ContasEdicaoModel model = new ContasEdicaoModel();
                var conta = _contaDominioService.ObterConta(id);
                if (conta != null)
                {
                    model.IdConta = conta.IdConta;
                    model.Nome = conta.Nome;
                    model.Data = conta.Data.ToString("yyyy-MM-dd");
                    model.Valor = conta.Valor.ToString();

                    if (conta.Tipo == TipoConta.Receber)
                        model.Tipo = "Receber";
                    else
                        model.Tipo = "Pagar";

                    return View("Edicao",model);
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Excluir(ContasExcluirModel model)
        {
            try
            {
                Conta conta = new Conta();
                conta = _contaDominioService.ObterConta(model.IdConta);
                if (conta != null)
                {
                    _contaDominioService.ExcluirConta(model.IdConta);
                    TempData["MensagemSucesso"] = $"Conta {model.Nome} excluida com sucesso!";
                    ModelState.Clear();
                    return View("Excluir");
                }

                TempData["MensagemSucesso"] = $"Conta {model.Nome} não encontrada!";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            
            return View("Excluir",model);
        }
        public IActionResult Excluir(Guid id)
        {
            try
            {
                ContasExcluirModel model = new ContasExcluirModel();
                var conta = _contaDominioService.ObterConta(id);
                if (conta != null)
                {
                    model.IdConta = conta.IdConta;
                    model.Nome = conta.Nome;
                    model.Data = conta.Data.ToString("yyyy-MM-dd");
                    model.Valor = conta.Valor.ToString();

                    if (conta.Tipo == TipoConta.Receber)
                        model.Tipo = "Receber";
                    else
                        model.Tipo = "Pagar";

                    TempData["MensagemErro"] = $"Você realmente deseja excluir o registro {model.Nome}?";
                    return View("Excluir", model);
                }

                TempData["MensagemErro"] = $"Registro não encontrado.";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            return View();
        }
    }
}
