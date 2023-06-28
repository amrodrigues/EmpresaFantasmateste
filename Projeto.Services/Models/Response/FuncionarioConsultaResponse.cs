using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Services.Models.Response
{
    public class FuncionarioConsultaResponse
    {
        public int IdFuncionario { get; set; }
        public string Nome { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataAdmissao { get; set; }
    }
}