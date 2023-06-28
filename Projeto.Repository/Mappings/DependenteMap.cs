using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Projeto.Repository.Mappings
{
    public class DependenteMap : EntityTypeConfiguration<Dependente>
    {
        public DependenteMap()
        {
            //nome da tabela..
            ToTable("Dependente");

            //chave primária..
            HasKey(d => d.IdDependente);

            //mapeando os campos..
            Property(d => d.IdDependente)
                .HasColumnName("IdDependente")
                .IsRequired();

            Property(d => d.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(50)
                .IsRequired();

            Property(d => d.DataNascimento)
                .HasColumnName("DataNascimento")
                .IsRequired();

            Property(d => d.IdFuncionario)
                .HasColumnName("IdFuncionario")
                .IsRequired();

            //mapear o relacionamento..
            HasRequired(d => d.Funcionario) //Dependente TEM 1 Funcionario
                .WithMany(f => f.Dependentes) //Funcionario TEM Muitos Dependentes
                .HasForeignKey(d => d.IdFuncionario); //chave estrangeira
        }
    }
}
