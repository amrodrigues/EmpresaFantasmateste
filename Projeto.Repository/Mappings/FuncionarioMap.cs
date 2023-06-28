using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Projeto.Repository.Mappings
{
    //classe de mapeamento para a entidade Funcionario..
    public class FuncionarioMap : EntityTypeConfiguration<Funcionario>
    {
        //construtor
        public FuncionarioMap()
        {
            //nome da tabela..
            ToTable("Funcionario");

            //chave primária..
            HasKey(f => f.IdFuncionario);

            //mapeando as propriedades..
            Property(f => f.IdFuncionario)
                .HasColumnName("IdFuncionario")
                .IsRequired();

            Property(f => f.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(50)
                .IsRequired();

            Property(f => f.Salario)
                .HasColumnName("Salario")
                .HasPrecision(18,2)
                .IsRequired();

            Property(f => f.DataAdmissao)
               .HasColumnName("DataAdmissao")
               .IsRequired();
        }
    }
}
