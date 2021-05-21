using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
using CEAG.WEB.ViewModel.Telefone;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class TelefonesController : Controller
    {
        private IRepositorioGenerico<Telefone,int> _repositorioGenericoTelefone = new RepositorioTelefone(new CeagDbContext());
        private RepositorioTelefone _repositorioGenericoTelefoneAlterar = new RepositorioTelefone(new CeagDbContext());
        private IRepositorioGenerico<Aluno, int> _repositorioGenericoAluno = new RepositorioAluno(new CeagDbContext());
        // GET: Telefones
        public ActionResult Index(int? codAluno)
        {
            
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<List<Aluno>, List<AlunoExibicaoViewModel>>());
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<TelefoneExibicaoViewModel> dto = mapper.Map<List<TelefoneExibicaoViewModel>>(_repositorioGenericoTelefone.Selecionar(codAluno.Value));
            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(codAluno.Value));
            ViewBag.Aluno = aluno;
            
            //List<AlunoExibicaoViewModel> retorno = Mapper<List<Aluno>, List<AlunoExibicaoViewModel>>(_repositorioGenericoAluno.Selecionar(1));
            return View(dto);
        }

       

        // GET: Telefones/Create
        public ActionResult Create(int codAluno)
        {
            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            var alunoConsulta = new Aluno();
            using (RepositorioAluno _repositorioAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                alunoConsulta = _repositorioAlunoLocal.SelecionarPorId(codAluno);
                if (alunoConsulta == null || alunoConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                {
                    return CarregarMensagemDeErro("Aluno não existe nesta escola.");
                }
            }

            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(codAluno));
            ViewBag.Aluno = aluno;
            CarregarComboTela();
            return View();
        }

        // POST: Telefones/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TelefoneViewModel telefoneViewModel)
        {
            telefoneViewModel.Inclusao = DateTime.Now;
            telefoneViewModel.Numero = Regex.Replace(telefoneViewModel.Numero, "[^0-9]", "");
            telefoneViewModel.Ddd = Regex.Replace(telefoneViewModel.Ddd, "[^0-9]", "");
            if (telefoneViewModel.Tipo.ToUpper().Equals("CELULAR"))
            {
                telefoneViewModel.Numero = '9' + telefoneViewModel.Numero;
            }
            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
               
                #region Mapper

                Mapper mapper = Constants.Utils.ViewModelParaDominio();

                #endregion

                var telefone = mapper.Map<TelefoneViewModel, Telefone>(telefoneViewModel);
                telefone.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoTelefoneAlterar.InserirTelefoneAluno(telefone);
                return RedirectToAction("Details", "Alunos", new { codAluno = telefone.CodAluno });
            }

            #region Mapper
            Mapper mapper1 = Constants.Utils.DominioParaViewModel();
            #endregion

            AlunoExibicaoViewModel aluno = mapper1.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(telefoneViewModel.CodAluno));
            ViewBag.Aluno = aluno;
            CarregarComboTela();
            return View(telefoneViewModel);
        }

        // GET: Telefones/Edit/5
        public ActionResult Edit(int? codTelefone)
        {
            
            if (codTelefone == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var telefone = _repositorioGenericoTelefone.SelecionarPorId(codTelefone.Value);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);

            TelefoneViewModel dto = mapper.Map<TelefoneViewModel>(telefone);
            if (dto.Tipo.ToUpper().Equals("CELULAR"))
            {
                if (dto.Numero.Length > 8)
                {
                    dto.Numero = dto.Numero.Substring(1, 8);
                }
               
            }

            if (dto == null)
            {
                return HttpNotFound();
            }
            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(telefone.CodAluno.Value));
            ViewBag.Aluno = aluno;

            CarregarComboTela();
            return View(dto);
        }

        // POST: Telefones/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TelefoneViewModel telefoneViewModel)
        {
            
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
            var mapper = new Mapper(config);
            telefoneViewModel.Inclusao = DateTime.Now;
            telefoneViewModel.Numero = Regex.Replace(telefoneViewModel.Numero, "[^0-9]", "");
            telefoneViewModel.Ddd = Regex.Replace(telefoneViewModel.Ddd, "[^0-9]", "");
            if (telefoneViewModel.Tipo.ToUpper().Equals("CELULAR"))
            {
                telefoneViewModel.Numero = '9' + telefoneViewModel.Numero;
            }
            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var telefone = mapper.Map<TelefoneViewModel, Telefone>(telefoneViewModel);

                telefone.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoTelefone.Alterar(telefone);

                return RedirectToAction("Details", "Alunos", new { codAluno = telefone.CodAluno });
            }

            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(telefoneViewModel.CodAluno));
            ViewBag.Aluno = aluno;

            CarregarComboTela();
            return View(telefoneViewModel);

        }

        // GET: Telefones/Delete/5
        public ActionResult Delete(int? codTelefone)
        {
            
            if (codTelefone == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var telefoneBanco = _repositorioGenericoTelefone.SelecionarPorId(codTelefone.Value);
            if (telefoneBanco == null)
            {
                return HttpNotFound();
            }
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            var telefoneViewModel = mapper.Map<Telefone, TelefoneViewModel>(telefoneBanco);
            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(telefoneBanco.CodAluno.Value));
            ViewBag.Aluno = aluno;

            CarregarComboTela();

            return View(telefoneViewModel);
        }
        // POST: Telefones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codTelefone)
        {
            
            var codAluno = _repositorioGenericoTelefone.SelecionarPorId(codTelefone).CodAluno;
            _repositorioGenericoTelefone.ExcluirPorId(codTelefone);
            return RedirectToAction("Details", "Alunos", new { codAluno = codAluno.Value });
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
            ViewBag.DropdownLocalTelefone = Utils.CarregarLocalTelefone();
            ViewBag.DropdownTipoTelefone = Utils.CarregarTipoTelefone();
            ViewBag.DropdownTipoContato = Utils.CarregarTipoContato();
            ViewBag.DropdownPessoaTelefone = Utils.CarregarTipoPessoaContato();
        }
    }
}
