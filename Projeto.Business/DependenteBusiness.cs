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
    public class DependenteBusiness
        : BaseBusiness<Dependente> , IDependenteBusiness
    {
        //atributo..
        private IDependenteRepository repository;

        public DependenteBusiness(IDependenteRepository repository)
            : base(repository)
        {
            this.repository = repository;
        }

        //sobrescrever o método de cadastro do Dependente..
        public override void Cadastrar(Dependente obj)
        {
            //verificar se o dependente é menor de idade..
            DateTime hoje = DateTime.Now;
            int idade = hoje.Year - obj.DataNascimento.Year;

            //calculando a idade..
            if (obj.DataNascimento > hoje.AddYears(-idade)) idade--;

            if(idade < 18)
            {
                repository.Insert(obj);
            }
            else
            {
                throw new Exception("Dependente não pode ser maior de idade.");
            }
        }
    }
}
