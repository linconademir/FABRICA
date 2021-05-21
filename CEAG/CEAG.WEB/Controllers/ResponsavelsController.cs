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
using CEAG.WEB.ViewModel.Responsavel;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class ResponsavelsController : Controller
    {
        private IRepositorioGenerico<Logradouro, int> _repositorioGenericoLogradouro = new RepositorioLogradouro(new CeagDbContext());
        private IRepositorioGenerico<Aluno, int> _repositorioGenericoAluno = new RepositorioAluno(new CeagDbContext());
        private IRepositorioGenerico<Responsavel, int> _repositorioGenericoResponsavel = new RepositorioResponsavel(new CeagDbContext());
        private RepositorioTelefone _repositorioTelefone = new RepositorioTelefone(new CeagDbContext());


        // GET: Responsavels
        public ActionResult Index(int codAluno)
        {


            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<ResponsavelExibicaoViewModel> dto = mapper.Map<List<ResponsavelExibicaoViewModel>>(_repositorioGenericoResponsavel.Selecionar(codAluno));

            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(codAluno));
            ViewBag.Aluno = aluno;
            return View(dto);
        }

        // GET: Responsavels/Details/5
        public ActionResult Details(int? codResponsavel)
        {
            if (codResponsavel == null)
            {
                return CarregarMensagemDeErro("Responsável não existe nesta escola.");
            }

            Responsavel responsavel = _repositorioGenericoResponsavel.SelecionarPorId(codResponsavel.Value);
            if (responsavel.Aluno.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Responsável não existe nesta escola.");
            }

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            ResponsavelExibicaoViewModel dto = mapper.Map<ResponsavelExibicaoViewModel>(responsavel);

            if (dto == null)
            {
                return HttpNotFound();
            }
            return View(dto);
        }

        // GET: Responsavels/Create
        public ActionResult Create(int codAluno)
        {


            Aluno alunoConsulta = _repositorioGenericoAluno.SelecionarPorId(codAluno);
            if (alunoConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            AlunoExibicaoViewModel alunoExibicao = mapper.Map<AlunoExibicaoViewModel>(alunoConsulta);
            ViewBag.Aluno = alunoExibicao;

            CarregarComboTela();
            ResponsavelViewModel responsavelViewModel = new ResponsavelViewModel();

            return View(responsavelViewModel);
        }

        // POST: Responsavels/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResponsavelViewModel responsavelViewModel, IEnumerable<string> Tipos)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                #region Inserindo o Logradouro básico
                responsavelViewModel.Cep = Regex.Replace(responsavelViewModel.Cep, "[^0-9]", "");
                responsavelViewModel.Cpf = Regex.Replace(responsavelViewModel.Cpf, "[^0-9]", "");

                var logradouro = mapper.Map<ResponsavelViewModel, Logradouro>(responsavelViewModel);
                logradouro.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoLogradouro.Inserir(logradouro);
                responsavelViewModel.CodLogradouro = logradouro.CodLogradouro;


                #endregion

                #region Inserindo o Responsável


                foreach (var item in Tipos)
                {
                    var responsavel = mapper.Map<ResponsavelViewModel, Responsavel>(responsavelViewModel);
                    responsavel.CodAcesso = usuario.CodAcesso;
                    responsavel.Tipo = item;
                    _repositorioGenericoResponsavel.Inserir(responsavel);


                    #region Inserindo Telefone

                    if (!string.IsNullOrEmpty(responsavelViewModel.NumeroTelefone))
                    {
                        responsavelViewModel.Numero = Regex.Replace(responsavelViewModel.Numero, "[^0-9]", "");
                        responsavelViewModel.Ddd = Regex.Replace(responsavelViewModel.Ddd, "[^0-9]", "");
                        if (responsavelViewModel.TipoTelefone.ToUpper().Equals("CELULAR"))
                        {
                            responsavelViewModel.Numero = '9' + responsavelViewModel.Numero;
                        }
                        Telefone telefone = new Telefone
                        {
                            CodResponsavel = responsavel.CodResponsavel,
                            Ddd = responsavelViewModel.Ddd,
                            Numero = responsavelViewModel.NumeroTelefone,
                            Tipo = responsavelViewModel.TipoTelefone,
                            Local = responsavelViewModel.LocalTelefone,
                            TipoContato = "NORMAL",
                            Pessoa = "PESSOAL",
                            Inclusao = DateTime.Now,
                            CodAcesso = usuario.CodAcesso
                        };

                        _repositorioTelefone.InserirTelefoneResponsavel(telefone);
                    }
                }
                #endregion

                #endregion

                return RedirectToAction("Details", "Alunos", new { codAluno = responsavelViewModel.CodAluno });
            }

            CarregarComboTela();
            return View(responsavelViewModel);
        }

        // GET: Responsavels/Edit/5
        public ActionResult Edit(int? codResponsavel)
        {


            if (codResponsavel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var escola = UsuarioEscola.ResgatarEscolaLogada();
            Responsavel responsavelConsulta = _repositorioGenericoResponsavel.SelecionarPorId(codResponsavel.Value);
            Aluno alunoConsulta = _repositorioGenericoAluno.SelecionarPorId(responsavelConsulta.CodAluno);
            if (alunoConsulta.CodEscola != escola.CodEscola)
            {
                return CarregarMensagemDeErro("Responsável não existe nesta escola.");
            }


            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            Logradouro logradouroResponsavelConsulta = _repositorioGenericoLogradouro.SelecionarPorId(responsavelConsulta.CodLogradouro);
            List<Telefone> telefoneResponsavelConsulta = _repositorioTelefone.SelecionarTelefoneResponsavel(responsavelConsulta.CodResponsavel);
            ResponsavelViewModel dto = mapper.Map<ResponsavelViewModel>(responsavelConsulta);
            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(alunoConsulta);
            ViewBag.Aluno = aluno;
            if (dto == null)
            {
                return HttpNotFound();
            }

            dto.Rua = logradouroResponsavelConsulta.Rua;
            dto.Numero = logradouroResponsavelConsulta.Numero;
            dto.Complemento = logradouroResponsavelConsulta.Complemento;
            dto.Referencia = logradouroResponsavelConsulta.Referencia;
            dto.Cep = logradouroResponsavelConsulta.Cep;
            dto.Bairro = logradouroResponsavelConsulta.Bairro;
            dto.Municipio = logradouroResponsavelConsulta.Municipio;
            dto.Uf = logradouroResponsavelConsulta.Uf;
            if (telefoneResponsavelConsulta.Any())
            {
                dto.NumeroTelefone = telefoneResponsavelConsulta[0].Numero;
                dto.Ddd = telefoneResponsavelConsulta[0].Ddd;
                dto.TipoTelefone = telefoneResponsavelConsulta[0].Tipo;
                dto.LocalTelefone = telefoneResponsavelConsulta[0].Local;
                dto.CodTelefone = telefoneResponsavelConsulta[0].CodTelefone;
                dto.InclusaoTelefone = telefoneResponsavelConsulta[0].Inclusao;
            }

            CarregarComboTela();

            return View(dto);
        }

        // POST: Responsavels/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ResponsavelViewModel responsavelViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                #region Alterando o Logradouro básico
                responsavelViewModel.Cep = Regex.Replace(responsavelViewModel.Cep, "[^0-9]", "");
                responsavelViewModel.Cpf = Regex.Replace(responsavelViewModel.Cpf, "[^0-9]", "");

                var logradouro = mapper.Map<ResponsavelViewModel, Logradouro>(responsavelViewModel);
                logradouro.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoLogradouro.Alterar(logradouro);
                responsavelViewModel.CodLogradouro = logradouro.CodLogradouro;

                #endregion

                #region Alterando o Responsável

                var responsavel = mapper.Map<ResponsavelViewModel, Responsavel>(responsavelViewModel);
                responsavel.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoResponsavel.Alterar(responsavel);
                #endregion

                #region Alterando o Telefone

                if (!string.IsNullOrEmpty(responsavelViewModel.NumeroTelefone))
                {
                    responsavelViewModel.Numero = Regex.Replace(responsavelViewModel.Numero, "[^0-9]", "");
                    responsavelViewModel.Ddd = Regex.Replace(responsavelViewModel.Ddd, "[^0-9]", "");
                    if (responsavelViewModel.Tipo.ToUpper().Equals("CELULAR"))
                    {
                        responsavelViewModel.Numero = '9' + responsavelViewModel.Numero;
                    }
                    Telefone telefone = new Telefone
                    {
                        Ddd = responsavelViewModel.Ddd,
                        Numero = responsavelViewModel.NumeroTelefone,
                        Tipo = responsavelViewModel.TipoTelefone,
                        Local = responsavelViewModel.LocalTelefone,
                        TipoContato = "NORMAL",
                        Pessoa = "PESSOAL",
                        CodAcesso = usuario.CodAcesso
                    };

                    if (responsavelViewModel.CodTelefone > 0)
                    {
                        telefone.CodTelefone = responsavelViewModel.CodTelefone;
                        telefone.CodResponsavel = responsavel.CodResponsavel;
                        telefone.Inclusao = responsavelViewModel.InclusaoTelefone;
                        _repositorioTelefone.Alterar(telefone);
                    }
                    else
                    {
                        telefone.Inclusao = DateTime.Now;
                        telefone.CodResponsavel = responsavel.CodResponsavel;
                        _repositorioTelefone.InserirTelefoneResponsavel(telefone);
                    }


                }

                #endregion

                return RedirectToAction("Details", "Alunos", new { codAluno = responsavel.CodAluno });
            }
            return View(responsavelViewModel);
        }

        // GET: Responsavels/Delete/5
        public ActionResult Delete(int? codResponsavel)
        {


            if (codResponsavel == null)
            {
                return CarregarMensagemDeErro("Responsável não existe nesta escola.");
            }

            var escola = UsuarioEscola.ResgatarEscolaLogada();
            Responsavel responsavelConsulta = _repositorioGenericoResponsavel.SelecionarPorId(codResponsavel.Value);
            Aluno alunoConsulta = _repositorioGenericoAluno.SelecionarPorId(responsavelConsulta.CodAluno);
            if (alunoConsulta.CodEscola != escola.CodEscola)
            {
                return CarregarMensagemDeErro("Responsável não existe nesta escola.");
            }

            var responsavelBanco = _repositorioGenericoResponsavel.SelecionarPorId(codResponsavel.Value);
            if (responsavelBanco == null)
            {
                return HttpNotFound();
            }
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            var responsavelViewModel = mapper.Map<Responsavel, ResponsavelViewModel>(responsavelBanco);
            AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(_repositorioGenericoAluno.SelecionarPorId(responsavelBanco.CodAluno));
            ViewBag.Aluno = aluno;

            CarregarComboTela();

            return View(responsavelViewModel);
        }

        // POST: Responsavels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codResponsavel)
        {
            var responsavel = _repositorioGenericoResponsavel.SelecionarPorId(codResponsavel);
            _repositorioGenericoResponsavel.ExcluirPorId(codResponsavel);
            return RedirectToAction("Details", "Alunos", new { codAluno = responsavel.CodAluno });
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
            ViewBag.DropdownEstadoCivil = Utils.CarregarListaEstadoCivil();
            ViewBag.DropdownLocalTelefone = Utils.CarregarLocalTelefone();
            ViewBag.DropdownTipoTelefone = Utils.CarregarTipoTelefone();
            ViewBag.DropdownSexo = Utils.CarregarListaSexo();
            ViewBag.DropdownUf = Utils.CarregarListaUf();
            ViewBag.DropdownSimNao = Utils.CarregarListaSimNao();
            ViewBag.DropdownTipoResponsavel = Utils.CarregarTipoResponsavel();
        }
    }
}
