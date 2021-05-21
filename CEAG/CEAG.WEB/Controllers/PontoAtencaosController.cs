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
using CEAG.WEB.ViewModel.PontoAtencao;

namespace CEAG.WEB.Controllers
{
    public class PontoAtencaosController : Controller
    {

        // GET: PontoAtencaos
        public ActionResult Index()
        {
            List<PontoAtencao> pontoAtencaos = new List<PontoAtencao>();
            using (RepositorioPontoAtencao _repoLocal = new RepositorioPontoAtencao(new CeagDbContext()))
            {
                pontoAtencaos = _repoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }

            return View(pontoAtencaos);
        }

        // GET: PontoAtencaos/Create
        public ActionResult Create(string tipoPontoAtencao)
        {
            ViewBag.TipoPontoAtencao = tipoPontoAtencao;
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View();
        }

        // POST: PontoAtencaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PontoAtencaoViewModel pontoAtencaoViewModel)
        {
            pontoAtencaoViewModel.Inclusao = DateTime.Now;
            if (ModelState.IsValid)
            {
                #region Mapper

                Mapper mapper = Constants.Utils.ViewModelParaDominio();

                #endregion

                var pontoAtencao = mapper.Map<PontoAtencaoViewModel, PontoAtencao>(pontoAtencaoViewModel);
                pontoAtencao.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                pontoAtencao.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
                using (RepositorioPontoAtencao _repoLocal = new RepositorioPontoAtencao(new CeagDbContext()))
                {
                    _repoLocal.Inserir(pontoAtencao);
                }
                return RedirectToAction("Index","Home");
            }

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(pontoAtencaoViewModel);
        }

        public ActionResult ViewUser(string acessoNome)
        {
            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion
            if (!string.IsNullOrEmpty(acessoNome))
            {
                List<PontoAtencao> pontoAtencaos = new List<PontoAtencao>();
                using (RepositorioPontoAtencao _repoLocal = new RepositorioPontoAtencao(new CeagDbContext()))
                {
                    pontoAtencaos = _repoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
                }

                using (RepositorioAcesso _repoLocal = new RepositorioAcesso(new CeagDbContext()))
                {
                    var acesso = _repoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).Where(s => s.Nome.Equals(acessoNome)).SingleOrDefault();
                    pontoAtencaos = pontoAtencaos.Where(p => p.CodAcesso.Equals(acesso.CodAcesso)).ToList();
                }

                return View(mapper.Map<List<PontoAtencaoViewModel>>(pontoAtencaos));
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ResolverPonto(int? codPontoAtencao)
        {
            PontoAtencao pontoAtencaos = new PontoAtencao(); ;
            if (codPontoAtencao.HasValue)
            {
                using (RepositorioPontoAtencao _repoLocal = new RepositorioPontoAtencao(new CeagDbContext()))
                {
                    pontoAtencaos = _repoLocal.SelecionarPorId(codPontoAtencao.Value);
                    pontoAtencaos.Resolucao = DateTime.Now;
                    pontoAtencaos.CodAcessoResolucao = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                    _repoLocal.Alterar(pontoAtencaos);
                }
            }

            Acesso acesso = new Acesso();
            using (RepositorioAcesso _repoLocal = new RepositorioAcesso(new CeagDbContext()))
            {
                acesso = _repoLocal.SelecionarPorId((pontoAtencaos != null ? pontoAtencaos.CodAcesso : 0));
            }

            return RedirectToAction("ViewUser", "PontoAtencaos", new { acessoNome = acesso.Nome });
        }

    }
}
