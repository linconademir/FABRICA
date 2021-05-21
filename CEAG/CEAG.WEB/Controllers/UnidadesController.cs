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
using CEAG.WEB.ViewModel.Unidade;
using PagedList;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class UnidadesController : Controller
    {
        private RepositorioUnidade _repositorioUnidade = new RepositorioUnidade(new CeagDbContext());

        // GET: Unidades
        public ActionResult Index(string sortOrder, string currentFilter, string searchAno, string searchString, int? page, string erro)
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

            var unidades = new List<Unidade>();
            if (!string.IsNullOrEmpty(searchAno) && !searchAno.Equals("TODOS"))
            {
                unidades = _repositorioUnidade.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                        .Where(p => p.Ano == Convert.ToUInt32(searchAno)).OrderBy(p => p.Numero).ToList();
            }
            else
            {
                unidades = _repositorioUnidade.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).OrderByDescending(p => p.Ano).OrderBy(p => p.Numero).ToList();
            }


            #endregion

            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<UnidadeExibicaoViewModel> dto = mapper.Map<List<UnidadeExibicaoViewModel>>(unidades);

            #endregion
            CarregarComboTela(new List<Unidade>());
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(dto.ToPagedList(pageNumber, pageSize));
        }


        // GET: Unidades/Create
        public ActionResult Create(string erro)
        {
            

            var unidadesCadastradas = _repositorioUnidade.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                        .Where(p => p.Ano == DateTime.Now.Year).OrderBy(p => p.Numero).ToList();


            ViewBag.MessageError = erro;
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            CarregarComboTela(unidadesCadastradas);
            return View();
        }

     
        // POST: Unidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UnidadeViewModel unidadeViewModel)
        {
            

            unidadeViewModel.Inclusao = DateTime.Now;
            unidadeViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            var unidadesCadastradas = _repositorioUnidade.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                        .Where(p => p.Ano == DateTime.Now.Year).OrderBy(p => p.Numero).ToList();

            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                if (unidadeViewModel.Fechamento.HasValue)
                {
                    ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
                    CarregarComboTela(unidadesCadastradas);
                    if (unidadeViewModel.Fechamento.Value <= unidadeViewModel.Abertura)
                    {
                        return RedirectToAction("Create", "Unidades", new { erro = "Data de fechamento está menor que a data de abertura." });
                    }

                    if (unidadeViewModel.Abertura.Date.Year != DateTime.Now.Year)
                    {
                        return RedirectToAction("Create", "Unidades", new { erro = "A data não corresponde ao ano atual." });
                    }
                }

                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);

                var unidade = mapper.Map<Unidade>(unidadeViewModel);
                unidade.CodAcesso = usuario.CodAcesso;
                _repositorioUnidade.Inserir(unidade);
                return RedirectToAction("Index");
            }

            CarregarComboTela(unidadesCadastradas);
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            return View(unidadeViewModel);
        }

        // GET: Unidades/Edit/5
        public ActionResult Edit(int? codUnidade, string erro)
        {
            
            if (codUnidade == null)
            {
                return CarregarMensagemDeErro("Unidade não existe nesta escola.");
            }

            Unidade unidade = _repositorioUnidade.SelecionarPorId(codUnidade.Value);
            if (unidade == null)
            {
                return CarregarMensagemDeErro("Unidade não existe nesta escola.");
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != unidade.CodEscola)
            {
                return CarregarMensagemDeErro("Unidade não existe nesta escola.");
            }


            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            var unidadeViewModel = mapper.Map<UnidadeViewModel>(unidade);
            var unidadesCadastradas = _repositorioUnidade.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                      .Where(p => p.Numero != unidade.Numero).OrderBy(p => p.Numero).ToList();

            if (unidadeViewModel.Fechamento.HasValue)
            {
                ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
                CarregarComboTela(unidadesCadastradas);
                if (unidadeViewModel.Fechamento.Value <= unidadeViewModel.Abertura)
                {
                    return RedirectToAction("Edit", "Unidades", new { erro = "Data de fechamento está menor que a data de abertura." });
                }

                //if (unidadeViewModel.Abertura.Date.Year != DateTime.Now.Year)
                //{
                //    return RedirectToAction("Edit", "Unidades", new { erro = "A data não corresponde ao ano atual." });
                //}
            }
            ViewBag.MessageError = erro;
            CarregarComboTela(unidadesCadastradas);
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(unidadeViewModel);
        }

        // POST: Unidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UnidadeViewModel unidadeViewModel)
        {
            
            unidadeViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);

                var unidade = mapper.Map<UnidadeViewModel, Unidade>(unidadeViewModel);
                unidade.CodAcesso = usuario.CodAcesso;
                _repositorioUnidade.Alterar(unidade);
                return RedirectToAction("Index");
            }
            var unidadesCadastradas = _repositorioUnidade.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                      .Where(p => p.Ano == DateTime.Now.Year).OrderBy(p => p.Numero).ToList();
            CarregarComboTela(unidadesCadastradas);
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(unidadeViewModel);
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

        private void CarregarComboTela(List<Unidade> unidadesCadastradas)
        {
            #region Numero Unidade

            var listaNumeroUnidade = new List<SelectListItem>();

            if (!unidadesCadastradas.Where(p => p.Numero == 1).Any())
            {
                listaNumeroUnidade.Add(new SelectListItem { Value = "1", Text = "1º Unidade" });
            }
            if (!unidadesCadastradas.Where(p => p.Numero == 2).Any())
            {
                listaNumeroUnidade.Add(new SelectListItem { Value = "2", Text = "2º Unidade" });
            }
            if (!unidadesCadastradas.Where(p => p.Numero == 3).Any())
            {
                listaNumeroUnidade.Add(new SelectListItem { Value = "3", Text = "3º Unidade" });
            }
            if (!unidadesCadastradas.Where(p => p.Numero == 4).Any())
            {
                listaNumeroUnidade.Add(new SelectListItem { Value = "4", Text = "4º Unidade" });
            }


            var dropdownNumeroUnidade = new SelectList(listaNumeroUnidade, "Value", "Text");
            ViewBag.DropdownNumeroUnidade = dropdownNumeroUnidade;
            ViewBag.Ano = DateTime.Now.Year;
            ViewBag.DropdownAnoLetivo = Utils.CarregarAno();

            #endregion
        }

    }
}
