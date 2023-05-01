using GestaoContas.Dominio.Entities;
using GestaoContas.Dominio.Interfaces.Repositories;
using GestaoContas.InfraEstrutura.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContas.InfraEstrutura.Repositories
{
    public class ContaRepository : IContaRepository
    {
        public void CreateConta(Conta conta)
        {
            using (var sqlServerContext = new SqlServerContext())
            {
                sqlServerContext.Add(conta);
                sqlServerContext.SaveChanges();
            }
        }

        public void DeleteConta(Conta conta)
        {
            using (var sqlServerContext = new SqlServerContext())
            {
                sqlServerContext.Conta.Remove(conta);
                sqlServerContext.SaveChanges();
            }
        }

        public List<Conta> GetAllTipo(DateTime dataMin, DateTime dataMax, TipoConta operacao)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realiza a consulta no banco de dados
                return sqlServerContext.Conta.Where(c => c.Data >= dataMin && 
                                                         c.Data <= dataMax &&
                                                         c.Tipo == operacao).OrderByDescending(c => c.Data).ToList();
            }
        }

        public List<Conta> GetAllContas(DateTime dataMin, DateTime dataMax)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realiza a consulta no banco de dados
                return sqlServerContext.Conta.Where(c => c.Data >= dataMin &&
                                                         c.Data <= dataMax).OrderByDescending(c => c.Data).ToList();
            }
        }

        public Conta GetById(Guid idConta)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realiza a consulta no banco de dados
                return sqlServerContext.Conta.FirstOrDefault(c => c.IdConta.Equals(idConta));
            }
        }

        public void UpdateConta(Conta conta)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realizando o update no banco
                sqlServerContext.Conta.Entry(conta).State = EntityState.Modified;
                sqlServerContext.SaveChanges();
            }
        }

        public void CreatePesquisa(Pesquisa pesquisa)
        {
            using (var sqlServerContext = new SqlServerContext())
            {
                sqlServerContext.Add(pesquisa);
                sqlServerContext.SaveChanges();
            }
        }

        public void DeletePesquisa(Pesquisa pesquisa)
        {
            using (var sqlServerContext = new SqlServerContext())
            {
                sqlServerContext.Pesquisa.Remove(pesquisa);
                sqlServerContext.SaveChanges();
            }
        }

        public Pesquisa GetByPesquisa()
        {
            using (var sqlServerContext = new SqlServerContext())
            {
                //realiza a consulta no banco de dados
                return sqlServerContext.Pesquisa.FirstOrDefault();
            }
        }

        public void UpdatePesquisa(Pesquisa pesquisa)
        {
            using (var sqlServerContext = new SqlServerContext())
            {
                //realizando o update no banco
                sqlServerContext.Pesquisa.Entry(pesquisa).State = EntityState.Modified;
                sqlServerContext.SaveChanges();
            }
        }
    }
}
