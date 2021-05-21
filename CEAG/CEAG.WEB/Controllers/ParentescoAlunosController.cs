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
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.Parentesco;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class ParentescoAlunosController : Controller
    {

        // GET: ParentescoAlunos/Create
        public ActionResult Create(int codAluno)
        {
            if (codAluno <= 0)
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            Aluno aluno = new Aluno();
            using (RepositorioAluno _repoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                aluno = _repoLocal.SelecionarPorId(codAluno);
            }

            if (aluno.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            ParentescoAlunoViewModel parentescoAlunoViewModel = new ParentescoAlunoViewModel();
            parentescoAlunoViewModel.CodAlunoBase = codAluno;
            ViewBag.Aluno = mapper.Map<AlunoExibicaoViewModel>(aluno);
            CarregarComboTela();

            return View(parentescoAlunoViewModel);
        }

        // POST: ParentescoAlunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParentescoAlunoViewModel parentescoAlunoViewModel)
        {
            parentescoAlunoViewModel.Inclusao = DateTime.Now;
            if (ModelState.IsValid)
            {
                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion

                //Instanciando as listas de parentescos
                Parentesco parentesco = new Parentesco();
                List<ParentescoAluno> listaParentescoAlunoAlunoBase = new List<ParentescoAluno>();
                List<ParentescoAluno> listaParentescoAlunoAlunoSelecionado = new List<ParentescoAluno>();

                //Consultando os parentescos do alunos BASE e CADASTRADO
                using (RepositorioParentescoAluno _repoLocal = new RepositorioParentescoAluno(new CeagDbContext()))
                {
                    listaParentescoAlunoAlunoBase = _repoLocal.SelecionarParentesPorAluno(parentescoAlunoViewModel.CodAlunoBase);
                    if (!listaParentescoAlunoAlunoBase.Any())
                    {
                        listaParentescoAlunoAlunoSelecionado = _repoLocal.SelecionarParentesPorAluno(parentescoAlunoViewModel.CodAluno.Value);
                    }
                }

                
                if (!listaParentescoAlunoAlunoBase.Any() && !listaParentescoAlunoAlunoSelecionado.Any())
                {
                    parentesco.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                    parentesco.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
                    parentesco.Descricao = parentescoAlunoViewModel.Descricao;
                    parentesco.Inclusao = DateTime.Now;

                    //Inserindo primeiro registro de parentesco
                    using (RepositorioParentesco _repoLocal = new RepositorioParentesco(new CeagDbContext()))
                    {
                        _repoLocal.Inserir(parentesco);
                    }

                    //Inserindo registro de parentesco Aluno
                    using (RepositorioParentescoAluno _repoLocal = new RepositorioParentescoAluno(new CeagDbContext()))
                    {
                        parentescoAlunoViewModel.CodParentesco = parentesco.CodParentesco;
                        _repoLocal.Inserir(mapper.Map<ParentescoAluno>(parentescoAlunoViewModel));
                    }

                    //Inserindo registro de parentesco Aluno
                    using (RepositorioParentescoAluno _repoLocal = new RepositorioParentescoAluno(new CeagDbContext()))
                    {
                        ParentescoAluno parentescoAlunoBase = new ParentescoAluno();
                        parentescoAlunoBase.CodAluno = parentescoAlunoViewModel.CodAlunoBase;
                        parentescoAlunoBase.CodParentesco = parentesco.CodParentesco;
                        parentescoAlunoBase.Inclusao = DateTime.Now;
                        _repoLocal.Inserir(parentescoAlunoBase);
                    }

                }
                else
                {
                    using (RepositorioParentesco _repoLocal = new RepositorioParentesco(new CeagDbContext()))
                    {
                        parentesco = _repoLocal.SelecionarPorId(listaParentescoAlunoAlunoBase.Select(p => p.CodParentesco).FirstOrDefault().Value);
                        if (parentesco.CodParentesco == 0)
                        {
                            parentesco = _repoLocal.SelecionarPorId(listaParentescoAlunoAlunoSelecionado.Select(p => p.CodParentesco).FirstOrDefault().Value);
                        }
                    }


                    if (!listaParentescoAlunoAlunoBase.Any())
                    {
                        using (RepositorioParentescoAluno _repoLocal = new RepositorioParentescoAluno(new CeagDbContext()))
                        {
                            ParentescoAluno parentescoAlunoBase = new ParentescoAluno();
                            parentescoAlunoBase.CodAluno = parentescoAlunoViewModel.CodAluno;
                            parentescoAlunoBase.CodParentesco = parentesco.CodParentesco;
                            parentescoAlunoBase.Inclusao = DateTime.Now;
                            _repoLocal.Inserir(parentescoAlunoBase);
                        }
                    }
                    else
                    {
                        if (!listaParentescoAlunoAlunoSelecionado.Any())
                        {
                            using (RepositorioParentescoAluno _repoLocal = new RepositorioParentescoAluno(new CeagDbContext()))
                            {
                                parentescoAlunoViewModel.CodParentesco = parentesco.CodParentesco;
                                _repoLocal.Inserir(mapper.Map<ParentescoAluno>(parentescoAlunoViewModel));
                            }
                        }
                    }
                    
                }

               
               return RedirectToAction("Details", "Alunos", new { codAluno = parentescoAlunoViewModel.CodAlunoBase });

            }

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            CarregarComboTela();

            return View(parentescoAlunoViewModel);
        }

       
        // POST: ParentescoAlunos/Delete/5
        public ActionResult Delete(int codParentescoAluno, int codAluno)
        {
            if (codParentescoAluno <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ParentescoAluno parentescoAluno = new ParentescoAluno();
            using (RepositorioParentescoAluno _repoLocal = new RepositorioParentescoAluno(new CeagDbContext()))
            {
                parentescoAluno = _repoLocal.SelecionarPorId(codParentescoAluno);
                if (parentescoAluno == null)
                {
                    return HttpNotFound();
                }

                if (parentescoAluno.Parentesco.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            using (RepositorioParentescoAluno _repoLocal = new RepositorioParentescoAluno(new CeagDbContext()))
            {
                _repoLocal.Excluir(parentescoAluno);
            }

            return RedirectToAction("Details", "Alunos", new { codAluno = codAluno });

        }

        private void CarregarComboTela()
        {
            ViewBag.DropdownTipoParentesco = Utils.CarregarTipoParentesco();
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
    }
}
