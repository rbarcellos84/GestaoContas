using GestaoContas.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContas.InfraEstrutura.Mappings
{
    public class PesquisaMapping : IEntityTypeConfiguration<Pesquisa>
    {
        //metodo para realizar o mapeamento da entidade
        public void Configure(EntityTypeBuilder<Pesquisa> builder)
        {
            //criando o nome da tabela ou acessando o nome da tabela
            builder.ToTable("PESQUISA");

            //chave primaria da tabela
            builder.HasKey(p => p.IdPesquisa);

            //mapeamento dos campos da tabela
            builder.Property(p => p.IdPesquisa).HasColumnName("IDPESQUISA").IsRequired();
            builder.Property(p => p.DataInicio).HasColumnName("DATAINICIO").HasMaxLength(50).IsRequired();
            builder.Property(p => p.DataFim).HasColumnName("DATAFIM").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Conta).HasColumnName("CONTA").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Formato).HasColumnName("FORMATO").HasMaxLength(50).IsRequired();
            builder.Property(p => p.TipoConsulta).HasColumnName("TIPOCONSULTA").HasMaxLength(50).IsRequired();
        }
    }
}
