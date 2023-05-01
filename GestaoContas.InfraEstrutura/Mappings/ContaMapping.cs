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
    public class ContaMapping : IEntityTypeConfiguration<Conta>
    {
        //metodo para realizar o mapeamento da entidade
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            //criando o nome da tabela ou acessando o nome da tabela
            builder.ToTable("CONTA");

            //chave primaria da tabela
            builder.HasKey(c => c.IdConta);

            //mapeamento dos campos da tabela
            builder.Property(c => c.IdConta).HasColumnName("IDCONTA").IsRequired();
            builder.Property(c => c.Nome).HasColumnName("NOME").HasMaxLength(150).IsRequired();
            builder.Property(c => c.Data).HasColumnName("DATA").IsRequired();
            builder.Property(c => c.Valor).HasColumnName("VALOR").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(c => c.Tipo).HasColumnName("TIPO").IsRequired();
        }
    }
}
