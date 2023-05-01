using GestaoContas.Dominio.Entities;
using GestaoContas.InfraEstrutura.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContas.InfraEstrutura.Contexts
{
    public class SqlServerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string de conexao com o banco de dados
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=BdSistemaConta;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adiciona cada classe de mapeamento do projeto
            modelBuilder.ApplyConfiguration(new ContaMapping());
            modelBuilder.ApplyConfiguration(new PesquisaMapping());
        }
        public DbSet<Conta> Conta { get; set; }
        public DbSet<Pesquisa> Pesquisa { get; set; }

        ///<summary>
        ///PM> Add-Migration Inicial
        ///PM> Build started...
        ///PM> Build succeeded.
        ///PM> To undo this action, use Remove-Migration.
        ///PM> Update-Database
        ///PM> Build started...
        ///PM> Build succeeded.
        ///PM> Applying migration '20230424055138_Initial'.
        ///PM> Done.
        ///</summary>
    }
}
