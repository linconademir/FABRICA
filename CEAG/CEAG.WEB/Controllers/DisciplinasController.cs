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
using CEAG.NEGOCIO.Regras;
using CEAG.NEGOCIO.Utils;
using CEAG.REPOSITORIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Acesso;
using CEAG.WEB.ViewModel.Disciplina;
using PagedList;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class DisciplinasController : Controller
    {
        // GET: Disciplinas
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

            #region Consulta

            NegocioDisciplina _instanciaDisciplina = new NegocioDisciplina();
            var disciplinas = new List<Disciplina>();
            disciplinas = _instanciaDisciplina.ObterDisciplina(UsuarioEscola.ResgatarUsuarioLogadoAcesso(), searchString);

            #endregion Consulta

            #region Mapper

            Mapper mapperDominioParaView = Constants.Utils.DominioParaViewModel();
            List<DisciplinaExibicaoViewModel> dto = mapperDominioParaView.Map<List<DisciplinaExibicaoViewModel>>(disciplinas);

            #endregion Mapper

            #region ViewBag

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            #endregion

            return View(dto.OrderBy(p => p.Descricao).ToPagedList(pageNumber, pageSize));
        }

        // GET: Disciplinas/Create
        public ActionResult Create()
        {
            #region ViewBag

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            CarregarComboTela();

            #endregion ViewBag

            return View();
        }

        // POST: Disciplinas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DisciplinaViewModel disciplinaViewModel)
        {
            if (ModelState.IsValid)
            {
                #region Carregar Instancias

                var _instanciaNegocioDisciplina = new NegocioDisciplina();
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();

                #endregion Carregar Instancias

                #region Mapper

                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                var disciplina = mapper.Map<DisciplinaViewModel, Disciplina>(disciplinaViewModel);
                var acesso = mapper.Map<AcessoViewModel, Acesso>(usuario);

                #endregion

                #region Inserindo a Disciplina

                _instanciaNegocioDisciplina.Inserir(disciplina, acesso);

                #endregion

                return RedirectToAction("Index");
            }

            #region ViewBag

            CarregarComboTela();
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            #endregion

            return View(disciplinaViewModel);
        }

        // GET: Disciplinas/Edit/5
        public ActionResult Edit(int? codDisciplina)
        {
            if (codDisciplina == null)
            {
                return CarregarMensagemDeErro("Disciplina não existe nesta escola.");
            }

            #region Carregando Instancias

            NegocioDisciplina _instanciaNegocioDisciplina = new NegocioDisciplina();
            var acesso = UsuarioEscola.ResgatarUsuarioLogadoAcesso();

            #endregion

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            #region Realizando consulta

            Disciplina disciplinaConsulta = _instanciaNegocioDisciplina.ObterDisciplina(codDisciplina.Value);

            #endregion Realizando consulta


            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != disciplinaConsulta.CodEscola)
            {
                return CarregarMensagemDeErro("Disciplina não existe nesta escola.");
            }

            DisciplinaViewModel dto = mapper.Map<DisciplinaViewModel>(disciplinaConsulta);

            if (dto == null)
            {
                return CarregarMensagemDeErro("Disciplina não existe nesta escola.");
            }

            CarregarComboTela();
            return View(dto);
        }

        // POST: Disciplinas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DisciplinaViewModel disciplinaViewModel)
        {

            if (ModelState.IsValid)
            {

                #region Mapper

                Mapper mapper = Constants.Utils.ViewModelParaDominio();

                #endregion

                #region Alterando os dados da Disciplina
                var _instanciaDisciplina = new NegocioDisciplina();

                var disciplina = mapper.Map<DisciplinaViewModel, Disciplina>(disciplinaViewModel);
                var acesso = UsuarioEscola.ResgatarUsuarioLogadoAcesso();
                _instanciaDisciplina.Alterar(disciplina, acesso);
                #endregion

                return RedirectToAction("Index");
            }
            return View(disciplinaViewModel);
        }

        // GET: Disciplinas/Delete/5
        public ActionResult Delete(int? codDisciplina)
        {

            if (codDisciplina == null)
            {
                return CarregarMensagemDeErro("Disciplina não existe nesta escola.");
            }
            
            var _instanciaDisciplina = new NegocioDisciplina();
            var disciplinaBanco = _instanciaDisciplina.ObterDisciplina(codDisciplina.Value);
           
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != disciplinaBanco.CodEscola)
            {
                return CarregarMensagemDeErro("Disciplina não existe nesta escola.");
            }

            if (disciplinaBanco == null)
            {
                return CarregarMensagemDeErro("Disciplina não existe nesta escola.");
            }

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            var disciplinaViewModel = mapper.Map<Disciplina, DisciplinaViewModel>(disciplinaBanco);

            CarregarComboTela();

            return View(disciplinaViewModel);
        }

        // POST: Disciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codDisciplina)
        {
            var _instanciaDisciplina = new NegocioDisciplina();
            var disciplinaBanco = _instanciaDisciplina.ObterDisciplina(codDisciplina);

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != disciplinaBanco.CodEscola)
            {
                return CarregarMensagemDeErro("Disciplina não existe nesta escola.");
            }

            _instanciaDisciplina.Excluir(disciplinaBanco);

            return RedirectToAction("Index", "Disciplinas");
        }

        private void CarregarComboTela()
        {

            ViewBag.DropdownSimNao = Utils.CarregarListaSimNao();

            #region Tipo Disciplina

            var listaTipoDisciplina = new List<SelectListItem>
            {
                new SelectListItem { Value = "BASE NACIONAL COMUM", Text = "BASE NACIONAL COMUM" },
                new SelectListItem { Value = "PARTE DIVERSIFICADA", Text = "PARTE DIVERSIFICADA" }
            };
            var dropdownTipoDisciplina = new SelectList(listaTipoDisciplina, "Value", "Text");
            ViewBag.DropdownTipoDisciplina = dropdownTipoDisciplina;


            #endregion

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
