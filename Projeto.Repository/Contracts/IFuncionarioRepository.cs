using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities; //importando..

namespace Projeto.Repository.Contracts
{
    public interface IFuncionarioRepository
        : IBaseRepository<Funcionario>
    {
        List<Funcionario> FindByNome(string nome);
    }
}
