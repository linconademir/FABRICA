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
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Feriado;
using PagedList;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class FeriadosController : Controller
    {
        private RepositorioFeriado _repositorioFeriado = new RepositorioFeriado(new CeagDbContext());

        // GET: Feriados
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            
            #region Paginação ####################

            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int pageSize = Constantes.PAGE_SIZE;
            int pageNumber = (page ?? 1);

            #endregion Paginação ####################

            #region Método de consulta ####################

            var feriados = new List<Feriado>();
            if (!string.IsNullOrEmpty(searchString))
            {
                feriados = _repositorioFeriado.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                .Where(p =>
                                p.Descricao.ToUpper().Contains(searchString.ToUpper())
                                || p.Tipo.ToUpper().Contains(searchString.ToUpper())
                    ).OrderByDescending(s => s.Data).ToList();
            }
            else
            {
                feriados = _repositorioFeriado.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).OrderByDescending(s => s.Data).ToList();
            }


            #endregion Método de consulta ####################

            #region Mapper ####################

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<FeriadoExibicaoViewModel> dto = mapper.Map<List<FeriadoExibicaoViewModel>>(feriados);

            #endregion Mapper ####################

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(dto.ToPagedList(pageNumber, pageSize));
        }

        // GET: Feriados/Create
        public ActionResult Create()
        {
            
            var escola = UsuarioEscola.ResgatarEscolaLogada();
            ViewBag.CodEscola = escola.CodEscola;

            CarregarComboTela();
            return View();
        }

        // POST: Feriados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeriadoViewModel feriadoViewModel)
        {
            
            var usuario = UsuarioEscola.ResgatarUsuarioLogado();
            feriadoViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);

                var feriado = mapper.Map<FeriadoViewModel, Feriado>(feriadoViewModel);
                feriado.CodAcesso = usuario.CodAcesso;
                _repositorioFeriado.Inserir(feriado);
                return RedirectToAction("Index");
            }

            var escola = UsuarioEscola.ResgatarEscolaLogada();
            ViewBag.CodEscola = escola.CodEscola;

            CarregarComboTela();
            return View(feriadoViewModel);
        }

        // GET: Feriados/Edit/5
        public ActionResult Edit(int? codFeriado)
        {
            
            if (codFeriado == null)
            {
                return CarregarMensagemDeErro("Feriado não existe nesta escola.");
            }

            Feriado feriadoConsulta = _repositorioFeriado.SelecionarPorId(codFeriado.Value);

            if (feriadoConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Feriado não existe nesta escola.");
            }

            if (feriadoConsulta == null)
            {
                return CarregarMensagemDeErro("Feriado não existe nesta escola.");
            }

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            FeriadoViewModel dto = mapper.Map<FeriadoViewModel>(feriadoConsulta);

            var escola = UsuarioEscola.ResgatarEscolaLogada();
            ViewBag.Escola = escola;
            CarregarComboTela();
            return View(dto);
        }

        // POST: Feriados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FeriadoViewModel feriadoViewModel)
        {
            
            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);

                var feriado = mapper.Map<FeriadoViewModel, Feriado>(feriadoViewModel);
                feriado.CodAcesso = usuario.CodAcesso;
                _repositorioFeriado.Alterar(feriado);

                return RedirectToAction("Index");
            }

            var escola = UsuarioEscola.ResgatarEscolaLogada();
            ViewBag.Escola = escola;
            return View(feriadoViewModel);
        }
        
        // POST: Feriados/Delete/5
        public ActionResult ExcluirFeriado(int codFeriado)
        {
            
            Feriado feriadoConsulta = _repositorioFeriado.SelecionarPorId(codFeriado);

            if (feriadoConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Feriado não existe nesta escola.");
            }
           
            _repositorioFeriado.ExcluirPorId(codFeriado);

            return RedirectToAction("Index");
        }

        private void CarregarComboTela()
        {
            ViewBag.DropdownTipoFeriado = Utils.CarregarTipoFeriado();
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
