using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.NEGOCIO.Utils;
using CEAG.REPOSITORIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Acesso;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Funcionario;
using CEAG.WEB.ViewModel.FuncionarioDisciplina;
using CEAG.WEB.ViewModel.Horario;
using CEAG.WEB.ViewModel.Turma;
using CEAG.WEB.ViewModel.TurmaFuncionarioDisciplinas;
using PagedList;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR,PROFESSOR")]
    public class TurmasController : Controller
    {
        private IRepositorioGenerico<Aluno, int> _repositorioGenericoAluno = new RepositorioAluno(new CeagDbContext());
        private RepositorioTurma _repositorioTurma = new RepositorioTurma(new CeagDbContext());
        private IRepositorioGenerico<Funcionario, int> _repositorioGenericoFuncionario = new RepositorioFuncionario(new CeagDbContext());
        private IRepositorioGenerico<Disciplina, int> _repositorioGenericoDisciplina = new RepositorioDisciplina(new CeagDbContext());
        private RepositorioTurmaFuncionarioDisciplina _repositorioTurmaFuncionarioDisciplina = new RepositorioTurmaFuncionarioDisciplina(new CeagDbContext());
        private RepositorioFuncionarioDisciplina _repositorioFuncionarioDisciplina = new RepositorioFuncionarioDisciplina(new CeagDbContext());
        private RepositorioAlunoMatricula _repositorioGenericoAlunoMatricula = new RepositorioAlunoMatricula(new CeagDbContext());
        private RepositorioHorario _repositorioHorario = new RepositorioHorario(new CeagDbContext());
        private RepositorioHorarioAula _repositorioHorarioAulas = new RepositorioHorarioAula(new CeagDbContext());

        #region Métodos de teldo padrão do scalffoding


        
        // GET: Turmas
        public ActionResult Index(string sortOrder, string currentFilter, string searchAno, string searchString, int? page)
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
            ViewBag.PesquisaAno = searchAno;

            int pageSize = Constantes.PAGE_SIZE;
            int pageNumber = (page ?? 1);
            #endregion


            AcessoViewModel usuario = UsuarioEscola.ResgatarUsuarioLogado();

            #region Método de consulta
            var turmas = new List<Turma>();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                turmas = _repositorioTurma.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                        .Where(p =>
                                    p.Descricao.ToUpper().Contains(searchString.ToUpper())
                                    || p.Nivel.ToUpper().Contains(searchString.ToUpper())
                                    || p.Segmento.ToUpper().Contains(searchString.ToUpper())

                        ).ToList();
            }
            else
            {
                turmas = _repositorioTurma.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }

            if (!string.IsNullOrEmpty(searchAno) && !searchAno.Equals("TODOS"))
            {
                turmas = turmas.Where(p => p.AnoLetivo == Convert.ToInt32(searchAno)).ToList();
            }

            if (UsuarioEscola.ResgatarUsuarioLogado().AcessoRoles[0].Role.Descricao.Equals("PROFESSOR"))
            {
                turmas = turmas.Where(p => usuario.TurmaFuncionarioDisciplinas.Select(s => s.CodTurma).Contains(p.CodTurma)).ToList();
            }


            #endregion

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            List<TurmaExibicaoViewModel> dto = mapper.Map<List<TurmaExibicaoViewModel>>(turmas);



            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            CarregarComboTela(null);
            //return View(dto);
            return View(dto.OrderBy(p => p.Descricao).ToPagedList(pageNumber, pageSize));
        }


        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        // GET: Turmas/Details/5
        public ActionResult Details(int? codTurma, string sortOrder, string currentFilter, string searchString, int? page)
        {
            
            if (codTurma == null)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }
            var turma = _repositorioTurma.SelecionarPorId(codTurma.Value);

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turma.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

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

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            var turmaFuncionarioDisciplina = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(codTurma.Value);
            List<TurmaFuncionarioDisciplinaExibicaoViewModel> turmaFuncionarioDisciplinasExibicaOViewModel = new List<TurmaFuncionarioDisciplinaExibicaoViewModel>();
            foreach (var item in turmaFuncionarioDisciplina)
            {
                var foreachturmaFuncionarioDisciplina = mapper.Map<TurmaFuncionarioDisciplinaExibicaoViewModel>(item);
                var professor = mapper.Map<FuncionarioExibicaoViewModel>(item.Funcionario);
                var disciplina = mapper.Map<DisciplinaExibicaoViewModel>(item.Disciplina);
                foreachturmaFuncionarioDisciplina.Funcionario = professor;
                foreachturmaFuncionarioDisciplina.Disciplina = disciplina;
                turmaFuncionarioDisciplinasExibicaOViewModel.Add(foreachturmaFuncionarioDisciplina);
            }
            List<TurmaFuncionarioDisciplinaExibicaoViewModel> dtoTurmaFuncionarioDisciplinas = new List<TurmaFuncionarioDisciplinaExibicaoViewModel>();


            foreach (var codFuncionario in turmaFuncionarioDisciplinasExibicaOViewModel.Select(p => p.CodFuncionario).Distinct())
            {
                foreach (var professor in turmaFuncionarioDisciplinasExibicaOViewModel.Where(p => p.CodFuncionario == codFuncionario))
                {
                    foreach (var item in turmaFuncionarioDisciplinasExibicaOViewModel.Where(p => p.CodFuncionario == codFuncionario))
                    {
                        if (professor.Funcionario.Disciplinas == null)
                        {
                            professor.Funcionario.Disciplinas = item.Disciplina.Descricao;
                        }
                        else
                        {
                            if (!professor.Funcionario.Disciplinas.Contains(item.Disciplina.Descricao))
                            {
                                professor.Funcionario.Disciplinas += " | " + item.Disciplina.Descricao;
                            }
                        }

                    }

                }

                dtoTurmaFuncionarioDisciplinas.Add(turmaFuncionarioDisciplinasExibicaOViewModel.Where(p => p.CodFuncionario == codFuncionario).Take(1).SingleOrDefault());
            }

            List<AlunoMatricula> alunoMatricula = _repositorioGenericoAlunoMatricula.SelecionarPorIdTurma(codTurma.Value);

            if (!alunoMatricula.Any())
            {
                alunoMatricula.Add(new AlunoMatricula());
            }

            TurmaExibicaoViewModel dto = mapper.Map<TurmaExibicaoViewModel>(turma);

            List<AlunoMatriculaExibicaoViewModel> dtoAlunoMatricula = mapper.Map<List<AlunoMatriculaExibicaoViewModel>>(alunoMatricula);
            //List<TurmaFuncionarioDisciplinaExibicaoViewModel> dtoTurmaFuncionarioDisciplina = mapper.Map<List<TurmaFuncionarioDisciplinaExibicaoViewModel>>(turmaFuncionarioDisciplina);
            if (dtoAlunoMatricula == null)
            {
                return CarregarMensagemDeErro("Matricula não existe nesta escola.");
            }

            if (!dtoTurmaFuncionarioDisciplinas.Any())
            {
                dtoTurmaFuncionarioDisciplinas.Add(new TurmaFuncionarioDisciplinaExibicaoViewModel
                {
                    Funcionario = new FuncionarioExibicaoViewModel()
                });
            }
            dto.TurmaFuncionarioDisciplinas = dtoTurmaFuncionarioDisciplinas;

            foreach (var item in dtoAlunoMatricula)
            {
                var aluno = _repositorioGenericoAluno.SelecionarPorId(item.CodAluno);
                item.Aluno = mapper.Map<AlunoExibicaoViewModel>(aluno);
                item.Turma = dto;
            }

            
            var retorno = CarregarHorarioDaTurma(codTurma.Value);
            if (retorno == null)
            {
                return CarregarMensagemDeErro("A turma não tem horário padrão definido.");
            }

            TurmaFuncionarioDisciplinaHorarioExibicaoViewModel turmaFuncionarioDisciplinaHorarioExibicaoViewModel = retorno;// CarregarHorarioDaTurma(codTurma.Value);

            ViewBag.TurmaFuncionarioDisciplinaHorarioExibicaoViewModel = turmaFuncionarioDisciplinaHorarioExibicaoViewModel;
            ViewBag.TurmaFuncionarioDisciplina = turmaFuncionarioDisciplinasExibicaOViewModel;
            ViewBag.Turma = dtoAlunoMatricula[0].Turma;
            return View(dtoAlunoMatricula.Where(p => p.Aluno != null).Any() ? dtoAlunoMatricula.OrderBy(p => p.Aluno.Nome).ToPagedList(pageNumber, pageSize) : dtoAlunoMatricula.ToPagedList(pageNumber, pageSize));
        }



        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        // GET: Turmas/Create
        public ActionResult Create()
        {
            
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            CarregarComboTela(null);
            return View();
        }



        // POST: Turmas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TurmaViewModel turmaViewModel)
        {
            

            turmaViewModel.Inclusao = DateTime.Now;
            turmaViewModel.Abertura = null;
            turmaViewModel.Fechamento = null;
            turmaViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                if (!turmaViewModel.Nivel.ToUpper().Equals("SELECIONE O NIVEL"))
                {
                    #region Mapper
                    Mapper mapper = Constants.Utils.ViewModelParaDominio();
                    #endregion

                    #region Inserindo a Turma

                    var turma = mapper.Map<TurmaViewModel, Turma>(turmaViewModel);
                    turma.CodAcesso = usuario.CodAcesso;
                    _repositorioTurma.Inserir(turma);

                    #endregion

                   
                    return RedirectToAction("Index", "Turmas", new { codEscola = turmaViewModel.CodEscola });
                }
            }
            CarregarComboTela(null);
            return View(turmaViewModel);
        }


        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        // GET: Turmas/Edit/5
        public ActionResult Edit(int? codTurma)
        {

            if (codTurma == null)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            Turma turmaConsulta = _repositorioTurma.SelecionarPorId(codTurma.Value);
            var escola = UsuarioEscola.ResgatarEscolaLogada();

            if (turmaConsulta == null)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turmaConsulta.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            TurmaViewModel dto = mapper.Map<TurmaViewModel>(turmaConsulta);



            CarregarComboTela(dto.Nivel);

            return View(dto);
        }

        // POST: Turmas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TurmaViewModel turmaViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion


                #region Alterando a Turma

                var turma = mapper.Map<TurmaViewModel, Turma>(turmaViewModel);
                turma.CodAcesso = usuario.CodAcesso;
                _repositorioTurma.Alterar(turma);
                #endregion

                return RedirectToAction("Index", "Turmas");
            }
            return View(turmaViewModel);
        }

        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        // GET: Turmas/Delete/5
        public ActionResult Delete(int? codTurma)
        {
            

            if (codTurma == null)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            var turmaConsulta = _repositorioTurma.SelecionarPorId(codTurma.Value);
            if (turmaConsulta == null)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }


            if (turmaConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            var alunos = _repositorioGenericoAlunoMatricula.SelecionarPorIdTurma(codTurma.Value);

            if (alunos.Any())
            {
                return CarregarMensagemDeErro("Existem alunos na turma, Impossivel excluir.");
            }

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion
            var turmaViewModel = mapper.Map<Turma, TurmaViewModel>(turmaConsulta);

            CarregarComboTela(turmaConsulta.Nivel);

            return View(turmaViewModel);
        }

        // POST: Turmas/Delete/5
        [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codTurma)
        {
            

            var turmaConsulta = _repositorioTurma.SelecionarPorId(codTurma);
            
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turmaConsulta.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            var alunos = _repositorioGenericoAlunoMatricula.SelecionarPorIdTurma(codTurma);

            if (alunos.Any())
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            var professores = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(codTurma);

            //Excluindo os professores da turma
            foreach (var item in professores)
            {
                RepositorioTurmaFuncionarioDisciplina _repoLocal = new RepositorioTurmaFuncionarioDisciplina(new CeagDbContext());
                var turmaFuncionarioDisciplina = _repoLocal.SelecionarPorId(item.CodTurmaFuncionarioDisciplina);
                _repoLocal.Excluir(turmaFuncionarioDisciplina);
            }

            RepositorioTurma _repoLocalTurma = new RepositorioTurma(new CeagDbContext());
            var turma = _repoLocalTurma.SelecionarPorId(codTurma);
            _repoLocalTurma.Excluir(turma);

            return RedirectToAction("Index", "Turmas");
        }

        #endregion

        #region Metodos de Tela fora do padrão scalffoding

        #region Listas Modais


        public ActionResult ListaTurma(int? codAlunoMatricula)
        {
            

            AlunoMatricula alunoMatricula = _repositorioGenericoAlunoMatricula.SelecionarPorId(codAlunoMatricula.Value);

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != alunoMatricula.Aluno.CodEscola)
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }

            #region Método de consulta

            var turmas = new List<Turma>();
            turmas = _repositorioTurma.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                    .Where(p =>
                                p.AnoLetivo == DateTime.Now.Year
                                || p.AnoLetivo == DateTime.Now.Year + 1

                    ).ToList();


            #endregion

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            List<TurmaExibicaoViewModel> dto = mapper.Map<List<TurmaExibicaoViewModel>>(turmas);



            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.CodAlunoMatricula = codAlunoMatricula;
            CarregarComboTela(null);
            //return View(dto);
            return View(dto);
        }
        #endregion

        public ActionResult ListaAno(int codTurma)
        {
            ViewBag.CodTurma = codTurma;
            return View();
        }

        public ActionResult DuplicarTurma(int codTurma, int anoLetivo)
        {
            
            var turma = _repositorioTurma.SelecionarPorId(codTurma);
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turma.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            List<Turma> turmas = _repositorioTurma.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).Where(p => p.AnoLetivo == anoLetivo).ToList();
            if (turmas.Where(p => p.Descricao.Equals(turma.Descricao)).Any())
            {
                return CarregarMensagemDeErro("Turma já existe no ano selecionado.");
            }

            _repositorioTurma.DuplicarTurma(codTurma, anoLetivo, UsuarioEscola.ResgatarUsuarioLogado().CodAcesso);

            return RedirectToAction("Index", "Turmas", new { searchAno = anoLetivo });
        }

        public ActionResult AdicionarProfessor(int codTurma, int codFuncionario)
        {
            var turma = new Turma();
            using (RepositorioTurma _repositorioTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repositorioTurmaLocal.SelecionarPorId(codTurma);
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turma.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            var funcionario = new Funcionario();
            using (RepositorioFuncionario _repositorioFuncionarioLocal = new RepositorioFuncionario(new CeagDbContext()))
            {
                funcionario = _repositorioFuncionarioLocal.SelecionarPorId(codFuncionario);
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != funcionario.CodEscola)
            {
                return CarregarMensagemDeErro("Professor não existe nesta escola.");
            }

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            TurmaExibicaoViewModel turmaMaper = mapper.Map<TurmaExibicaoViewModel>(turma);

            var turmaFuncionarioDisciplina = new List<TurmaFuncionarioDisciplina>();
            using (RepositorioTurmaFuncionarioDisciplina _repoLocal = new RepositorioTurmaFuncionarioDisciplina(new CeagDbContext()))
            {
                turmaFuncionarioDisciplina = _repoLocal.SelecionarPorTurma(codTurma);
            }

            List<DisciplinaExibicaoViewModel> disciplinasExibicao = new List<DisciplinaExibicaoViewModel>();
            
            FuncionarioExibicaoViewModel professorMaper = mapper.Map<FuncionarioExibicaoViewModel>(funcionario);


            var professorDisciplina = new List<FuncionarioDisciplina>();
            using (RepositorioFuncionarioDisciplina _repoLocal = new RepositorioFuncionarioDisciplina(new CeagDbContext()))
            {
                professorDisciplina = _repoLocal.SelecionarDisciplinasPorProfessor(codFuncionario);
            }

            List<FuncionarioDisciplinaExibicaoViewModel> professorDisciplinaMaper = mapper.Map<List<FuncionarioDisciplinaExibicaoViewModel>>(professorDisciplina);


            var horariosTurma = _repositorioHorarioAulas.SelecionarPorIdHorario(turma.CodHorario.Value);
            List<HorarioAulaViewModel> horarioAulaViewModel = mapper.Map<List<HorarioAulaViewModel>>(horariosTurma);
            List<HorarioAulaViewModel> horarioAulaViewModelParaTela = new List<HorarioAulaViewModel>();
            foreach (var itemDiaSemana in horarioAulaViewModel.Select(p => p.DiaSemana).Distinct())
            {
                if (itemDiaSemana == 1)
                {
                    foreach (var item in horarioAulaViewModel.Where(p => p.DiaSemana == itemDiaSemana).OrderBy(s => s.Inicio))
                    {

                        for (int i = 2; i <= 6; i++)
                        {
                            var status = "LIVRE";
                            var disciplina = turmaFuncionarioDisciplina.Where(p => p.CodHorarioAula == item.CodHorarioAula && p.DiaSemana == i);
                            if (disciplina.Any())
                            {
                                status = disciplina.Select(p => p.Disciplina.Descricao).SingleOrDefault();//"OCUPADO";
                            }
                            List<TurmaFuncionarioDisciplina> funcionarioHorario = _repositorioTurmaFuncionarioDisciplina.SelecionarPorProfessor(codFuncionario).Where(p => p.Turma.AnoLetivo == turmaMaper.AnoLetivo).ToList();
                            if (funcionarioHorario.Where(p => p.CodHorarioAula == item.CodHorarioAula && p.DiaSemana == i).Any())
                            {
                                using (RepositorioTurma _repoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
                                {
                                    List<TurmaFuncionarioDisciplina> funcionarioSelecionado = funcionarioHorario.Where(p => p.CodHorarioAula == item.CodHorarioAula && p.DiaSemana == i).ToList();
                                    status = funcionarioSelecionado[0].Turma.Descricao;
                                }
                            }

                            HorarioAulaViewModel aula = new HorarioAulaViewModel
                            {
                                DiaSemana = i,
                                CodHorario = horarioAulaViewModel[0].CodHorario,
                                CodHorarioAula = item.CodHorarioAula,
                                Inicio = item.Inicio,
                                Termino = item.Termino,
                                Turno = item.Turno,
                                Status = status
                            };
                            Utils.VerificarNomeDiaSemana(aula);
                            horarioAulaViewModelParaTela.Add(aula);
                        }
                    }

                }
            }

            foreach (var item in professorDisciplinaMaper)
            {

                var disciplinaBanco = _repositorioGenericoDisciplina.SelecionarPorId(item.CodDisciplina);
                item.Funcionario = professorMaper;
                item.HorarioAulaViewModels = horarioAulaViewModelParaTela;
                item.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(disciplinaBanco);
                disciplinasExibicao.Add(mapper.Map<DisciplinaExibicaoViewModel>(disciplinaBanco));
            }

            ViewBag.Funcionario = professorMaper;
            ViewBag.Turma = turmaMaper;
            ViewBag.Disciplinas = disciplinasExibicao;
            return View(professorDisciplinaMaper);
        }

        public ActionResult VisualizarHorario(int codTurma)
        {
            

            var turmaConsulta = _repositorioTurma.SelecionarPorId(codTurma);

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turmaConsulta.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }


            TurmaFuncionarioDisciplinaHorarioExibicaoViewModel turmaFuncionarioDisciplinaHorarioExibicaoViewModel = CarregarHorarioDaTurma(codTurma);

            return View(turmaFuncionarioDisciplinaHorarioExibicaoViewModel);

        }

        private TurmaFuncionarioDisciplinaHorarioExibicaoViewModel CarregarHorarioDaTurma(int codTurma)
        {

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            var turmaFuncionarioDisciplina = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(codTurma);
            List<TurmaFuncionarioDisciplinaExibicaoViewModel> listTurmaFuncionarioDisciplinaExibicaoViewModel = new List<TurmaFuncionarioDisciplinaExibicaoViewModel>();
            Turma turma = _repositorioTurma.SelecionarPorId(codTurma);
            List<HorarioAula> horariosTurma = new List<HorarioAula>();
            TurmaExibicaoViewModel turmaExibicaoViewModel = mapper.Map<TurmaExibicaoViewModel>(turma);
            if (turmaFuncionarioDisciplina.Any())
            {
                foreach (var itemTurmaFuncionarioDisciplina in turmaFuncionarioDisciplina)
                {
                    TurmaFuncionarioDisciplinaExibicaoViewModel turmaFuncionarioDisciplinaExibicaoViewModel = mapper.Map<TurmaFuncionarioDisciplinaExibicaoViewModel>(itemTurmaFuncionarioDisciplina);
                    turmaFuncionarioDisciplinaExibicaoViewModel.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(itemTurmaFuncionarioDisciplina.Disciplina);
                    turmaFuncionarioDisciplinaExibicaoViewModel.Funcionario = mapper.Map<FuncionarioExibicaoViewModel>(itemTurmaFuncionarioDisciplina.Funcionario);
                    listTurmaFuncionarioDisciplinaExibicaoViewModel.Add(turmaFuncionarioDisciplinaExibicaoViewModel);
                }

            }

            if (!turma.CodHorario.HasValue)
            {
                return null;
            }

            horariosTurma = _repositorioHorarioAulas.SelecionarPorIdHorario(turma.CodHorario.Value);
            List<HorarioAulaViewModel> horarioAulaViewModel = mapper.Map<List<HorarioAulaViewModel>>(horariosTurma);
            List<HorarioAulaViewModel> horarioAulaViewModelParaTela = new List<HorarioAulaViewModel>();

            foreach (var itemDiaSemana in horarioAulaViewModel.Select(p => p.DiaSemana).Distinct())
            {
                if (itemDiaSemana == 1)
                {
                    foreach (var item in horarioAulaViewModel.Where(p => p.DiaSemana == itemDiaSemana).OrderBy(s => s.Inicio))
                    {

                        for (int i = 2; i <= 6; i++)
                        {
                            HorarioAulaViewModel aula = new HorarioAulaViewModel
                            {
                                DiaSemana = i,
                                CodHorario = horarioAulaViewModel[0].CodHorario,
                                CodHorarioAula = item.CodHorarioAula,
                                Inicio = item.Inicio,
                                Termino = item.Termino,
                                Turno = item.Turno
                            };
                            Utils.VerificarNomeDiaSemana(aula);
                            horarioAulaViewModelParaTela.Add(aula);
                        }
                    }

                }
            }

            TurmaFuncionarioDisciplinaHorarioExibicaoViewModel turmaFuncionarioDisciplinaHorarioExibicaoViewModel = new TurmaFuncionarioDisciplinaHorarioExibicaoViewModel();
            turmaFuncionarioDisciplinaHorarioExibicaoViewModel.HorarioAulaViewModels = horarioAulaViewModelParaTela;
            turmaFuncionarioDisciplinaHorarioExibicaoViewModel.TurmaExibicaoViewModel = turmaExibicaoViewModel;
            turmaFuncionarioDisciplinaHorarioExibicaoViewModel.TurmaFuncionarioDisciplinaExibicaoViewModels = listTurmaFuncionarioDisciplinaExibicaoViewModel;
            return turmaFuncionarioDisciplinaHorarioExibicaoViewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarProfessor(IEnumerable<string> IdsHorario, int codFuncionario, int codTurma)
        {
            

            var turmaConsulta = _repositorioTurma.SelecionarPorId(codTurma);

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turmaConsulta.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }



            foreach (var item in IdsHorario)
            {
                int codDisciplina = 0;
                int codHorarioAula = 0;
                int diaSemana = 0;

                string valor = "";
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i].Equals('-'))
                    {
                        if (diaSemana == 0)
                        {
                            diaSemana = Convert.ToInt32(valor);
                        }
                        else
                        {
                            if (codDisciplina == 0)
                            {
                                codDisciplina = Convert.ToInt32(valor);
                            }
                            else
                            {
                                codHorarioAula = Convert.ToInt32(valor);
                            }
                        }
                        valor = "";
                    }
                    else
                    {
                        valor = valor + item[i];
                    }
                }
                TurmaFuncionarioDisciplina turmaFuncionarioDisciplina = new TurmaFuncionarioDisciplina();
                turmaFuncionarioDisciplina.CodFuncionario = codFuncionario;
                turmaFuncionarioDisciplina.CodTurma = codTurma;
                turmaFuncionarioDisciplina.Inclusao = DateTime.Now;
                turmaFuncionarioDisciplina.CodDisciplina = codDisciplina;
                turmaFuncionarioDisciplina.CodHorarioAula = codHorarioAula;
                turmaFuncionarioDisciplina.DiaSemana = diaSemana;

                _repositorioTurmaFuncionarioDisciplina.Inserir(turmaFuncionarioDisciplina);
            }

            return RedirectToAction("Details", "Turmas", new { codTurma });
        }


        public ActionResult RemoverProfessor(int? codTurma, int? codFuncionario)
        {
            var turmaConsulta = _repositorioTurma.SelecionarPorId(codTurma.Value);
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turmaConsulta.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }
            var funcionario = _repositorioGenericoFuncionario.SelecionarPorId(codFuncionario.Value);

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != funcionario.CodEscola)
            {
                return CarregarMensagemDeErro("Professor não existe nesta escola.");
            }

            using (RepositorioAlunoMatriculaUnidade _repoLocal = new RepositorioAlunoMatriculaUnidade(new CeagDbContext()))
            {
                var alunoMatriculaUnidade = _repoLocal.SelecionarPorProfessor(codFuncionario.Value);
                foreach (var item in alunoMatriculaUnidade)
                {
                    _repoLocal.ExcluirPorId(item.CodAlunoMatriculaUnidade);
                }
            }

            var turmaProfessorDisciplina = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(codTurma.Value);
            foreach (var item in turmaProfessorDisciplina)
            {
                if (item.CodFuncionario == codFuncionario)
                {
                    _repositorioTurmaFuncionarioDisciplina.ExcluirPorId(item.CodTurmaFuncionarioDisciplina);
                }
            }

            return RedirectToAction("Details", "Turmas", new { codTurma });
        }



        #endregion


        #region Funções de apoio



        [HttpPost]
        public JsonResult ListarSegmento(string nivel)
        {
            

            List<SelectListItem> listaNivel = CarregarListaSegmento(nivel);
            return Json(new SelectList(listaNivel, "Value", "Text"));

        }

        private static List<SelectListItem> CarregarListaSegmento(string nivel)
        {
            var listaNivel = new List<SelectListItem>();
            if (nivel.ToUpper().Equals("FUNDAMENTAL I"))
            {
                listaNivel.Add(new SelectListItem { Value = "1º Ano", Text = "1º Ano" });
                listaNivel.Add(new SelectListItem { Value = "2º Ano", Text = "2º Ano" });
                listaNivel.Add(new SelectListItem { Value = "3º Ano", Text = "3º Ano" });
                listaNivel.Add(new SelectListItem { Value = "4º Ano", Text = "4º Ano" });
                listaNivel.Add(new SelectListItem { Value = "5º Ano", Text = "5º Ano" });
            }
            else if (nivel.ToUpper().Equals("FUNDAMENTAL II"))
            {
                listaNivel.Add(new SelectListItem { Value = "6º Ano", Text = "6º Ano" });
                listaNivel.Add(new SelectListItem { Value = "7º Ano", Text = "7º Ano" });
                listaNivel.Add(new SelectListItem { Value = "8º Ano", Text = "8º Ano" });
                listaNivel.Add(new SelectListItem { Value = "9º Ano", Text = "9º Ano" });
            }
            else if (nivel.ToUpper().Equals("INFANTIL"))
            {
                listaNivel.Add(new SelectListItem { Value = "MATERNAL I", Text = "MATERNAL I" });
                listaNivel.Add(new SelectListItem { Value = "MATERNAL II", Text = "MATERNAL II" });
                listaNivel.Add(new SelectListItem { Value = "JARDIM I", Text = "JARDIM I" });
                listaNivel.Add(new SelectListItem { Value = "JARDIM II", Text = "JARDIM II" });
            }
            else if (nivel.ToUpper().Equals("MÉDIO"))
            {
                listaNivel.Add(new SelectListItem { Value = "SEM OPÇÔES", Text = "SEM OPÇÔES" });
            }

            return listaNivel;
        }


        private void CarregarComboTela(string nivel)
        {
            #region Nivel

            var listaNivel = new List<SelectListItem>
            {
                new SelectListItem { Value = "SELECIONE O NIVEL", Text = "SELECIONE O NIVEL" },
                new SelectListItem { Value = "INFANTIL", Text = "INFANTIL" },
                new SelectListItem { Value = "FUNDAMENTAL I", Text = "FUNDAMENTAL I" },
                new SelectListItem { Value = "FUNDAMENTAL II", Text = "FUNDAMENTAL II" },
                new SelectListItem { Value = "MÉDIO", Text = "MÉDIO" }
            };


            var dropdownNivel = new SelectList(listaNivel, "Value", "Text");


            #endregion

            #region Segmento
            if (!string.IsNullOrEmpty(nivel))
            {
                List<SelectListItem> listaSegmento = CarregarListaSegmento(nivel);
                var dropdownSegmento = new SelectList(listaSegmento, "Value", "Text");
                ViewBag.DropdownSegmento = dropdownSegmento;
            }

            #endregion

            #region Horario

            var horario = _repositorioHorario.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            var dropdownHorario = new SelectList(horario, "CodHorario", "Descricao");
            #endregion

            ViewBag.DropdownPeriodo = Utils.CarregarTurnos();
            ViewBag.DropdownAnoLetivo = Utils.CarregarAno();
            ViewBag.DropdownNivel = dropdownNivel;
            ViewBag.DropdownHorario = dropdownHorario;
        }

        #endregion

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
