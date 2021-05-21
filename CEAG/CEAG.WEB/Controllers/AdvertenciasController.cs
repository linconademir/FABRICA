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
using CEAG.WEB.ViewModel.Advertencia;

namespace CEAG.WEB.Controllers
{
    public class AdvertenciasController : Controller
    {
        #region Metodos padrão scallfoding
      
        // GET: Advertencias
        public ActionResult Index(int codAlunoMatricula)
        {
            using (RepositorioAlunoMatricula _repositorioAlunoMatricula = new RepositorioAlunoMatricula(new CeagDbContext()))
            {
                var alunoMatricula = _repositorioAlunoMatricula.SelecionarPorId(codAlunoMatricula);
                if (alunoMatricula == null || alunoMatricula.Aluno.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                {
                    return CarregarMensagemDeErro("Aluno não existe nesta escola.");
                }
            }

            var advertencias = new List<Advertencia>();
            using (RepositorioAdvertencia _respositorioAdvertenciaLocal = new RepositorioAdvertencia(new CeagDbContext()))
            {
                advertencias = _respositorioAdvertenciaLocal.SelecionarPorMatricula(codAlunoMatricula);
            }

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            ViewBag.CodAlunoMatricula = codAlunoMatricula;
            List<AdvertenciaExibicaoViewModel> advertenciaExibicaoViewModels = mapper.Map<List<AdvertenciaExibicaoViewModel>>(advertencias);
            return View(advertenciaExibicaoViewModels);
        }

      
        // GET: Advertencias/Create
        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        public ActionResult Create(int codAlunoMatricula)
        {
            using (RepositorioAlunoMatricula _repositorioAlunoMatricula = new RepositorioAlunoMatricula(new CeagDbContext()))
            {
                var alunoMatricula = _repositorioAlunoMatricula.SelecionarPorId(codAlunoMatricula);
                if (alunoMatricula == null || alunoMatricula.Aluno.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                {
                    return CarregarMensagemDeErro("Aluno não existe nesta escola.");
                }
            }

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            AdvertenciaViewModel advertenciaViewModel = new AdvertenciaViewModel();
            advertenciaViewModel.CodAlunoMatricula = codAlunoMatricula;
            CarregarComboTela();

            return View(advertenciaViewModel);
        }

        // POST: Advertencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdvertenciaViewModel advertenciaViewModel)
        {
            advertenciaViewModel.Inclusao = DateTime.Now;

            if (ModelState.IsValid)
            {
                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion

                var advertencia = mapper.Map<Advertencia>(advertenciaViewModel);
                advertencia.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                using (RepositorioAdvertencia _repositorioAdvertenciaLocal = new RepositorioAdvertencia(new CeagDbContext()))
                {
                    _repositorioAdvertenciaLocal.Inserir(advertencia);
                }

                return RedirectToAction("Index", "Advertencias", new { codAlunoMatricula = advertenciaViewModel.CodAlunoMatricula});
            }

            CarregarComboTela();
            return View(advertenciaViewModel);
        }

        // GET: Advertencias/Edit/5
        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        public ActionResult Edit(int? codAdvertencia)
        {
            if (!codAdvertencia.HasValue)
            {
                return CarregarMensagemDeErro("Informação não identificada na base de dados.");
            }
            var escola = UsuarioEscola.ResgatarEscolaLogada();
            Advertencia advertencia = new Advertencia();
            using (RepositorioAdvertencia _repoLocal = new RepositorioAdvertencia(new CeagDbContext()))
            {
                advertencia = _repoLocal.SelecionarPorId(codAdvertencia.Value);
            }

            if (advertencia == null || advertencia.AlunoMatricula.Aluno.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada() || advertencia.Resolucao.HasValue)
            {
                return CarregarMensagemDeErro("Informação não identificada na base de dados.");
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != advertencia.AlunoMatricula.Aluno.CodEscola)
            {
                return CarregarMensagemDeErro("Informação não identificada na base de dados.");
            }

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            AdvertenciaViewModel dto = mapper.Map<AdvertenciaViewModel>(advertencia);

            CarregarComboTela();

            return View(dto);
        }

        // POST: Advertencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdvertenciaViewModel advertenciaViewModel)
        {
            if (ModelState.IsValid)
            {
                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion

                #region Alterando a Turma

                var advertencia = mapper.Map<Advertencia>(advertenciaViewModel);
                advertencia.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                using (RepositorioAdvertencia _repoLocal = new RepositorioAdvertencia(new CeagDbContext()))
                {
                    _repoLocal.Alterar(advertencia);
                }

                #endregion

                return RedirectToAction("Index", "Advertencias", new { codAlunoMatricula = advertenciaViewModel.CodAlunoMatricula });
            }

            CarregarComboTela();
            return View(advertenciaViewModel);
        }

        //// GET: Advertencias/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Advertencia advertencia = db.Advertencias.Find(id);
        //    if (advertencia == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(advertencia);
        //}

        //// POST: Advertencias/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Advertencia advertencia = db.Advertencias.Find(id);
        //    db.Advertencias.Remove(advertencia);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        #endregion

        #region Métodos de apoio

        private void CarregarComboTela()
        {
            ViewBag.DropdownTipoAdvertencia = Utils.CarregarTipoAdvertencia();
            ViewBag.DropdownMotivoAdvertencia = Utils.CarregarMotivoAdvertencia();
            ViewBag.DropdownTipoUrgenciaAdvertencia = Utils.CarregarTipoUrgenciaAdvertencia();
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

        #endregion
    }
}
