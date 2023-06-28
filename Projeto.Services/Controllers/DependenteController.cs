using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Projeto.Business.Contracts;
using Projeto.Services.Models.Request;
using Projeto.Services.Models.Response;
using Projeto.Entities;
using System.Web.Http.ModelBinding;

namespace Projeto.Services.Controllers
{
    [RoutePrefix("api/dependente")]
    public class DependenteController : ApiController
    {
        //atributo..
        private IDependenteBusiness business;

        //construtor para injeção de dependencia..
        public DependenteController(IDependenteBusiness business)
        {
            this.business = business;
        }

        [Route("cadastrar")] //URL: api/dependente/cadastrar
        [HttpPost] //requisição do tipo POST
        public HttpResponseMessage Cadastrar(DependenteCadastroRequest request)
        {
            var response = new DependenteCadastroResponse();

            if (ModelState.IsValid) //se passou nas validações..
            {
                try
                {
                    //obter dados do request..
                    Dependente d = new Dependente();
                    d.Nome = request.Nome;
                    d.DataNascimento = request.DataNascimento;
                    d.IdFuncionario = request.IdFuncionario;

                    business.Cadastrar(d); //gravando..

                    response.Mensagem = $"Dependente {request.Nome}, cadastrado com sucesso.";
                    //retornar STATUS de sucesso (HTTP 200)
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                catch (Exception e)
                {
                    response.Mensagem = e.Message;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                }
            }
            else
            {
                response.Mensagem = ObterMensagensDeValidacao(ModelState);
                //retornar STATUS de erro (HTTP 400)
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [Route("atualizar")] //URL: api/dependente/atualizar
        [HttpPut] //requisição do tipo PUT
        public HttpResponseMessage Atualizar(DependenteEdicaoRequest request)
        {
            var response = new DependenteEdicaoResponse();

            if (ModelState.IsValid) //se passou nas validações..
            {
                try
                {
                    //obter dados do request..
                    Dependente d = new Dependente();
                    d.IdDependente = request.IdDependente;
                    d.Nome = request.Nome;
                    d.DataNascimento = request.DataNascimento;
                    d.IdFuncionario = request.IdFuncionario;

                    business.Atualizar(d); //gravando..

                    response.Mensagem = $"Dependente {request.Nome}, atualizado com sucesso.";
                    //retornar STATUS de sucesso (HTTP 200)
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                catch (Exception e)
                {
                    response.Mensagem = e.Message;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                }
            }
            else
            {
                response.Mensagem = ObterMensagensDeValidacao(ModelState);
                //retornar STATUS de erro (HTTP 400)
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [Route("excluir")] //URL: api/dependente/excluir
        [HttpDelete] //requisição do tipo DELETE
        public HttpResponseMessage Excluir(int id)
        {
            var response = new DependenteExclusaoResponse();

            if (ModelState.IsValid) //se passou nas validações..
            {
                try
                {
                    //buscar o funcionario pelo id..
                    Dependente d = business.ConsultarPorId(id);
                    //excluindo..
                    business.Excluir(d);

                    response.Mensagem = $"Dependente {d.Nome}, excluido com sucesso.";
                    //retornar STATUS de sucesso (HTTP 200)
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                catch (Exception e)
                {
                    response.Mensagem = e.Message;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                }
            }
            else
            {
                response.Mensagem = ObterMensagensDeValidacao(ModelState);
                //retornar STATUS de erro (HTTP 400)
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [Route("consultar")] //api/dependente/consultar
        [HttpGet] //verbo HTTP GET..
        public HttpResponseMessage ConsultarTodos()
        {
            List<DependenteConsultaResponse> lista = new List<DependenteConsultaResponse>();

            try
            {
                //varrer os funcionarios obtidos no banco de dados..
                foreach (Dependente d in business.ConsultarTodos())
                {
                    DependenteConsultaResponse response = new DependenteConsultaResponse();
                    response.Funcionario = new FuncionarioConsultaResponse();

                    response.IdDependente = d.IdDependente;
                    response.Nome = d.Nome;
                    response.DataNascimento = d.DataNascimento;

                    response.Funcionario.IdFuncionario = d.Funcionario.IdFuncionario;
                    response.Funcionario.Nome = d.Funcionario.Nome;
                    response.Funcionario.Salario = d.Funcionario.Salario;
                    response.Funcionario.DataAdmissao = d.Funcionario.DataAdmissao;

                    lista.Add(response);
                }

                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, lista);
            }
        }

        [Route("consultarporid")] //api/dependente/consultarporid
        [HttpGet] //verbo HTTP GET..
        public HttpResponseMessage ConsultarPorId(int id)
        {
            DependenteConsultaResponse model = new DependenteConsultaResponse();
            model.Funcionario = new FuncionarioConsultaResponse();

            try
            {
                //buscando o funcionário pelo id..
                Dependente d = business.ConsultarPorId(id);

                model.IdDependente = d.IdDependente;
                model.Nome = d.Nome;
                model.DataNascimento = d.DataNascimento;

                model.Funcionario.IdFuncionario = d.Funcionario.IdFuncionario;
                model.Funcionario.Nome = d.Funcionario.Nome;
                model.Funcionario.Salario = d.Funcionario.Salario;
                model.Funcionario.DataAdmissao = d.Funcionario.DataAdmissao;

                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, model);
            }
        }

        private string ObterMensagensDeValidacao(ModelStateDictionary model)
        {
            List<string> erros = new List<string>();

            foreach (var m in model)
            {
                if (m.Value.Errors.Count > 0)
                {
                    erros.Add(m.Value.Errors.Select(s => s.ErrorMessage).FirstOrDefault());
                }
            }

            return string.Join(",", erros);
        }
    }
}
