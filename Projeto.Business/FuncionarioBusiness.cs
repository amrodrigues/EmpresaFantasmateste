using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using Projeto.Business.Contracts;
using Projeto.Repository.Contracts;

namespace Projeto.Business
{
    public class FuncionarioBusiness 
        : BaseBusiness<Funcionario>, IFuncionarioBusiness
    {
        //atributo..
        private IFuncionarioRepository repository;

        //construtor com entrada de argumentos..
        public FuncionarioBusiness(IFuncionarioRepository repository)
            : base(repository)
        {
            this.repository = repository;
        }

        public List<Funcionario> ConsultarPorNome(string nome)
        {
            return repository.FindByNome(nome);
        }
    }
}
