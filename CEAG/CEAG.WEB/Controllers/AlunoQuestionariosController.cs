using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.REPOSITORIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.AlunoQuestionario;
using CEAG.WEB.ViewModel.Questionario;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class AlunoQuestionariosController : Controller
    {
        private RepositorioAlunoQuestionario _repositorioAlunoQuestionario = new RepositorioAlunoQuestionario(new CeagDbContext());
        private IRepositorioGenerico<Aluno, int> _repositorioGenericoAluno = new RepositorioAluno(new CeagDbContext());
        private RepositorioAlunoMatricula _repositorioGenericoAlunoMatricula = new RepositorioAlunoMatricula(new CeagDbContext());

        public ActionResult ValidarSePossuiFicha(int codAluno)
        {
            
            var alunoConsulta = _repositorioGenericoAluno.SelecionarPorId(codAluno);
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != alunoConsulta.CodEscola)
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            var alunoQuestionarioBanco = _repositorioAlunoQuestionario.SelecionarPorIdAluno(codAluno);
            if (alunoQuestionarioBanco.Any())
            {
                var alunoMatriculaBanco = _repositorioGenericoAlunoMatricula.SelecionarPorIdAluno(codAluno).OrderByDescending(p => p.Ano).Take(1).SingleOrDefault();
                return RedirectToAction("EmitirFichaAluno", "Reports", new { codAlunoMatricula = alunoMatriculaBanco.CodAlunoMatricula });
            }
            return RedirectToAction("Create", "AlunoQuestionarios", new { codAluno });
        }

        // GET: AlunoQuestionarios/Create
        public ActionResult Create(int codAluno)
        {
            
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            var alunoConsulta = _repositorioGenericoAluno.SelecionarPorId(codAluno);
            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(alunoConsulta);
            List<AlunoQuestionarioViewModel> listaAlunoQuestionarioViewModel = new List<AlunoQuestionarioViewModel>();
            List<Questionario> questionarioBanco = _repositorioAlunoQuestionario.SelecionarQuestionarioEmBranco(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            var alunoQuestionarioBanco = _repositorioAlunoQuestionario.SelecionarPorIdAluno(codAluno);

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != alunoConsulta.CodEscola)
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }


            if (alunoQuestionarioBanco.Any())
            {
                foreach (var item in alunoQuestionarioBanco)
                {
                    AlunoQuestionarioViewModel alunoQuestionarioviewModel = new AlunoQuestionarioViewModel();
                    alunoQuestionarioviewModel.Resposta = item.Resposta;
                    alunoQuestionarioviewModel.Complemento = item.Complemento;
                    alunoQuestionarioviewModel.Inclusao = item.Inclusao;
                    alunoQuestionarioviewModel.CodQuestionario = item.CodQuestionario;
                    alunoQuestionarioviewModel.CodAluno = item.CodAluno;
                    alunoQuestionarioviewModel.CodAlunoQuestionario = item.CodAlunoQuestionario;
                    alunoQuestionarioviewModel.QuestionarioExibicaoViewModel = mapper.Map<QuestionarioExibicaoViewModel>(item.Questionario);

                    listaAlunoQuestionarioViewModel.Add(alunoQuestionarioviewModel);
                }
            }
            else
            {
                foreach (var itemQuestionario in questionarioBanco)
                {
                    AlunoQuestionarioViewModel alunoQuestionarioviewModel = new AlunoQuestionarioViewModel();
                    alunoQuestionarioviewModel.Resposta = "NÃO";
                    alunoQuestionarioviewModel.Complemento = "";
                    alunoQuestionarioviewModel.CodQuestionario = itemQuestionario.CodQuestionario;
                    alunoQuestionarioviewModel.CodAluno = aluno.CodAluno;
                    alunoQuestionarioviewModel.QuestionarioExibicaoViewModel = mapper.Map<QuestionarioExibicaoViewModel>(itemQuestionario);

                    listaAlunoQuestionarioViewModel.Add(alunoQuestionarioviewModel);
                }
            }
                                 
            AlunoQuestionarioComLista alunoQuestionarioComLista = new AlunoQuestionarioComLista
            {
                AlunoQuestionarioViewModels = listaAlunoQuestionarioViewModel,
                CodAluno = codAluno
            };
            ViewBag.QuantidadeRegistros = alunoQuestionarioComLista.AlunoQuestionarioViewModels.Count();
            ViewBag.Aluno = aluno;
            CarregarComboTela();
            return View(alunoQuestionarioComLista);
        }

        // POST: AlunoQuestionarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlunoQuestionarioComLista alunoQuestionarioComLista)
        {
            
            foreach (var item in alunoQuestionarioComLista.AlunoQuestionarioViewModels)
            {
                AlunoQuestionario alunoQuestionario;
                if (item.CodAlunoQuestionario > 0)
                {
                    alunoQuestionario = _repositorioAlunoQuestionario.SelecionarPorId(item.CodAlunoQuestionario);
                }
                else
                {
                    alunoQuestionario = new AlunoQuestionario();
                }

                alunoQuestionario.Inclusao = DateTime.Now;
                alunoQuestionario.Resposta = item.Resposta;
                alunoQuestionario.Complemento = item.Complemento;
                alunoQuestionario.CodAluno = item.CodAluno;
                alunoQuestionario.CodQuestionario = item.CodQuestionario;
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                alunoQuestionario.CodAcesso = usuario.CodAcesso;
                if (item.CodAlunoQuestionario > 0)
                {
                    _repositorioAlunoQuestionario.Alterar(alunoQuestionario);
                }
                else
                {
                    _repositorioAlunoQuestionario.Inserir(alunoQuestionario);
                }
            }
            CarregarComboTela();
            return RedirectToAction("Index", "AlunoMatriculas");
        }

        private ActionResult CarregarMensagemDeErro(string message)
        {
            Erro erroParametro = new Erro
            {
                CodErro = 404,
                statusCode = HttpStatusCode.BadRequest,
                MensagemErro = message,
                UrlChamada = System.Web.HttpContext.Current.Request.UrlReferrer == null ? "" : System.Web.HttpContext.Current.Request.UrlReferrer.ToString()
            };
            Session.Add("erroParametro", erroParametro);
            return RedirectToAction("ErroFinal", "Home");
        }


        private void CarregarComboTela()
        {
            ViewBag.DropdownSimNao = Utils.CarregarListaSimNao();
        }
    }
}
