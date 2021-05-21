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
using CEAG.WEB.Helpers;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.Debito;
using CEAG.WEB.ViewModel.Informacao;
using CEAG.WEB.ViewModel.Responsavel;
using CEAG.WEB.ViewModel.Telefone;
using CEAG.WEB.ViewModel.Turma;
using PagedList;

namespace CEAG.WEB.Controllers
{

    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class AlunosController : Controller
    {

        // GET: Alunos
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
            var alunos = new List<Aluno>();

            using (RepositorioAluno _repositorioAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    alunos = _repositorioAlunoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                    .Where(p =>
                                    p.Nome.ToUpper().Contains(searchString.ToUpper())
                                    || (p.Matricula != null && p.Matricula.ToUpper().Contains(searchString.ToUpper()))
                                    || (p.Cpf != null && p.Cpf.ToUpper().Contains(searchString.ToUpper()))
                                    || (p.MaeNome != null && p.MaeNome.ToUpper().Contains(searchString.ToUpper()))
                                    || (p.PaiNome != null && p.PaiNome.ToUpper().Contains(searchString.ToUpper()))
                                    || (p.Responsavels.Any() && p.Responsavels.Select(t => t.Nome.ToUpper()).Contains(searchString.ToUpper()))
                        //|| p.Cpf.ToUpper().Contains(pesquisa.ToUpper())

                        ).ToList();
                }
                else
                {
                    alunos = _repositorioAlunoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
                }
            }

            #endregion

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            //List<AlunoExibicaoViewModel> dto = mapper.Map<List<AlunoExibicaoViewModel>>(alunos);
           
            List<AlunoExibicaoViewModel> dto = new List<AlunoExibicaoViewModel>();
            foreach (var item in alunos)
            {
                AlunoExibicaoViewModel aluno = mapper.Map<AlunoExibicaoViewModel>(item);
                //aluno.ListaAlunoMatriculaExibicaoViewModel = mapper.Map<List<AlunoMatriculaExibicaoViewModel>>(item.AlunoMatriculas);
                aluno.ListaTurmasJaMatriculadas = mapper.Map<List<TurmaExibicaoViewModel>>(item.AlunoMatriculas.Select(p => p.Turma));
                dto.Add(aluno);

            }

            using (RepositorioDebito _repoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                foreach (var item in dto)
                {
                    List<Debito> debitosConsulta = _repoLocal.SelecionarPorAluno(item.CodAluno);
                    item.ListaDebitosPagosAbertos = new List<DebitoExibicaoViewModel>();
                    if (debitosConsulta.Any())
                    {
                        item.ListaDebitosPagosAbertos = mapper.Map<List<DebitoExibicaoViewModel>>(debitosConsulta.OrderBy(p => p.Periodo));
                    }
                }
            }



            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(dto.ToPagedList(pageNumber, pageSize));
        }

        //GET: Alunos/Details/5
        public ActionResult Details(int? codAluno)
        {

            if (codAluno == null)
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            var alunoConsulta = new Aluno();

            using (RepositorioAluno _repositorioAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                alunoConsulta = _repositorioAlunoLocal.SelecionarPorId(codAluno.Value);
                if (alunoConsulta == null)
                {
                    return CarregarMensagemDeErro("Aluno não existe nesta escola.");
                }
            }


            if (alunoConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            AlunoExibicaoViewModel dto = mapper.Map<AlunoExibicaoViewModel>(alunoConsulta);

            Logradouro logradouroAlunoConsulta = new Logradouro();
            using (RepositorioLogradouro _repositorioGenericoLogradouroLocal = new RepositorioLogradouro(new CeagDbContext()))
            {
                logradouroAlunoConsulta = _repositorioGenericoLogradouroLocal.SelecionarPorId(dto.CodLogradouro);
            }

            if (dto == null)
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
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
            List<TelefoneExibicaoViewModel> telefoneAlunoConsulta = new List<TelefoneExibicaoViewModel>();
            using (RepositorioTelefone _repositorioTelefoneLocal = new RepositorioTelefone(new CeagDbContext()))
            {
                telefoneAlunoConsulta = mapper.Map<List<TelefoneExibicaoViewModel>>(_repositorioTelefoneLocal.SelecionarTelefoneAluno(dto.CodAluno));
            }

            List<Responsavel> responsavels = new List<Responsavel>();
            using (RepositorioResponsavel _repositorioGenericoResponsavelLocal = new RepositorioResponsavel(new CeagDbContext()))
            {
                responsavels = _repositorioGenericoResponsavelLocal.Selecionar(dto.CodAluno);
            }

            List<ResponsavelExibicaoViewModel> responsavelAlunoConsulta = mapper.Map<List<ResponsavelExibicaoViewModel>>(responsavels);

            List<InformacaoExibicaoViewModel> informacaoAlunoConsulta = new List<InformacaoExibicaoViewModel>();

            using (RepositorioInformacao _repositorioGenericoInformacaoLocal = new RepositorioInformacao(new CeagDbContext()))
            {
                informacaoAlunoConsulta = mapper.Map<List<InformacaoExibicaoViewModel>>(_repositorioGenericoInformacaoLocal.Selecionar(dto.CodAluno));
            }

            List<AlunoMatriculaExibicaoViewModel> alunoMatriculaExibicaoViewModels = new List<AlunoMatriculaExibicaoViewModel>();

            using (RepositorioTurma _repositorioTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                foreach (var item in alunoConsulta.AlunoMatriculas)
                {
                    var turmaForeach = _repositorioTurmaLocal.SelecionarPorId(item.CodTurma);
                    AlunoMatriculaExibicaoViewModel alunoMatriculaExibicaoViewModel = mapper.Map<AlunoMatriculaExibicaoViewModel>(item);
                    alunoMatriculaExibicaoViewModel.Turma = mapper.Map<TurmaExibicaoViewModel>(turmaForeach);

                    alunoMatriculaExibicaoViewModels.Add(alunoMatriculaExibicaoViewModel);
                }
            }

            List<ParentescoAluno> parentes = new List<ParentescoAluno>();
            using (RepositorioParentescoAluno _repositorioParentescoLocal = new RepositorioParentescoAluno(new CeagDbContext()))
            {
                parentes = _repositorioParentescoLocal.SelecionarParentesPorAluno(alunoConsulta.CodAluno);
            }

            List<AlunoExibicaoViewModel> alunoParenteExibicaoViewModel = new List<AlunoExibicaoViewModel>();
            using (RepositorioAluno _repoLocalAluno = new RepositorioAluno(new CeagDbContext()))
            {
                foreach (var item in parentes)
                {
                    Aluno aluno = _repoLocalAluno.SelecionarPorId(item.CodAluno.Value);
                    AlunoExibicaoViewModel alunoParente = mapper.Map<AlunoExibicaoViewModel>(aluno);
                    alunoParente.NivelParentesco = item.Parentesco.Descricao;
                    alunoParente.CodParentescoAluno = item.CodParentescoAluno;
                    foreach (var itemMatriculas in aluno.AlunoMatriculas)
                    {
                        alunoParente.ListaTurmasJaMatriculadas.Add(mapper.Map<TurmaExibicaoViewModel>(itemMatriculas.Turma));
                    }

                    //alunoParente.Matriculado = AlunoJaTemMatricula(item.CodAluno.Value).CodTurma > 0;
                    alunoParenteExibicaoViewModel.Add(alunoParente);
                }
            }


            List<AlunoExibicaoViewModel> listaParaTela = new List<AlunoExibicaoViewModel>();

            if (alunoParenteExibicaoViewModel.Any())
            {
                listaParaTela = alunoParenteExibicaoViewModel.Where(p => p.CodAluno != codAluno.Value).ToList();
            }
               

            dto.Parentes = listaParaTela;
            dto.TelefoneViewModels = telefoneAlunoConsulta;
            dto.InformacoeViewModels = informacaoAlunoConsulta;
            dto.ResponsavelViewModels = responsavelAlunoConsulta;
            dto.ListaAlunoMatriculaExibicaoViewModel = alunoMatriculaExibicaoViewModels;

            return View(dto);
        }

        // GET: Alunos/Create
        public ActionResult Create()
        {

            Matricula matriculaConsulta = new Matricula();
            Matricula matriculaNova = new Matricula();

            using (RepositorioMatricula _repositorioMatriculaLocal = new RepositorioMatricula(new CeagDbContext()))
            {
                var escola = UsuarioEscola.ResgatarEscolaLogada();
                ViewBag.CodEscola = escola.CodEscola;
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();

                matriculaConsulta = _repositorioMatriculaLocal.SelecionarUltimaMatricula(escola.CodEscola);

                matriculaNova.Ano = DateTime.Now.Year;
                matriculaNova.Status = "PENDENTE";
                matriculaNova.Inclusao = DateTime.Now;
                matriculaNova.CodEscola = 1;
                matriculaNova.NumeroMatricula = GerarCodigoMatricula(matriculaConsulta);
                matriculaNova.Ordem = matriculaConsulta == null ? 1 : matriculaConsulta.Ordem + 1;
                matriculaNova.CodAcesso = usuario.CodAcesso;

                _repositorioMatriculaLocal.Inserir(matriculaNova);
            }

            ViewBag.NumeroMatricula = matriculaNova.NumeroMatricula;
            CarregarComboTela();
            return View();
        }

        private string GerarCodigoMatricula(Matricula matriculaConsulta)
        {
            var escola = UsuarioEscola.ResgatarEscolaLogada();
            return escola.Fantasia + DateTime.Now.Year.ToString() + Convert.ToString(matriculaConsulta == null ? "001" : (matriculaConsulta.Ordem + 1).ToString().PadLeft(3, '0'));
        }

        // POST: Alunos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlunoViewModel alunoViewModel)//[Bind(Include = "CodAluno,Matricula,Nome,Cpf,Rg,RgUf,RgOrgaoEmissor,Nascimento,Inclusao,Email,MaeNome,MaeProfissao,PaiNome,PaiProfissao,CodEscola,CodLograduro")] Aluno aluno)
        {

            alunoViewModel.Inclusao = DateTime.Now;
            alunoViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();


            if (ModelState.IsValid)
            {
                #region Mapper

                Mapper mapper = Constants.Utils.ViewModelParaDominio();

                #endregion

                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                #region Inserindo o Logradouro básico
                if (!string.IsNullOrEmpty(alunoViewModel.Cep))
                {
                    alunoViewModel.Cep = Regex.Replace(alunoViewModel.Cep, "[^0-9]", "");
                }
                if (!string.IsNullOrEmpty(alunoViewModel.Cpf))
                {
                    alunoViewModel.Cpf = Regex.Replace(alunoViewModel.Cpf, "[^0-9]", "");
                }

                var logradouro = mapper.Map<AlunoViewModel, Logradouro>(alunoViewModel);

                logradouro.CodAcesso = usuario.CodAcesso;

                using (RepositorioLogradouro _repositorioGenericoLogradouroLocal = new RepositorioLogradouro(new CeagDbContext()))
                {
                    _repositorioGenericoLogradouroLocal.Inserir(logradouro);
                }

                alunoViewModel.CodLogradouro = logradouro.CodLogradouro;

                #endregion

                #region Inserindo o Aluno

                var aluno = mapper.Map<AlunoViewModel, Aluno>(alunoViewModel);
                aluno.CodAcesso = usuario.CodAcesso;

                using (RepositorioAluno _repositorioAlunoLocal = new RepositorioAluno(new CeagDbContext()))
                {
                    _repositorioAlunoLocal.Inserir(aluno);
                }


                #endregion

                #region Inserindo Telefone

                if (!string.IsNullOrEmpty(alunoViewModel.NumeroTelefone))
                {
                    alunoViewModel.NumeroTelefone = Regex.Replace(alunoViewModel.NumeroTelefone, "[^0-9]", "");
                    alunoViewModel.Ddd = Regex.Replace(alunoViewModel.Ddd, "[^0-9]", "");
                    if (alunoViewModel.TipoTelefone.ToUpper().Equals("CELULAR"))
                    {
                        alunoViewModel.NumeroTelefone = '9' + alunoViewModel.NumeroTelefone;
                    }
                    Telefone telefone = new Telefone
                    {
                        CodAluno = aluno.CodAluno,
                        Ddd = alunoViewModel.Ddd,
                        Numero = alunoViewModel.NumeroTelefone,
                        Tipo = alunoViewModel.TipoTelefone,
                        Local = alunoViewModel.LocalTelefone,
                        TipoContato = "NORMAL",
                        Pessoa = "PESSOAL",
                        Inclusao = DateTime.Now,
                        CodAcesso = usuario.CodAcesso
                    };

                    using (RepositorioTelefone _repositorioTelefoneLocal = new RepositorioTelefone(new CeagDbContext()))
                    {
                        _repositorioTelefoneLocal.InserirTelefoneAluno(telefone);
                    }
                }

                #endregion
                return RedirectToAction("Details", "Alunos", new { codAluno = aluno.CodAluno });
            }
            CarregarComboTela();
            // ViewBag.CodEscola = new SelectList(db.Escolas, "CodEscola", "Razao", aluno.CodEscola);
            ViewBag.NumeroMatricula = alunoViewModel.Matricula;
            return View(alunoViewModel);
        }

        // GET: Alunos/Edit/5
        public ActionResult Edit(int? codAluno)
        {

            if (codAluno == null)
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }


            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion


            // Validando se o aluno é da Escola
            Aluno alunoConsulta = new Aluno();
            using (RepositorioAluno _repositorioAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                alunoConsulta = _repositorioAlunoLocal.SelecionarPorId(codAluno.Value);
            }

            if (alunoConsulta == null)
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            if (alunoConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            Logradouro logradouroAlunoConsulta = new Logradouro();
            using (RepositorioLogradouro _repositorioGenericoLogradouroLocal = new RepositorioLogradouro(new CeagDbContext()))
            {
                logradouroAlunoConsulta = _repositorioGenericoLogradouroLocal.SelecionarPorId(alunoConsulta.CodLogradouro);
            }

            List<Telefone> telefoneAlunoConsulta = new List<Telefone>();
            using (RepositorioTelefone _repositorioTelefoneLocal = new RepositorioTelefone(new CeagDbContext()))
            {
                telefoneAlunoConsulta = _repositorioTelefoneLocal.SelecionarTelefoneAluno(alunoConsulta.CodAluno);
            }


            AlunoViewModel dto = mapper.Map<AlunoViewModel>(alunoConsulta);

            if (dto == null)
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            dto.Rua = logradouroAlunoConsulta.Rua;
            dto.Numero = logradouroAlunoConsulta.Numero;
            dto.Complemento = logradouroAlunoConsulta.Complemento;
            dto.Referencia = logradouroAlunoConsulta.Referencia;
            dto.Cep = logradouroAlunoConsulta.Cep;
            dto.Bairro = logradouroAlunoConsulta.Bairro;
            dto.Municipio = logradouroAlunoConsulta.Municipio;
            dto.Uf = logradouroAlunoConsulta.Uf;
            if (telefoneAlunoConsulta.Any())
            {
                dto.NumeroTelefone = telefoneAlunoConsulta[0].Numero;
                dto.Ddd = telefoneAlunoConsulta[0].Ddd;
                dto.TipoTelefone = telefoneAlunoConsulta[0].Tipo;
                dto.LocalTelefone = telefoneAlunoConsulta[0].Local;
                dto.CodTelefone = telefoneAlunoConsulta[0].CodTelefone;
                dto.InclusaoTelefone = telefoneAlunoConsulta[0].Inclusao;
                // dto.TipoContato = telefoneAlunoConsulta[0].TipoContato;
            }

            CarregarComboTela();

            return View(dto);
        }

        // POST: Alunos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlunoViewModel alunoViewModel)
        {
            var escola = UsuarioEscola.ResgatarEscolaLogada();
            alunoViewModel.CodEscola = escola.CodEscola;

            if (!string.IsNullOrEmpty(alunoViewModel.Cep))
            {
                alunoViewModel.Cep = Regex.Replace(alunoViewModel.Cep, "[^0-9]", "");
            }
            if (!string.IsNullOrEmpty(alunoViewModel.Cpf))
            {
                alunoViewModel.Cpf = Regex.Replace(alunoViewModel.Cpf, "[^0-9]", "");
            }




            if (ModelState.IsValid)
            {

                #region Mapper

                Mapper mapper = Constants.Utils.ViewModelParaDominio();

                #endregion


                var usuario = UsuarioEscola.ResgatarUsuarioLogado();

                #region Alterando o Logradouro básico
                alunoViewModel.Cep = Regex.Replace(alunoViewModel.Cep, "[^0-9]", "");


                var logradouro = mapper.Map<AlunoViewModel, Logradouro>(alunoViewModel);
                logradouro.CodAcesso = usuario.CodAcesso;

                using (RepositorioLogradouro _repositorioGenericoLogradouroLocal = new RepositorioLogradouro(new CeagDbContext()))
                {
                    _repositorioGenericoLogradouroLocal.Alterar(logradouro);
                }

                alunoViewModel.CodLogradouro = logradouro.CodLogradouro;

                #endregion

                #region Alterando o Aluno

                var aluno = mapper.Map<AlunoViewModel, Aluno>(alunoViewModel);
                aluno.CodAcesso = usuario.CodAcesso;
                using (RepositorioAluno _repositorioAlunoLocal = new RepositorioAluno(new CeagDbContext()))
                {
                    _repositorioAlunoLocal.Alterar(aluno);
                }

                #endregion

                #region Alterando o Telefone

                if (!string.IsNullOrEmpty(alunoViewModel.NumeroTelefone))
                {
                    alunoViewModel.NumeroTelefone = Regex.Replace(alunoViewModel.NumeroTelefone, "[^0-9]", "");
                    alunoViewModel.Ddd = Regex.Replace(alunoViewModel.Ddd, "[^0-9]", "");
                    if (alunoViewModel.TipoTelefone.ToUpper().Equals("CELULAR"))
                    {
                        alunoViewModel.NumeroTelefone = '9' + alunoViewModel.NumeroTelefone;
                    }
                    Telefone telefone = new Telefone
                    {
                        Ddd = alunoViewModel.Ddd,
                        Numero = alunoViewModel.NumeroTelefone,
                        Tipo = alunoViewModel.TipoTelefone,
                        Local = alunoViewModel.LocalTelefone,
                        TipoContato = "NORMAL",
                        Pessoa = "PESSOAL",
                        CodAcesso = usuario.CodAcesso
                    };

                    if (alunoViewModel.CodTelefone > 0)
                    {
                        telefone.CodTelefone = alunoViewModel.CodTelefone;
                        telefone.CodAluno = aluno.CodAluno;
                        telefone.Inclusao = alunoViewModel.InclusaoTelefone;
                        using (RepositorioTelefone _repositorioTelefoneLocal = new RepositorioTelefone(new CeagDbContext()))
                        {
                            _repositorioTelefoneLocal.Alterar(telefone);
                        }
                    }
                    else
                    {
                        telefone.Inclusao = DateTime.Now;
                        telefone.CodAluno = aluno.CodAluno;
                        using (RepositorioTelefone _repositorioTelefoneLocal = new RepositorioTelefone(new CeagDbContext()))
                        {
                            _repositorioTelefoneLocal.InserirTelefoneAluno(telefone);
                        }
                    }
                }

                #endregion

                return RedirectToAction("Details", "Alunos", new { codAluno = aluno.CodAluno });
            }
            return View(alunoViewModel);
        }

        public ActionResult Informacoes(int codAluno)
        {
            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion


            Aluno alunoConsulta = new Aluno();
            using (RepositorioAluno _repositorioAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                alunoConsulta = _repositorioAlunoLocal.SelecionarPorId(codAluno);
            }

            if (alunoConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            AlunoExibicaoViewModel dto = mapper.Map<AlunoExibicaoViewModel>(alunoConsulta);
            return View(dto);
        }

        private void CarregarComboTela()
        {
            ViewBag.DropdownLocalTelefone = Utils.CarregarLocalTelefone();
            ViewBag.DropdownTipoTelefone = Utils.CarregarTipoTelefone();
            ViewBag.DropdownSexo = Utils.CarregarListaSexo();
            ViewBag.DropdownSimNao = Utils.CarregarListaSimNao();
            ViewBag.DropdownTipoResponsavel = Utils.CarregarTipoResponsavel();
            ViewBag.DropdownUf = Utils.CarregarListaUf();
        }

        public JsonResult ObterPorCpf(string cpf)
        {
            //ViewBag para listar todos os usuários
            cpf = Regex.Replace(cpf, "[^0-9]", "");
            var alunos = new Aluno();
            using (RepositorioAluno _repositorioAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                alunos = _repositorioAlunoLocal.SelecionarPorCpf(cpf);
            }

            var dados = (alunos?.Cpf);

            return Json(dados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterListaAluno(string busca)
        {
            //ViewBag para listar todos os usuários
            var alunos = new List<Aluno>();
            using (RepositorioAluno _repositorioAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                alunos = _repositorioAlunoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }

            List<Aluno> dados = alunos.Where(p => p.Nome.ToUpper().Contains(busca.ToUpper())).ToList();

            List<SelectListItem> lista = (from Aluno d in dados
                                          select new SelectListItem
                                          {
                                              Text = d.Nome + " - MÃE: " + d.MaeNome,
                                              Value = d.CodAluno.ToString()
                                          }).ToList();
            var js = new SelectList(lista, "Value", "Text");



            return Json(js, JsonRequestBehavior.AllowGet);
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


        private Turma AlunoJaTemMatricula(int codAluno)
        {
            Turma retorno = new Turma(); ;
            var matriculasPorAluno = new List<AlunoMatricula>();
            using (RepositorioAlunoMatricula _repositorioGenericoAlunoMatriculaLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
            {
                matriculasPorAluno = _repositorioGenericoAlunoMatriculaLocal.SelecionarPorIdAluno(codAluno);
            }

            //if (matriculasPorAluno.Any())
            //{
            //    Turma turma = new Turma();
            //    using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            //    {
            //        turma = _repositorioGenericoTurmaLocal.SelecionarPorId(matriculasPorAluno.OrderByDescending(p => p.Ano).FirstOrDefault().CodTurma);
            //    }
            //}
            return retorno;
        }

    }

}
