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
using CEAG.NEGOCIO.Utils;
using CEAG.REPOSITORIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.Funcionario;
using CEAG.WEB.ViewModel.FuncionarioDisciplina;
using CEAG.WEB.ViewModel.Telefone;
using PagedList;

namespace CEAG.WEB.Controllers
{

    [Authorize(Roles = "ADMINISTRADOR,DIRETOR")]
    public class FuncionariosController : Controller
    {
        #region Conexões
        private RepositorioFuncionario _repositorioFuncionario = new RepositorioFuncionario(new CeagDbContext());
        private IRepositorioGenerico<Logradouro, int> _repositorioGenericoLogradouro = new RepositorioLogradouro(new CeagDbContext());
        private RepositorioTurmaFuncionarioDisciplina _repositorioTurmaFuncionarioDisciplina = new RepositorioTurmaFuncionarioDisciplina(new CeagDbContext());
        private RepositorioFuncionarioDisciplina _repositorioGenericoFuncionarioDisciplina = new RepositorioFuncionarioDisciplina(new CeagDbContext());
        private RepositorioTelefone _repositorioTelefone = new RepositorioTelefone(new CeagDbContext());
        private RepositorioTurma _repositorioTurma = new RepositorioTurma(new CeagDbContext());
      
        #endregion Conexões



        // GET: Funcionarios
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

            var funcionarios = new List<Funcionario>();
            if (!string.IsNullOrEmpty(searchString))
            {
                funcionarios = _repositorioFuncionario.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                .Where(p =>
                                p.Nome.ToUpper().Contains(searchString.ToUpper())
                                || p.Email.ToUpper().Contains(searchString.ToUpper())
                                || p.Cpf.ToUpper().Contains(searchString.ToUpper())
                                || p.Funcao.ToUpper().Contains(searchString.ToUpper())
                                || p.Titulacao.ToUpper().Contains(searchString.ToUpper())
                    ).ToList();
            }
            else
            {
                funcionarios = _repositorioFuncionario.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }
            #endregion

            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<FuncionarioExibicaoViewModel> dto = mapper.Map<List<FuncionarioExibicaoViewModel>>(funcionarios);

            #endregion

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(dto.ToPagedList(pageNumber, pageSize));
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int? codFuncionario)
        {
            

            if (codFuncionario == null)
            {
                return CarregarMensagemDeErro("Funcionário não existe nesta escola.");
            }

            var funcionarioConsulta = _repositorioFuncionario.SelecionarPorId(codFuncionario.Value);
            if (funcionarioConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Funcionário não existe nesta escola.");
            }

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            FuncionarioExibicaoViewModel dto = mapper.Map<FuncionarioExibicaoViewModel>(funcionarioConsulta);
            Logradouro logradouroAlunoConsulta = _repositorioGenericoLogradouro.SelecionarPorId(dto.CodLogradouro);

            if (dto == null)
            {
                return CarregarMensagemDeErro("Funcionário não existe nesta escola.");
            }
            dto.Rua = logradouroAlunoConsulta.Rua;
            dto.Numero = logradouroAlunoConsulta.Numero;
            dto.Complemento = logradouroAlunoConsulta.Complemento;
            dto.Referencia = logradouroAlunoConsulta.Referencia;
            dto.Cep = logradouroAlunoConsulta.Cep;
            dto.Bairro = logradouroAlunoConsulta.Bairro;
            dto.Municipio = logradouroAlunoConsulta.Municipio;
            dto.Uf = logradouroAlunoConsulta.Uf;
            dto.CodLogradouro = logradouroAlunoConsulta.CodLogradouro;
            List<TelefoneExibicaoViewModel> telefoneAlunoConsulta = mapper.Map<List<TelefoneExibicaoViewModel>>(_repositorioTelefone.SelecionarTelefoneFuncionario(dto.CodFuncionario));

            dto.Telefones = telefoneAlunoConsulta;


            return View(dto);
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            CarregarComboTela();
            return View();
        }

        // POST: Funcionarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuncionarioViewModel funcionarioViewModel)
        {
            

            funcionarioViewModel.Inclusao = DateTime.Now;
            funcionarioViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);

                #region Inserindo o Logradouro básico
                funcionarioViewModel.Cep = Regex.Replace(funcionarioViewModel.Cep, "[^0-9]", "");
                funcionarioViewModel.Cpf = Regex.Replace(funcionarioViewModel.Cpf, "[^0-9]", "");

                var logradouro = mapper.Map<FuncionarioViewModel, Logradouro>(funcionarioViewModel);
                logradouro.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoLogradouro.Inserir(logradouro);
                funcionarioViewModel.CodLogradouro = logradouro.CodLogradouro;

                #endregion

                #region Inserindo o Funcionario

                var funcionario = mapper.Map<FuncionarioViewModel, Funcionario>(funcionarioViewModel);
                funcionario.CodAcesso = usuario.CodAcesso;
                _repositorioFuncionario.Inserir(funcionario);

                #endregion

                #region Inserindo Telefone

                if (!string.IsNullOrEmpty(funcionarioViewModel.NumeroTelefone))
                {
                    funcionarioViewModel.NumeroTelefone = Regex.Replace(funcionarioViewModel.NumeroTelefone, "[^0-9]", "");
                    funcionarioViewModel.Ddd = Regex.Replace(funcionarioViewModel.Ddd, "[^0-9]", "");
                    if (funcionarioViewModel.TipoTelefone.ToUpper().Equals("CELULAR"))
                    {
                        funcionarioViewModel.NumeroTelefone = '9' + funcionarioViewModel.NumeroTelefone;
                    }
                    Telefone telefone = new Telefone
                    {
                        CodFuncionario = funcionario.CodFuncionario,
                        Ddd = funcionarioViewModel.Ddd,
                        Numero = funcionarioViewModel.NumeroTelefone,
                        Tipo = funcionarioViewModel.TipoTelefone,
                        Local = funcionarioViewModel.LocalTelefone,
                        TipoContato = "NORMAL",
                        Pessoa = "PESSOAL",
                        Inclusao = DateTime.Now,
                        CodAcesso = usuario.CodAcesso
                    };

                    _repositorioTelefone.InserirTelefoneFuncionario(telefone);
                }

                #endregion
                return RedirectToAction("Index");
            }
            CarregarComboTela();
            return View(funcionarioViewModel);
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int? codFuncionario)
        {
            

            if (codFuncionario == null)
            {
                return CarregarMensagemDeErro("Funcionário não existe nesta escola.");
            }

            var funcionarioConsulta = _repositorioFuncionario.SelecionarPorId(codFuncionario.Value);
            if (funcionarioConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Funcionário não existe nesta escola.");
            }

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            Logradouro logradouroFuncionarioConsulta = _repositorioGenericoLogradouro.SelecionarPorId(funcionarioConsulta.CodLogradouro);
            List<Telefone> telefoneFuncionarioConsulta = _repositorioTelefone.SelecionarTelefoneFuncionario(funcionarioConsulta.CodFuncionario);
            FuncionarioViewModel dto = mapper.Map<FuncionarioViewModel>(funcionarioConsulta);

            if (dto == null)
            {
                return CarregarMensagemDeErro("Funcionário não existe nesta escola.");
            }

            dto.Rua = logradouroFuncionarioConsulta.Rua;
            dto.Numero = logradouroFuncionarioConsulta.Numero;
            dto.Complemento = logradouroFuncionarioConsulta.Complemento;
            dto.Referencia = logradouroFuncionarioConsulta.Referencia;
            dto.Cep = logradouroFuncionarioConsulta.Cep;
            dto.Bairro = logradouroFuncionarioConsulta.Bairro;
            dto.Municipio = logradouroFuncionarioConsulta.Municipio;
            dto.Uf = logradouroFuncionarioConsulta.Uf;
            if (telefoneFuncionarioConsulta.Any())
            {
                dto.NumeroTelefone = telefoneFuncionarioConsulta[0].Numero;
                dto.Ddd = telefoneFuncionarioConsulta[0].Ddd;
                dto.TipoTelefone = telefoneFuncionarioConsulta[0].Tipo;
                dto.LocalTelefone = telefoneFuncionarioConsulta[0].Local;
                dto.CodTelefone = telefoneFuncionarioConsulta[0].CodTelefone;
                dto.InclusaoTelefone = telefoneFuncionarioConsulta[0].Inclusao;
            }

            CarregarComboTela();

            return View(dto);
        }

        // POST: Funcionarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FuncionarioViewModel funcionarioViewModel)
        {
            
            funcionarioViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            funcionarioViewModel.Cpf = Regex.Replace(funcionarioViewModel.Cpf, "[^0-9]", "");
           
            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);

                #region Alterando o Logradouro básico
                funcionarioViewModel.Cep = Regex.Replace(funcionarioViewModel.Cep, "[^0-9]", "");
               

                var logradouro = mapper.Map<FuncionarioViewModel, Logradouro>(funcionarioViewModel);
                logradouro.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoLogradouro.Alterar(logradouro);
                funcionarioViewModel.CodLogradouro = logradouro.CodLogradouro;

                #endregion

                #region Alterando o Funcionario

                var funcionario = mapper.Map<FuncionarioViewModel, Funcionario>(funcionarioViewModel);
                funcionario.CodAcesso = usuario.CodAcesso;
                _repositorioFuncionario.Alterar(funcionario);
                #endregion

                #region Alterando o Telefone

                if (!string.IsNullOrEmpty(funcionarioViewModel.NumeroTelefone))
                {
                    funcionarioViewModel.NumeroTelefone = Regex.Replace(funcionarioViewModel.NumeroTelefone, "[^0-9]", "");
                    funcionarioViewModel.Ddd = Regex.Replace(funcionarioViewModel.Ddd, "[^0-9]", "");
                    if (funcionarioViewModel.TipoTelefone.ToUpper().Equals("CELULAR"))
                    {
                        funcionarioViewModel.NumeroTelefone = '9' + funcionarioViewModel.NumeroTelefone;
                    }
                    Telefone telefone = new Telefone
                    {
                        Ddd = funcionarioViewModel.Ddd,
                        Numero = funcionarioViewModel.NumeroTelefone,
                        TipoContato = "NORMAL",
                        Pessoa = "PESSOAL",
                        Tipo = funcionarioViewModel.TipoTelefone,
                        Local = funcionarioViewModel.LocalTelefone,
                        CodAcesso = usuario.CodAcesso
                    };

                    if (funcionarioViewModel.CodTelefone > 0)
                    {
                        telefone.CodTelefone = funcionarioViewModel.CodTelefone;
                        telefone.CodFuncionario = funcionario.CodFuncionario;
                        telefone.Inclusao = funcionarioViewModel.InclusaoTelefone;
                        _repositorioTelefone.Alterar(telefone);
                    }
                    else
                    {
                        telefone.Inclusao = DateTime.Now;
                        telefone.CodFuncionario = funcionario.CodFuncionario;
                        _repositorioTelefone.InserirTelefoneFuncionario(telefone);
                    }

                }

                #endregion

                return RedirectToAction("Index");
            }
            return View(funcionarioViewModel);
        }


        public ActionResult DeletarFuncionario(int codFuncionario)
        {
            
            var usuario = UsuarioEscola.ResgatarUsuarioLogado();
            var funcionario = _repositorioFuncionario.SelecionarPorId(codFuncionario);
            if (funcionario.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Funcionário não existe nesta escola.");
            }

            funcionario.Cancelamento = DateTime.Now;
            funcionario.CodAcesso = usuario.CodAcesso;
            _repositorioFuncionario.Alterar(funcionario);

            return RedirectToAction("Index");
        }


        private void CarregarComboTela()
        {
            ViewBag.DropdownLocalTelefone = Utils.CarregarLocalTelefone();
            ViewBag.DropdownTipoTelefone = Utils.CarregarTipoTelefone();
            ViewBag.DropdownSexo = Utils.CarregarListaSexo();
            ViewBag.DropdownUf = Utils.CarregarListaUf();
            ViewBag.DropdownSimNao = Utils.CarregarListaSimNao(); ;
            ViewBag.DropdownTitulação = Utils.CarregarTitulacao();
        }
        public JsonResult ObterPorCpf(string cpf)
        {
            //ViewBag para listar todos os usuários
            cpf = Regex.Replace(cpf, "[^0-9]", "");
            var funcionario = _repositorioFuncionario.SelecionarPorCpf(cpf);
            var dados = (funcionario?.Cpf);

            ViewBag.FuncionarioCadastro = dados;

            return Json(dados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaFuncionario()
        {
            #region Método de consulta

            var funcionarios = new List<Funcionario>();
            funcionarios = _repositorioFuncionario.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());

            #endregion

            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<FuncionarioExibicaoViewModel> dto = mapper.Map<List<FuncionarioExibicaoViewModel>>(funcionarios);
            List<FuncionarioDisciplina> dtoFuncionarioDisciplina = _repositorioGenericoFuncionarioDisciplina.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());


            #endregion

            foreach (var itemNaTurma in dtoFuncionarioDisciplina)
            {
                foreach (var itemFuncionarioExibicao in dto)
                {
                    if (itemNaTurma.CodFuncionario == itemFuncionarioExibicao.CodFuncionario)
                    {
                        itemFuncionarioExibicao.Professor = true;
                    }
                }
            }


            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(dto);
        }

        public ActionResult ListaProfessor(int? codTurma)
        {
            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);

            #endregion

            var turma = _repositorioTurma.SelecionarPorId(codTurma.Value);
            if (turma.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Funcionário não existe nesta escola.");
            }

            var turmaFuncionarioDisciplinas = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(codTurma.Value);

            #region Método de consulta
            var funcionariosNaTurma = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(codTurma.Value);
            var funcionarioDisciplinas = new List<FuncionarioDisciplina>();
            funcionarioDisciplinas = _repositorioGenericoFuncionarioDisciplina.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            var listaFuncionarioDisciplinaExibicao = new List<FuncionarioDisciplinaExibicaoViewModel>();
            foreach (var item in funcionarioDisciplinas)
            {
                var funcionario = mapper.Map<FuncionarioExibicaoViewModel>(item.Funcionario);
                var funcionarioDisciplinaExibicao = mapper.Map<FuncionarioDisciplinaExibicaoViewModel>(item);
                funcionarioDisciplinaExibicao.Funcionario = funcionario;

                listaFuncionarioDisciplinaExibicao.Add(funcionarioDisciplinaExibicao);
            }

            List<FuncionarioDisciplinaExibicaoViewModel> odtFuncionarioDisciplinaExibicao = new List<FuncionarioDisciplinaExibicaoViewModel>();

            foreach (var codFuncionario in listaFuncionarioDisciplinaExibicao.Select(p => p.CodFuncionario).Distinct())
            {
                foreach (var professor in listaFuncionarioDisciplinaExibicao.Where(p => p.CodFuncionario == codFuncionario))
                {
                    foreach (var item in listaFuncionarioDisciplinaExibicao.Where(p => p.CodFuncionario == codFuncionario))
                    {
                        professor.Funcionario.Disciplinas += (professor.Funcionario.Disciplinas == null ? item.Disciplina.Descricao : " | " + item.Disciplina.Descricao);
                    }

                }

                odtFuncionarioDisciplinaExibicao.Add(listaFuncionarioDisciplinaExibicao.Where(p => p.CodFuncionario == codFuncionario).Take(1).SingleOrDefault());
            }

            foreach (var itemNaTurma in funcionariosNaTurma)
            {
                foreach (var itemFuncionarioExibicao in odtFuncionarioDisciplinaExibicao)
                {
                    if (itemNaTurma.CodFuncionario == itemFuncionarioExibicao.CodFuncionario)
                    {
                        itemFuncionarioExibicao.Funcionario.NaTurma = true;
                    }
                }
            }


            #endregion

            ViewBag.CodTurma = codTurma.Value;
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(odtFuncionarioDisciplinaExibicao);
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
