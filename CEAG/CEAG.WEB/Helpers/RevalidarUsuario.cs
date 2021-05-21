using AutoMapper;
using CEAG.ACESSODADOS.Context;
using CEAG.REPOSITORIO;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Acesso;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CEAG.WEB.Helpers
{
    public class RevalidarUsuario : ActionFilterAttribute
    {
       // public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() == 0 && HttpContext.Current.User.Identity.GetUserId() != null)
        //    {
        //        var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
        //        var mapper = new Mapper(config);

        //        using (RepositorioAcesso _repositorioAcesso = new RepositorioAcesso(new CeagDbContextAcesso()))
        //        {
        //            var vLogin = _repositorioAcesso.SelecionarPorEmail(HttpContext.Current.User.Identity.GetUserName()).FirstOrDefault();
        //            AcessoViewModel dtoAcesso = mapper.Map<AcessoViewModel>(vLogin);
        //            Constants.UsuarioEscola.AdicionarUsuarioConstante(dtoAcesso.CodEscola.Value, dtoAcesso.Escola, dtoAcesso);
        //        }
        //    }

        //    base.OnActionExecuted(filterContext);
        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() == 0 && HttpContext.Current.User.Identity.GetUserId() != null)
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
                var mapper = new Mapper(config);

                using (RepositorioAcesso _repositorioAcesso = new RepositorioAcesso(new CeagDbContextAcesso()))
                {
                    var vLogin = _repositorioAcesso.SelecionarPorEmail(HttpContext.Current.User.Identity.GetUserName()).FirstOrDefault();
                    AcessoViewModel dtoAcesso = mapper.Map<AcessoViewModel>(vLogin);
                    Constants.UsuarioEscola.AdicionarUsuarioConstante(dtoAcesso.CodEscola.Value, dtoAcesso.Escola, dtoAcesso);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
