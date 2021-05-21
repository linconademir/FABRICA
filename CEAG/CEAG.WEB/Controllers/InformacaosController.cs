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
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.Informacao;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class InformacaosController : Controller
    {
        private IRepositorioGenerico<Aluno, int> _repositorioGenericoAluno = new RepositorioAluno(new CeagDbContext());
        private IRepositorioGenerico<Informacao, int> _repositorioGenericoInformacao = new RepositorioInformacao(new CeagDbContext());

        // GET: Informacaos
        public ActionResult Index(int codAluno)
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<List<Aluno>, List<AlunoExibicaoViewModel>>());
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<InformacaoExibicaoViewModel> dto = mapper.Map<List<InformacaoExibicaoViewModel>>(_repositorioGenericoInformacao.Selecionar(codAluno));
            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(codAluno));
            ViewBag.Aluno = aluno;
            return View(dto);
        }


        // GET: Informacaos/Create
        public ActionResult Create(int codAluno)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(codAluno));
            ViewBag.Aluno = aluno;

            CarregarComboTela();
            return View();
        }

        // POST: Informacaos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InformacaoViewModel informacaoViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
            var mapper = new Mapper(config);
            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();

                var informacao = mapper.Map<InformacaoViewModel, Informacao>(informacaoViewModel);
                informacao.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoInformacao.Inserir(informacao);

                //return RedirectToAction("Index");
                return RedirectToAction("Details", "Alunos", new { codAluno = informacao.CodAluno });
            }

            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(informacaoViewModel.CodAluno));
            ViewBag.Aluno = aluno;

            CarregarComboTela();
            return View(informacaoViewModel);
        }

        // GET: Informacaos/Edit/5
        public ActionResult Edit(int? codInformacao)
        {
            if (codInformacao == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var informacao = _repositorioGenericoInformacao.SelecionarPorId(codInformacao.Value);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);

            InformacaoViewModel dto = mapper.Map<InformacaoViewModel>(informacao);
           

            if (dto == null)
            {
                return HttpNotFound();
            }
            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(informacao.CodAluno));
            ViewBag.Aluno = aluno;

            CarregarComboTela();
            return View(dto);
        }

        // POST: Informacaos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InformacaoViewModel informacaoViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
            var mapper = new Mapper(config);

            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var informacao = mapper.Map<InformacaoViewModel, Informacao>(informacaoViewModel);
                informacao.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoInformacao.Alterar(informacao);

                return RedirectToAction("Details", "Alunos", new { codAluno = informacao.CodAluno });
            }

            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(informacaoViewModel.CodAluno));
            ViewBag.Aluno = aluno;

            CarregarComboTela();
            return View(informacaoViewModel);
            
        }

        // GET: Informacaos/Delete/5
        public ActionResult Delete(int? codInformacao)
        {
            if (codInformacao == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var informacaoBanco = _repositorioGenericoInformacao.SelecionarPorId(codInformacao.Value);
            if (informacaoBanco == null)
            {
                return HttpNotFound();
            }
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            var informacaoViewModel = mapper.Map<Informacao, InformacaoViewModel>(informacaoBanco);
            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(informacaoBanco.CodAluno));
            ViewBag.Aluno = aluno;

            CarregarComboTela();

            return View(informacaoViewModel);
        }

        // POST: Informacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codInformacao)
        {
            var informacao = _repositorioGenericoInformacao.SelecionarPorId(codInformacao);
            _repositorioGenericoInformacao.ExcluirPorId(codInformacao);
            return RedirectToAction("Details", "Alunos", new { codAluno = informacao.CodAluno });
        }

        private void CarregarComboTela()
        {
            ViewBag.DropdownListaInformacoes = Utils.CarregarInformacoesAdicionais();
        }

      
    }
}
