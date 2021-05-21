using AutoMapper;
using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.REPOSITORIO;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Acesso;
using CEAG.WEB.ViewModel.PontoAtencao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CEAG.WEB.Controllers
{
    //[AllowAnonymous]
    //[Authorize(Roles = "Administrador, Usuario")]
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR,SECRETARIA,PROFESSOR ")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            AcessoViewModel acesso = new AcessoViewModel();
            acesso = UsuarioEscola.ResgatarUsuarioLogado();
            List<PontoAtencao> pontoAtencaos = new List<PontoAtencao>();
            using (RepositorioPontoAtencao _repoLocal = new RepositorioPontoAtencao(new CeagDbContext()))
            {
                pontoAtencaos = _repoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }

            List<PontoAtencaoViewModel> pontoAtencaoViewModels = new List<PontoAtencaoViewModel>();
            using (RepositorioAcesso _repoLocal = new RepositorioAcesso(new CeagDbContext()))
            {
                foreach (var item in pontoAtencaos)
                {
                    PontoAtencaoViewModel pontoAtencaoViewModel = new PontoAtencaoViewModel();
                    pontoAtencaoViewModel = mapper.Map<PontoAtencaoViewModel>(item);
                    pontoAtencaoViewModel.AcessoNome = _repoLocal.SelecionarPorId(item.CodAcesso).Nome;
                    pontoAtencaoViewModels.Add(pontoAtencaoViewModel);
                }
            }

            ViewBag.PontosAtencao = pontoAtencaoViewModels;
            return View(acesso);
        }

        public ActionResult IndexAcademico()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //POST
        
        public ActionResult ErroFinal()
        {
           Erro erro = (Erro)Session["erroParametro"];
            return View(erro);
        }

    }
}