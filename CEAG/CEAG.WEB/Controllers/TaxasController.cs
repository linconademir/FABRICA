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
using CEAG.WEB.ViewModel.Taxa;
using PagedList;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class TaxasController : Controller
    {
        private IRepositorioGenerico<Taxa, int> _repositorioGenericoTaxa = new RepositorioTaxa(new CeagDbContext());

        // GET: Taxas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            
            #region Paginação

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
            #endregion

            #region Método de consulta

            var taxas = new List<Taxa>();
            if (!string.IsNullOrEmpty(searchString))
            {
                taxas = _repositorioGenericoTaxa.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                .Where(p =>
                                p.Descricao.ToUpper().Contains(searchString.ToUpper())
                                || p.Tipo.ToUpper().Contains(searchString.ToUpper())
                    ).ToList();
            }
            else
            {
                taxas = _repositorioGenericoTaxa.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }


            #endregion

            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<TaxaExibicaoViewModel> dto = mapper.Map<List<TaxaExibicaoViewModel>>(taxas);

            #endregion

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(dto.ToPagedList(pageNumber, pageSize));
        }

        // GET: Taxas/Create
        public ActionResult Create()
        {
            
            var escola = UsuarioEscola.ResgatarEscolaLogada();
            ViewBag.CodEscola = escola.CodEscola;

            CarregarComboTela();
            return View();
        }

        // POST: Taxas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaxaViewModel taxaViewModel)
        {
            
            taxaViewModel.Inclusao = DateTime.Now;
            taxaViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);

                var taxa = mapper.Map<TaxaViewModel, Taxa>(taxaViewModel);
                taxa.CodAcesso = usuario.CodAcesso;
                
                _repositorioGenericoTaxa.Inserir(taxa);
                return RedirectToAction("Index");
            }

            var escola = UsuarioEscola.ResgatarEscolaLogada();
            ViewBag.CodEscola = escola.CodEscola;

            CarregarComboTela();
            return View(taxaViewModel);
        }

        // GET: Taxas/Edit/5
        public ActionResult Edit(int? codTaxa)
        {
            
            if (codTaxa == null)
            {
                return CarregarMensagemDeErro("Taxa não existe nesta escola.");
            }

            Taxa taxaConsulta = _repositorioGenericoTaxa.SelecionarPorId(codTaxa.Value);

            if (taxaConsulta == null)
            {
                return CarregarMensagemDeErro("Taxa não existe nesta escola.");
            }

            if (taxaConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Taxa não existe nesta escola.");
            }

          

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            TaxaViewModel dto = mapper.Map<TaxaViewModel>(taxaConsulta);

            var escola = UsuarioEscola.ResgatarEscolaLogada();
            ViewBag.Escola = escola;
            CarregarComboTela();
            return View(dto);
        }

        // POST: Taxas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaxaViewModel taxaViewModel)
        {
            
            taxaViewModel.Inclusao = DateTime.Now;
            taxaViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var taxa = mapper.Map<TaxaViewModel, Taxa>(taxaViewModel);
                taxa.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoTaxa.Alterar(taxa);

                return RedirectToAction("Index");
            }

            var escola = UsuarioEscola.ResgatarEscolaLogada();
            ViewBag.Escola = escola;
            return View(taxaViewModel);
        }

        
       
        public ActionResult CancelarTaxa(int codTaxa)
        {
            
            var usuario = UsuarioEscola.ResgatarUsuarioLogado();
            var taxa = _repositorioGenericoTaxa.SelecionarPorId(codTaxa);
            if (taxa.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Taxa não existe nesta escola.");
            }
            taxa.Cancelamento = DateTime.Now;
            taxa.CodAcesso = usuario.CodAcesso;
            _repositorioGenericoTaxa.Alterar(taxa);

            return RedirectToAction("Index");
        }

        private void CarregarComboTela()
        {
            ViewBag.DropdownTipoTaxa = Utils.CarregarTipoTaxas();
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
