using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Projeto.Services.Models.Request;
using Projeto.Services.Models.Response;
using Projeto.Business.Contracts;
using Projeto.Entities;
using System.Web.Http.ModelBinding;

namespace Projeto.Services.Controllers
{
    [RoutePrefix("api/funcionario")]
    public class FuncionarioController : ApiController
    {
        //atributo..
        private IFuncionarioBusiness business;

        //construtor para injeção de dependencia..
        public FuncionarioController(IFuncionarioBusiness business)
        {
            this.business = business;
        }

        [Route("cadastrar")] //URL: api/funcionario/cadastrar
        [HttpPost] //requisição do tipo POST
        public HttpResponseMessage Cadastrar(FuncionarioCadastroRequest request)
        {
            var response = new FuncionarioCadastroResponse();

            if (ModelState.IsValid) //se passou nas validações..
            {
                try
                {
                    //obter dados do request..
                    Funcionario f = new Funcionario();
                    f.Nome = request.Nome;
                    f.Salario = request.Salario;
                    f.DataAdmissao = request.DataAdmissao;

                    business.Cadastrar(f); //gravando..

                    response.Mensagem = $"Funcionário {request.Nome}, cadastrado com sucesso.";
                    //retornar STATUS de sucesso (HTTP 200)
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                catch(Exception e)
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

        [Route("atualizar")] //URL: api/funcionario/atualizar
        [HttpPut] //requisição do tipo PUT
        public HttpResponseMessage Atualizar(FuncionarioEdicaoRequest request)
        {
            var response = new FuncionarioEdicaoResponse();

            if (ModelState.IsValid) //se passou nas validações..
            {
                try
                {
                    //obter dados do request..
                    Funcionario f = new Funcionario();
                    f.IdFuncionario = request.IdFuncionario;
                    f.Nome = request.Nome;
                    f.Salario = request.Salario;
                    f.DataAdmissao = request.DataAdmissao;

                    business.Atualizar(f); //gravando..

                    response.Mensagem = $"Funcionário {request.Nome}, atualizado com sucesso.";
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

        [Route("excluir")] //URL: api/funcionario/excluir
        [HttpDelete] //requisição do tipo DELETE
        public HttpResponseMessage Excluir(int id)
        {
            var response = new FuncionarioExclusaoResponse();

            if (ModelState.IsValid) //se passou nas validações..
            {
                try
                {
                    //buscar o funcionario pelo id..
                    Funcionario f = business.ConsultarPorId(id);
                    //excluindo..
                    business.Excluir(f);

                    response.Mensagem = $"Funcionário {f.Nome}, excluido com sucesso.";
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

        [Route("consultar")] //api/funcionario/consultar
        [HttpGet] //verbo HTTP GET..
        public HttpResponseMessage ConsultarTodos()
        {
            List<FuncionarioConsultaResponse> lista = new List<FuncionarioConsultaResponse>();

            try
            {
                //varrer os funcionarios obtidos no banco de dados..
                foreach(Funcionario f in business.ConsultarTodos())
                {
                    FuncionarioConsultaResponse response = new FuncionarioConsultaResponse();
                    response.IdFuncionario = f.IdFuncionario;
                    response.Nome = f.Nome;
                    response.Salario = f.Salario;
                    response.DataAdmissao = f.DataAdmissao;

                    lista.Add(response);
                }

                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch(Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, lista);
            }
        }

        [Route("consultarporid")] //api/funcionario/consultarporid
        [HttpGet] //verbo HTTP GET..
        public HttpResponseMessage ConsultarPorId(int id)
        {
            FuncionarioConsultaResponse model = new FuncionarioConsultaResponse();

            try
            {
                //buscando o funcionário pelo id..
                Funcionario f = business.ConsultarPorId(id);

                model.IdFuncionario = f.IdFuncionario;
                model.Nome = f.Nome;
                model.Salario = f.Salario;
                model.DataAdmissao = f.DataAdmissao;

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
