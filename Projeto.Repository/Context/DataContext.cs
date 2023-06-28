using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity;
using Projeto.Entities;
using Projeto.Repository.Mappings;

namespace Projeto.Repository.Context
{
    //Regra 1) Herdar DbContext
    public class DataContext : DbContext
    {
        //Regra 2) Construtor que envie para a superclasse (DbContext)
        //o caminho da connectionstring do banco de dados..
        public DataContext()
            : base(ConfigurationManager.ConnectionStrings["aula"].ConnectionString)
        {

        }

        //Regra 3) Sobrescrever o método OnModelCreating..
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //incluir cada classe de mapeamento..
            modelBuilder.Configurations.Add(new DependenteMap());
            modelBuilder.Configurations.Add(new FuncionarioMap());
        }

        //Regra 4) Declarar uma propriedade DbSet para cada entidade
        public DbSet<Dependente> Dependente { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
    }
}
