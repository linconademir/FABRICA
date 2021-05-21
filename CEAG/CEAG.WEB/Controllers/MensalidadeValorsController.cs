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
using CEAG.NEGOCIO.Utils;
using CEAG.REPOSITORIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.MensalidadeValores;
using PagedList;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR")]
    public class MensalidadeValorsController : Controller
    {
        private IRepositorioGenerico<MensalidadeValor, int> _repositorioGenericoMensalidadeValores = new RepositorioMensalidadeValor(new CeagDbContext());

        // GET: MensalidadeValors
        public ActionResult Index(string sortOrder, string currentFilter, string searchAno, string searchString, int? page)
        {
            

            #region Paginação

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = searchString;

            int pageSize = Constantes.PAGE_SIZE;
            int pageNumber = (page ?? 1);
            #endregion


            #region Método de consulta

            var mensalidadeValores = new List<MensalidadeValor>();
            if (!string.IsNullOrEmpty(searchString))
            {
                mensalidadeValores = _repositorioGenericoMensalidadeValores.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                        .Where(p =>
                                    p.Turma.ToUpper().Contains(searchString.ToUpper())
                        ).ToList();
            }
            else
            {
                mensalidadeValores = _repositorioGenericoMensalidadeValores.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }


            #endregion


            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<MensalidadeValorExibicaoViewModel> dto = mapper.Map<List<MensalidadeValorExibicaoViewModel>>(mensalidadeValores);

            #endregion


            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            return View(dto.OrderBy(p => p.Turma).ToPagedList(pageNumber, pageSize));
        }

        //// GET: MensalidadeValors/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MensalidadeValor mensalidadeValor = db.MensalidadeValors.Find(id);
        //    if (mensalidadeValor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mensalidadeValor);
        //}

        // GET: MensalidadeValors/Create
        public ActionResult Create()
        {
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            CarregarComboTela();
            return View();
        }

        // POST: MensalidadeValors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MensalidadeValorViewModel mensalidadeValorViewModel)
        {
            

            mensalidadeValorViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion

                #region Inserindo a Turma

                var mensalidadeValor = mapper.Map<MensalidadeValorViewModel, MensalidadeValor>(mensalidadeValorViewModel);
                mensalidadeValor.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoMensalidadeValores.Inserir(mensalidadeValor);

                #endregion

                return RedirectToAction("Index", "MensalidadeValors");
            }

            CarregarComboTela();
            return View(mensalidadeValorViewModel);
        }

        // GET: MensalidadeValors/Edit/5
        public ActionResult Edit(int? codMensalidadeValor)
        {
            

            if (codMensalidadeValor == null)
            {
                return CarregarMensagemDeErro("Dados não existem nesta escola.");
            }

            MensalidadeValor mensalidadeValorConsulta = _repositorioGenericoMensalidadeValores.SelecionarPorId(codMensalidadeValor.Value);
            var escola = UsuarioEscola.ResgatarEscolaLogada();

            if (mensalidadeValorConsulta == null)
            {
                return CarregarMensagemDeErro("Dados não existem nesta escola.");
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != mensalidadeValorConsulta.CodEscola)
            {
                return CarregarMensagemDeErro("Dados não existem nesta escola.");
            }

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            MensalidadeValorViewModel dto = mapper.Map<MensalidadeValorViewModel>(mensalidadeValorConsulta);

            CarregarComboTela();

            return View(dto);
        }

        // POST: MensalidadeValors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MensalidadeValorViewModel mensalidadeValorViewModel)
        {
            

            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion


                #region Alterando a Turma

                var mensalidadeValor = mapper.Map<MensalidadeValorViewModel, MensalidadeValor>(mensalidadeValorViewModel);
                mensalidadeValor.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoMensalidadeValores.Alterar(mensalidadeValor);
                #endregion

                return RedirectToAction("Index", "MensalidadeValors");
            }
            CarregarComboTela();
            return View(mensalidadeValorViewModel);
        }

        // GET: MensalidadeValors/Delete/5
        public ActionResult Delete(int? codMensalidadeValor)
        {
            

            if (codMensalidadeValor == null)
            {
                return CarregarMensagemDeErro("Dados não existem nesta escola.");
            }

            var mensalidadeValorConsulta = _repositorioGenericoMensalidadeValores.SelecionarPorId(codMensalidadeValor.Value);
            if (mensalidadeValorConsulta == null)
            {
                return CarregarMensagemDeErro("Dados não existem nesta escola.");
            }


            if (mensalidadeValorConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Dados não existem nesta escola.");
            }

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion
            var mensalidadeValorViewModel = mapper.Map<MensalidadeValor, MensalidadeValorViewModel>(mensalidadeValorConsulta);

            return View(mensalidadeValorViewModel);
        }

        // POST: Disciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codMensalidadeValor)
        {
            
            var disciplinaBanco = _repositorioGenericoMensalidadeValores.SelecionarPorId(codMensalidadeValor);
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != disciplinaBanco.CodEscola)
            {
                return CarregarMensagemDeErro("Dados não existem nesta escola.");
            }

            _repositorioGenericoMensalidadeValores.ExcluirPorId(codMensalidadeValor);
            return RedirectToAction("Index", "MensalidadeValors");
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
            ViewBag.DropdownListaNivel = Utils.CarregarListaNivel();
        }
    }
}
