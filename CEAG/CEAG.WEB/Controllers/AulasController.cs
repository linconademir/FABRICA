using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.NEGOCIO.Enum;
using CEAG.NEGOCIO.Utils;
using CEAG.REPOSITORIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Acesso;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.Aula;
using CEAG.WEB.ViewModel.AulaAluno;
using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Horario;
using CEAG.WEB.ViewModel.Turma;
using CEAG.WEB.ViewModel.TurmaFuncionarioDisciplinas;
using PagedList;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR,PROFESSOR")]
    public class AulasController : Controller
    {
        private RepositorioAula _repositorioAula = new RepositorioAula(new CeagDbContext());
        private RepositorioUnidade _repositorioUnidade = new RepositorioUnidade(new CeagDbContext());
        private RepositorioTurma _repositorioTurma = new RepositorioTurma(new CeagDbContext());
        private RepositorioAulaAluno _repositorioAulaAluno = new RepositorioAulaAluno(new CeagDbContext());
        private RepositorioHorarioAula _repositorioHorarioAula = new RepositorioHorarioAula(new CeagDbContext());
        private RepositorioDisciplina _repositorioDisciplina = new RepositorioDisciplina(new CeagDbContext());
        private RepositorioAlunoMatricula _repositorioAlunoMatricula = new RepositorioAlunoMatricula(new CeagDbContext());
        private RepositorioTurmaFuncionarioDisciplina _repositorioTurmaFuncionarioDisciplina = new RepositorioTurmaFuncionarioDisciplina(new CeagDbContext());
        private RepositorioFeriado _repositorioFeriado = new RepositorioFeriado(new CeagDbContext());

        // GET: Aulas
        public ActionResult Index(string sortOrder, string currentFilter, string searchAno, string searchString, int? page, int codTurma, int? searchmes)
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

            var aulas = new List<Aula>();
            if (!string.IsNullOrEmpty(searchString))
            {
                aulas = _repositorioAula.SelecionarPorIdTurma(codTurma)
                        .Where(p => ((searchmes.HasValue && searchmes.Value > 0 && p.Realizada.Month == searchmes) || !searchmes.HasValue || searchmes == 0)
                                    &&
                                        (
                                            (!string.IsNullOrEmpty(p.Assunto) && p.Assunto.ToUpper().Contains(searchString.ToUpper()))
                                            || p.Disciplina.Descricao.ToUpper().Contains(searchString.ToUpper())
                                        )
                                       

                        ).OrderByDescending(p => p.Realizada).ToList();
            }
            else
            {
                aulas = _repositorioAula.SelecionarPorIdTurma(codTurma)
                    .Where(p => ((searchmes.HasValue && searchmes.Value > 0 && p.Realizada.Month == searchmes) || !searchmes.HasValue || searchmes == 0))
                    .OrderByDescending(p => p.Realizada).ToList();
            }


            #endregion

            #region Mapper


            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion



            List<AulaExibicaoViewModel> dto = new List<AulaExibicaoViewModel>();
            foreach (var item in aulas)
            {
                AulaExibicaoViewModel aulaExibicao = new AulaExibicaoViewModel();
                aulaExibicao = mapper.Map<AulaExibicaoViewModel>(item);
                aulaExibicao.AulaAlunos = mapper.Map<List<AulaAlunoExibicaoViewModel>>(item.AulaAlunos);
                dto.Add(aulaExibicao);

            }
            #endregion
            CarregarComboTela();
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.CodTurma = codTurma;
            ViewBag.Mes = searchmes;
            return View(dto.ToPagedList(pageNumber, pageSize));
        }

        // GET: Aulas/Details/5
        public ActionResult Details(int? codAula)
        {

            if (codAula == null)
            {
                return CarregarMensagemDeErro("Aula não existe nesta Turma.");
            }

            Aula aulaConsulta = _repositorioAula.SelecionarPorId(codAula.Value);

            if (aulaConsulta == null)
            {
                return CarregarMensagemDeErro("Aula não existe nesta Turma.");
            }

            if (aulaConsulta.Turma.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Aula não existe nesta Turma.");
            }


            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion



            AulaViewModel dto = new AulaViewModel();
            dto = mapper.Map<AulaViewModel>(aulaConsulta);
            dto.AulaAlunos = new List<AulaAlunoExibicaoViewModel>();
            dto.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(aulaConsulta.Disciplina);
            dto.TurmaExibicaoViewModel = mapper.Map<TurmaExibicaoViewModel>(aulaConsulta.Turma);

            var listaAluno = _repositorioAlunoMatricula.SelecionarPorIdTurma(aulaConsulta.CodTurma);

            foreach (var item in aulaConsulta.AulaAlunos)
            {
                AulaAlunoExibicaoViewModel aulaAlunoExibicao = mapper.Map<AulaAlunoExibicaoViewModel>(item);
                aulaAlunoExibicao.AlunoMatriculaExibicaoViewModel = mapper.Map<AlunoMatriculaExibicaoViewModel>(item.AlunoMatricula);
                aulaAlunoExibicao.AlunoMatriculaExibicaoViewModel.Aluno = mapper.Map<AlunoExibicaoViewModel>(item.AlunoMatricula.Aluno);

                dto.AulaAlunos.Add(aulaAlunoExibicao);
            }

            List<AlunoMatriculaExibicaoViewModel> listaAlunosExibicaoViewModel = new List<AlunoMatriculaExibicaoViewModel>();
            foreach (var item in listaAluno)
            {
                AlunoMatriculaExibicaoViewModel aluno = mapper.Map<AlunoMatriculaExibicaoViewModel>(item);
                aluno.Aluno = mapper.Map<AlunoExibicaoViewModel>(item.Aluno);
                listaAlunosExibicaoViewModel.Add(aluno);
            }

            var horarioAula = _repositorioHorarioAula.SelecionarPorId(dto.CodHorarioAula);

            var escola = UsuarioEscola.ResgatarEscolaLogada();
            ViewBag.HorarioAula = mapper.Map<HorarioAulaViewModel>(horarioAula);
            ViewBag.ListaAlunos = listaAlunosExibicaoViewModel;

            ViewBag.Escola = escola;
            return View(dto);
        }

        // GET: Aulas/Create
        public ActionResult Create(int codTurma, int codDisciplina, int codHorarioAula, DateTime? realizada)
        {
            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion


            var horarioAula = _repositorioHorarioAula.SelecionarPorId(codHorarioAula);
            var listaAluno = _repositorioAlunoMatricula.SelecionarPorIdTurma(codTurma);

            var turmaConsulta = _repositorioTurma.SelecionarPorId(codTurma);
            var disciplinaConsulta = _repositorioDisciplina.SelecionarPorId(codDisciplina);

            if (disciplinaConsulta == null || turmaConsulta == null)
            {
                return CarregarMensagemDeErro("Turma não existe nesta Escola.");
            }

            if (turmaConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Turma não existe nesta Escola.");
            }

            if (disciplinaConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Disciplina não existe nesta Escola.");
            }

            if (!listaAluno.Any())
            {
                return CarregarMensagemDeErro("Nao existem alunos matriculados na turma "+turmaConsulta.Descricao);
            }

            List<AlunoMatriculaExibicaoViewModel> listaAlunosExibicaoViewModel = new List<AlunoMatriculaExibicaoViewModel>();
            TurmaExibicaoViewModel turma = new TurmaExibicaoViewModel();
            foreach (var item in listaAluno)
            {
                AlunoMatriculaExibicaoViewModel aluno = mapper.Map<AlunoMatriculaExibicaoViewModel>(item);
                aluno.Aluno = mapper.Map<AlunoExibicaoViewModel>(item.Aluno);

                if (turma == null)
                {
                    turma = mapper.Map<TurmaExibicaoViewModel>(item.Turma);
                }

                listaAlunosExibicaoViewModel.Add(aluno);
            }

            var disciplina = _repositorioDisciplina.SelecionarPorId(codDisciplina);



            ViewBag.CodDisciplina = codDisciplina;
            ViewBag.CodTurma = codTurma;
            ViewBag.CodHorarioAula = horarioAula.CodHorarioAula;
            ViewBag.Realizada = realizada;
            ViewBag.ListaAlunos = listaAlunosExibicaoViewModel;
            ViewBag.Disciplina = disciplina;
            ViewBag.Turma = listaAluno[0].Turma;
            ViewBag.HorarioAula = mapper.Map<HorarioAulaViewModel>(horarioAula);
            ViewBag.MesNome = realizada.Value.ToString("MMMM", new CultureInfo("PT-pt")).ToUpper();

            return View();
        }

        // POST: Aulas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AulaViewModel aulaViewModel, IEnumerable<int> IdsAluno)
        {

            aulaViewModel.Inclusao = DateTime.Now;

            if (ModelState.IsValid)
            {
                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion

                var aula = mapper.Map<AulaViewModel, Aula>(aulaViewModel);
                var listaAlunoTurma = _repositorioAlunoMatricula.SelecionarPorIdTurma(aulaViewModel.CodTurma);
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                aula.CodAcesso = usuario.CodAcesso;
                _repositorioAula.Inserir(aula);

                foreach (var item in listaAlunoTurma)
                {
                    AulaAluno aulaAluno = new AulaAluno();
                    aulaAluno.CodAlunoMatricula = item.CodAlunoMatricula;
                    aulaAluno.CodAula = aula.CodAula;
                    aulaAluno.Inclusao = DateTime.Now;
                    if (IdsAluno == null)
                    {
                        aulaAluno.Situacao = EnumComum.Chamada.AUSENTE.ToString();
                    }
                    else
                    {
                        if (IdsAluno.Contains(item.CodAlunoMatricula))
                        {
                            aulaAluno.Situacao = EnumComum.Chamada.PRESENTE.ToString();
                        }
                        else
                        {
                            aulaAluno.Situacao = EnumComum.Chamada.AUSENTE.ToString();
                        }
                    }
                    aulaAluno.CodAcesso = usuario.CodAcesso;
                    _repositorioAulaAluno.Inserir(aulaAluno);
                }

                return RedirectToAction("Index", "Aulas", new { codTurma = aulaViewModel.CodTurma });
            }

            return View(aulaViewModel);
        }

        // GET: Aulas/Edit/5
        public ActionResult Edit(int? codAula)
        {

            if (codAula == null)
            {
                return CarregarMensagemDeErro("Aula não existe nesta Turma.");
            }

            Aula aulaConsulta = _repositorioAula.SelecionarPorId(codAula.Value);
            if (aulaConsulta == null)
            {
                return CarregarMensagemDeErro("Aula não existe nesta Turma.");
            }
            if (aulaConsulta.Turma.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Aula não existe nesta Turma.");
            }


            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion



            AulaViewModel dto = new AulaViewModel();
            dto = mapper.Map<AulaViewModel>(aulaConsulta);
            dto.AulaAlunos = new List<AulaAlunoExibicaoViewModel>();
            dto.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(aulaConsulta.Disciplina);
            dto.TurmaExibicaoViewModel = mapper.Map<TurmaExibicaoViewModel>(aulaConsulta.Turma);

            var listaAluno = _repositorioAlunoMatricula.SelecionarPorIdTurma(aulaConsulta.CodTurma);

            foreach (var item in aulaConsulta.AulaAlunos)
            {
                AulaAlunoExibicaoViewModel aulaAlunoExibicao = mapper.Map<AulaAlunoExibicaoViewModel>(item);
                aulaAlunoExibicao.AlunoMatriculaExibicaoViewModel = mapper.Map<AlunoMatriculaExibicaoViewModel>(item.AlunoMatricula);
                aulaAlunoExibicao.AlunoMatriculaExibicaoViewModel.Aluno = mapper.Map<AlunoExibicaoViewModel>(item.AlunoMatricula.Aluno);

                dto.AulaAlunos.Add(aulaAlunoExibicao);
            }

            List<AlunoMatriculaExibicaoViewModel> listaAlunosExibicaoViewModel = new List<AlunoMatriculaExibicaoViewModel>();
            foreach (var item in listaAluno)
            {
                AlunoMatriculaExibicaoViewModel aluno = mapper.Map<AlunoMatriculaExibicaoViewModel>(item);
                aluno.Aluno = mapper.Map<AlunoExibicaoViewModel>(item.Aluno);
                listaAlunosExibicaoViewModel.Add(aluno);
            }

            var horarioAula = _repositorioHorarioAula.SelecionarPorId(dto.CodHorarioAula);

            var escola = UsuarioEscola.ResgatarEscolaLogada();
            ViewBag.HorarioAula = mapper.Map<HorarioAulaViewModel>(horarioAula);
            ViewBag.ListaAlunos = listaAlunosExibicaoViewModel;

            ViewBag.Escola = escola;
            return View(dto);
        }

        //// POST: Aulas/Edit/5
        //// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        //// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AulaViewModel aulaViewModel, IEnumerable<int> IdsAluno)
        {

            aulaViewModel.Inclusao = DateTime.Now;

            if (ModelState.IsValid)
            {

                #region Mapper

                Mapper mapper = Constants.Utils.ViewModelParaDominio();

                #endregion


                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var aula = mapper.Map<AulaViewModel, Aula>(aulaViewModel);

                aula.CodAcesso = usuario.CodAcesso;
                _repositorioAula.Alterar(aula);

                var listaAulaAluno = _repositorioAula.SelecionarPorId(aula.CodAula);

                foreach (var item in listaAulaAluno.AulaAlunos)
                {
                    _repositorioAulaAluno.ExcluirPorId(item.CodAulaAluno);
                }

                var listaAlunoTurma = _repositorioAlunoMatricula.SelecionarPorIdTurma(aulaViewModel.CodTurma);

                foreach (var item in listaAlunoTurma)
                {
                    AulaAluno aulaAluno = new AulaAluno();
                    aulaAluno.CodAlunoMatricula = item.CodAlunoMatricula;
                    aulaAluno.CodAula = aula.CodAula;
                    aulaAluno.Inclusao = DateTime.Now;
                    if (IdsAluno == null)
                    {
                        aulaAluno.Situacao = EnumComum.Chamada.AUSENTE.ToString();
                    }
                    else
                    {
                        if (IdsAluno.Contains(item.CodAlunoMatricula))
                        {
                            aulaAluno.Situacao = EnumComum.Chamada.PRESENTE.ToString();
                        }
                        else
                        {
                            aulaAluno.Situacao = EnumComum.Chamada.AUSENTE.ToString();
                        }
                    }
                    aulaAluno.CodAcesso = usuario.CodAcesso;
                    _repositorioAulaAluno.Inserir(aulaAluno);
                }

                return RedirectToAction("Index", "Aulas", new { codTurma = aulaViewModel.CodTurma });
            }

            return View(aulaViewModel);

        }


        // POST: Aulas/Delete/5
        public ActionResult DeleteConfirmed(int codAula)
        {

            var aulaAlunos = _repositorioAula.SelecionarPorId(codAula);
            var codTurma = aulaAlunos.CodTurma;
            if (aulaAlunos.Turma.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Aula não existe nesta Turma.");
            }

            foreach (var item in aulaAlunos.AulaAlunos)
            {
                RepositorioAulaAluno _repoLocal = new RepositorioAulaAluno(new CeagDbContext());
                var aulaAluno = _repoLocal.SelecionarPorId(item.CodAulaAluno);
                _repoLocal.Excluir(aulaAluno);
            }


            RepositorioAula _repoLocalAula = new RepositorioAula(new CeagDbContext());
            var aula = _repoLocalAula.SelecionarPorId(codAula);
            _repoLocalAula.Excluir(aula);

            return RedirectToAction("Index", "Aulas", new { codTurma });
        }


        public ActionResult ListaDisciplina(int codTurma)
        {

            #region Método de consulta
            var turma = _repositorioTurma.SelecionarPorId(codTurma);
            AcessoViewModel usuario = UsuarioEscola.ResgatarUsuarioLogado();

            if (turma.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            var alunoMatriculas = _repositorioAlunoMatricula.SelecionarPorIdTurma(codTurma).ToList();
            var listTurmaFuncionarioDisciplina = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(codTurma);

            var acessoViewModel = UsuarioEscola.ResgatarUsuarioLogado();

            if (acessoViewModel.CodFuncionario > 0)
            {
                listTurmaFuncionarioDisciplina.Where(p => p.CodFuncionario == acessoViewModel.CodFuncionario);
            }
            #endregion

            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<TurmaFuncionarioDisciplinaExibicaoViewModel> dto = new List<TurmaFuncionarioDisciplinaExibicaoViewModel>();
            if (UsuarioEscola.ResgatarUsuarioLogado().AcessoRoles[0].Role.Descricao.Equals("PROFESSOR"))
            {
                foreach (var item in listTurmaFuncionarioDisciplina
                        .Where(p => usuario.TurmaFuncionarioDisciplinas
                        .Select(s => s.CodDisciplina).Contains(p.CodDisciplina)))
                {
                    var turmaFuncionaDisciplina = item;
                    if (!dto.Where(p => p.CodDisciplina == turmaFuncionaDisciplina.CodDisciplina).Any())
                    {
                        dto.Add(mapper.Map<TurmaFuncionarioDisciplinaExibicaoViewModel>(turmaFuncionaDisciplina));
                    }
                }
            }
            else
            {
                foreach (var item in listTurmaFuncionarioDisciplina)
                {
                    var turmaFuncionaDisciplina = item;
                    if (!dto.Where(p => p.CodDisciplina == turmaFuncionaDisciplina.CodDisciplina).Any())
                    {
                        dto.Add(mapper.Map<TurmaFuncionarioDisciplinaExibicaoViewModel>(turmaFuncionaDisciplina));
                    }
                }
            }
            #endregion

            return View(dto);
        }


        public ActionResult ListaDia(int? codTurma, int? codDisciplina, DateTime? anoMes)
        {
            anoMes = (anoMes == null ? DateTime.Now : anoMes);

            var turmaConsulta = _repositorioTurma.SelecionarPorId(codTurma.Value);
            var disciplinaConsulta = _repositorioDisciplina.SelecionarPorId(codDisciplina.Value);
            if (disciplinaConsulta == null || turmaConsulta == null)
            {
                return CarregarMensagemDeErro("Turma não existe nesta Escola.");
            }

            if (turmaConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Turma não existe nesta Escola.");
            }

            if (disciplinaConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Disciplina não existe nesta Escola.");
            }



            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion



            #region Método de consulta
            var unidades = _repositorioUnidade.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                        .Where(p => p.Ano == Convert.ToUInt32(anoMes.Value.Year)).OrderBy(p => p.Numero).ToList();

            var mesLiberado = true;
            if (!unidades.Where(p => p.Abertura <= anoMes && p.Fechamento >= anoMes).Any())
            {
                mesLiberado = false;
            }

            int diaInicio = Convert.ToInt32(new DateTime(anoMes.Value.Year, anoMes.Value.Month, 1).DayOfWeek);
            int qtdSemanas = Utilitarios.VerificarQuantasSemanaMes(new DateTime(anoMes.Value.Year, anoMes.Value.Month, 1));
            var diasDeAula = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(codTurma.Value);
            var turma = _repositorioTurma.SelecionarPorId(codTurma.Value);
            var disciplina = _repositorioDisciplina.SelecionarPorId(codDisciplina.Value);
            List<int> diasChamadasRealizadas = ConsultarChamadasRealizadasMes(codTurma.Value, codDisciplina.Value, anoMes.Value);
            List<Feriado> datasFeriadosImprensados = ConsultarDiasFeriadosImprensados(anoMes.Value);
            List<int> diasFeriadosImprensados = datasFeriadosImprensados.Select(p => p.Data.Day).ToList();

            List<TurmaFuncionarioDisciplinaExibicaoViewModel> listTurmaFuncionarioDisciplinaExibicaoViewModels = new List<TurmaFuncionarioDisciplinaExibicaoViewModel>();
            foreach (var item in diasDeAula.Where(p => p.CodDisciplina == codDisciplina))
            {
                var turmaFuncionarioDisciplinaExibicaoViewModel = mapper.Map<TurmaFuncionarioDisciplinaExibicaoViewModel>(item);
                turmaFuncionarioDisciplinaExibicaoViewModel.DiasChamadasRealizadas = diasChamadasRealizadas;
                turmaFuncionarioDisciplinaExibicaoViewModel.DatasFeriadosImprensados = datasFeriadosImprensados;
                turmaFuncionarioDisciplinaExibicaoViewModel.DiasFeriadosImprensados = diasFeriadosImprensados;
                listTurmaFuncionarioDisciplinaExibicaoViewModels.Add(turmaFuncionarioDisciplinaExibicaoViewModel);
            }



            ViewBag.Turma = turma;
            ViewBag.Disciplina = disciplina;
            ViewBag.MesLiberado = mesLiberado;
            ViewBag.CodTurma = codTurma;
            ViewBag.AnoMes = anoMes;
            ViewBag.CodDisciplina = codDisciplina;
            ViewBag.Dias = DateTime.DaysInMonth(anoMes.Value.Year, anoMes.Value.Month);
            ViewBag.DiaSemanaInicio = diaInicio;
            ViewBag.MesNome = anoMes.Value.ToString("MMMM", new CultureInfo("PT-pt")).ToUpper();
            ViewBag.QtdSemanas = qtdSemanas;

            #endregion

            return View(listTurmaFuncionarioDisciplinaExibicaoViewModels);
        }


        public ActionResult ListaHorarioDia(int codTurma, int contador, int codDisciplina, DateTime? anoMes)
        {

            var turmaConsulta = _repositorioTurma.SelecionarPorId(codTurma);
            var disciplina = _repositorioDisciplina.SelecionarPorId(codDisciplina);
            if (disciplina == null || turmaConsulta == null)
            {
                return CarregarMensagemDeErro("Turma não existe nesta Escola.");
            }

            if (turmaConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Turma não existe nesta Escola.");
            }

            if (disciplina.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Disciplina não existe nesta Escola.");
            }

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion


            #region Método de consulta

            var turmaFuncionarioDisciplinas = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(codTurma);
            var horarioAulas = _repositorioHorarioAula.SelecionarPorIdHorario(turmaFuncionarioDisciplinas[0].Turma.CodHorario.Value);

            DateTime dataPassadaComoParametro = new DateTime(anoMes.Value.Year, anoMes.Value.Month, contador);

            HorarioExibicaoViewModel horarioExibicaoViewModels = new HorarioExibicaoViewModel();
            horarioExibicaoViewModels = mapper.Map<HorarioExibicaoViewModel>(horarioAulas[0].Horario);
            horarioExibicaoViewModels.HorarioAulaViewModels = new List<HorarioAulaViewModel>();

            List<Aula> aulasDia = _repositorioAula.SelecionarPorIdTurma(codTurma, dataPassadaComoParametro.Year, dataPassadaComoParametro.Month).Where(p => p.Realizada.Day == dataPassadaComoParametro.Day).ToList();

            foreach (var item in horarioAulas)
            {
                if (turmaFuncionarioDisciplinas.Where(p => p.CodHorarioAula == item.CodHorarioAula
                && p.CodDisciplina == codDisciplina
                && p.DiaSemana == Convert.ToInt32(dataPassadaComoParametro.DayOfWeek) + 1).Any())
                {
                    var horarioAulaExibicaoViewModel = mapper.Map<HorarioAulaViewModel>(item);
                    if (aulasDia.Where(p => p.CodHorarioAula == item.CodHorarioAula).Any())
                    {
                        horarioAulaExibicaoViewModel.HorarioFechado = true;
                    }
                    else
                    {
                        horarioAulaExibicaoViewModel.HorarioFechado = false;
                    }
                    horarioExibicaoViewModels.HorarioAulaViewModels.Add(horarioAulaExibicaoViewModel);
                }
            }


            ViewBag.CodTurma = codTurma;
            ViewBag.AnoMes = anoMes;
            ViewBag.Disciplina = disciplina;
            ViewBag.Turma = turmaFuncionarioDisciplinas[0].Turma;
            ViewBag.CodDisciplina = codDisciplina;
            ViewBag.MesNome = anoMes.Value.ToString("MMMM", new CultureInfo("PT-pt")).ToUpper();
            ViewBag.Data = dataPassadaComoParametro;
            #endregion

            return View(horarioExibicaoViewModels);
        }

        private List<int> ConsultarChamadasRealizadasMes(int codTurma, int codDisciplina, DateTime anoMes)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<int> diasFechadosPorChamada = new List<int>();
            var turmaFuncionarioDisciplinas = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(codTurma);
            //verificar os horarios cadastrados no tipo de horario da turma
            var horarioAulas = _repositorioHorarioAula.SelecionarPorIdHorario(turmaFuncionarioDisciplinas[0].Turma.CodHorario.Value);


            var listaMenor = turmaFuncionarioDisciplinas.Where(p => p.CodDisciplina == codDisciplina);
            var valorConsulta = _repositorioAula.SelecionarPorIdTurma(codTurma, anoMes.Year, anoMes.Month);
            foreach (var item in valorConsulta.Select(p => p.Realizada.Date).Distinct())
            {
                var qtdAulasNoDia = listaMenor.Where(p => p.DiaSemana == (Convert.ToInt32(item.DayOfWeek) + 1)).Count();
                var qtdAulasRealizadas = valorConsulta.Where(p => p.Realizada.Year == item.Year
                            && p.Realizada.Month == item.Month
                            && p.Realizada.Day == item.Day
                            && p.CodDisciplina == codDisciplina).Count();

                if (qtdAulasNoDia <= qtdAulasRealizadas)
                {
                    diasFechadosPorChamada.Add(item.Day);
                }
            }
            return diasFechadosPorChamada;
        }


        private List<Feriado> ConsultarDiasFeriadosImprensados(DateTime anoMes)
        {
            List<Feriado> diasFeriadosImprensados = new List<Feriado>();

            var valorConsulta = _repositorioFeriado.SelecionarPorAno(UsuarioEscola.ResgatarCodigoEscolaSelecionada(), anoMes.Year).Where(p => p.Data.Month == anoMes.Month);
            foreach (var item in valorConsulta)
            {
                diasFeriadosImprensados.Add(item);
            }
            return diasFeriadosImprensados;
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
            ViewBag.DropDownMeses = Utils.CarregarMeses();
        }

    }
}
