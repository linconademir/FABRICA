using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.NEGOCIO.Enum;
using CEAG.NEGOCIO.Utils;
using CEAG.REPOSITORIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Acesso;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.AlunoMatriculaUnidade;
using CEAG.WEB.ViewModel.Boletim;
using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Funcionario;
using CEAG.WEB.ViewModel.Turma;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR,PROFESSOR")]
    public class AlunoMatriculaUnidadesController : Controller
    {
        // GET: AlunoMatriculaUnidades
        public ActionResult Index(int codTurma, string messageError)
        {
            #region Validação de Entrada na tela

            Turma turma;
            using (RepositorioTurma _repositorioLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repositorioLocal.SelecionarPorId(codTurma);
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turma.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            #endregion

            #region Método de consulta

            List<AlunoMatriculaUnidade> alunoMatriculaUnidades;
            AcessoViewModel usuario = UsuarioEscola.ResgatarUsuarioLogado();

            using (RepositorioAlunoMatriculaUnidade _repoLocal = new RepositorioAlunoMatriculaUnidade(new CeagDbContext()))
            {
                alunoMatriculaUnidades = _repoLocal.SelecionarPorTurma(codTurma);
            }
            #endregion

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            #region Tratamento dos dados

            List<AlunoMatriculaUnidadeExibicaoViewModel> dto = new List<AlunoMatriculaUnidadeExibicaoViewModel>();

            if (UsuarioEscola.ResgatarUsuarioLogado().AcessoRoles[0].Role.Descricao.Equals("PROFESSOR"))
            {
                foreach (var item in alunoMatriculaUnidades
                    .Where(p => usuario.TurmaFuncionarioDisciplinas
                    .Select(s => s.CodDisciplina).Contains(p.CodDisciplina)))
                {
                    AlunoMatriculaUnidadeExibicaoViewModel alunoMatriculaUnidade = mapper.Map<AlunoMatriculaUnidadeExibicaoViewModel>(item);
                    alunoMatriculaUnidade.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(item.Disciplina);
                    alunoMatriculaUnidade.Media = Math.Round((alunoMatriculaUnidade.NotaPC ?? 0)
                                                    + (alunoMatriculaUnidade.NotaProva ?? 0)
                                                    + (alunoMatriculaUnidade.NotaTeste ?? 0)
                                                    + (alunoMatriculaUnidade.NotaConselhoClasse ?? 0)
                                                    + (alunoMatriculaUnidade.NotaRecuperacao ?? 0), 2);
                    alunoMatriculaUnidade.Funcionario = mapper.Map<FuncionarioExibicaoViewModel>(item.Funcionario);
                    alunoMatriculaUnidade.AlunoMatricula = mapper.Map<AlunoMatriculaExibicaoViewModel>(item.AlunoMatricula);
                    alunoMatriculaUnidade.AlunoMatricula.Aluno = mapper.Map<AlunoExibicaoViewModel>(item.AlunoMatricula.Aluno);
                    alunoMatriculaUnidade.AlunoMatricula.Turma = mapper.Map<TurmaExibicaoViewModel>(item.AlunoMatricula.Turma);
                    dto.Add(alunoMatriculaUnidade);

                }
            }
            else
            {
                foreach (var item in alunoMatriculaUnidades)
                {
                    AlunoMatriculaUnidadeExibicaoViewModel alunoMatriculaUnidade = mapper.Map<AlunoMatriculaUnidadeExibicaoViewModel>(item);
                    alunoMatriculaUnidade.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(item.Disciplina);
                    alunoMatriculaUnidade.Media = Math.Round((alunoMatriculaUnidade.NotaPC ?? 0)
                                                    + (alunoMatriculaUnidade.NotaProva ?? 0)
                                                    + (alunoMatriculaUnidade.NotaTeste ?? 0)
                                                    + (alunoMatriculaUnidade.NotaConselhoClasse ?? 0)
                                                    + (alunoMatriculaUnidade.NotaRecuperacao ?? 0), 2);
                    alunoMatriculaUnidade.Funcionario = mapper.Map<FuncionarioExibicaoViewModel>(item.Funcionario);
                    alunoMatriculaUnidade.AlunoMatricula = mapper.Map<AlunoMatriculaExibicaoViewModel>(item.AlunoMatricula);
                    alunoMatriculaUnidade.AlunoMatricula.Aluno = mapper.Map<AlunoExibicaoViewModel>(item.AlunoMatricula.Aluno);
                    alunoMatriculaUnidade.AlunoMatricula.Turma = mapper.Map<TurmaExibicaoViewModel>(item.AlunoMatricula.Turma);
                    dto.Add(alunoMatriculaUnidade);

                }
            }

            #endregion

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.CodTurma = codTurma;
            ViewBag.MessageError = messageError;
            return View(dto);
        }


        public ActionResult IndexUnidade(int codTurma)
        {
            #region Validação de Entrada na tela

            Turma turma;
            using (RepositorioTurma _repositorioLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repositorioLocal.SelecionarPorId(codTurma);
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turma.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            #endregion

            #region Método de consulta

            List<AlunoMatriculaUnidade> alunoMatriculaUnidades;

            using (RepositorioAlunoMatriculaUnidade _repoLocal = new RepositorioAlunoMatriculaUnidade(new CeagDbContext()))
            {
                alunoMatriculaUnidades = _repoLocal.SelecionarPorTurma(codTurma);
            }
            #endregion

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            #region Tratamento dos dados
            List<AlunoMatriculaUnidadeExibicaoViewModel> dto = new List<AlunoMatriculaUnidadeExibicaoViewModel>();
            foreach (var item in alunoMatriculaUnidades)
            {
                AlunoMatriculaUnidadeExibicaoViewModel alunoMatriculaUnidade = mapper.Map<AlunoMatriculaUnidadeExibicaoViewModel>(item);
                alunoMatriculaUnidade.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(item.Disciplina);
                alunoMatriculaUnidade.Media = Math.Round(((alunoMatriculaUnidade.NotaPC ?? 0)
                                                + (alunoMatriculaUnidade.NotaProva ?? 0)
                                                + (alunoMatriculaUnidade.NotaTeste ?? 0)), 2);
                alunoMatriculaUnidade.Funcionario = mapper.Map<FuncionarioExibicaoViewModel>(item.Funcionario);
                alunoMatriculaUnidade.AlunoMatricula = mapper.Map<AlunoMatriculaExibicaoViewModel>(item.AlunoMatricula);
                alunoMatriculaUnidade.AlunoMatricula.Aluno = mapper.Map<AlunoExibicaoViewModel>(item.AlunoMatricula.Aluno);
                alunoMatriculaUnidade.AlunoMatricula.Turma = mapper.Map<TurmaExibicaoViewModel>(item.AlunoMatricula.Turma);
                dto.Add(alunoMatriculaUnidade);

            }


            #endregion

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.CodTurma = codTurma;
            return View(dto);
        }

        public ActionResult Boletim(int codAlunoMatricula)
        {
            #region Intancia das informações de Unidade para o boletim

            BoletimExibicaoViewModel boletimExibicaoViewModel = new BoletimExibicaoViewModel
            {
                PrimeiraUnidade = new List<AlunoMatriculaUnidadeExibicaoViewModel>(),
                SegundaUnidade = new List<AlunoMatriculaUnidadeExibicaoViewModel>(),
                TerceiraUnidade = new List<AlunoMatriculaUnidadeExibicaoViewModel>(),
                QuartaUnidade = new List<AlunoMatriculaUnidadeExibicaoViewModel>()
            };

            #endregion

            #region Método de consulta

            var alunoMatriculaUnidades = new List<AlunoMatriculaUnidade>();
            using (RepositorioAlunoMatriculaUnidade _repoLocal = new RepositorioAlunoMatriculaUnidade(new CeagDbContext()))
            {
                alunoMatriculaUnidades = _repoLocal.SelecionarPorAlunoMatricula(codAlunoMatricula);
            }
           

            if (!alunoMatriculaUnidades.Any())
            {
                return CarregarMensagemDeErro("Não existe nota lançada para este Aluno(a).");
            }

            boletimExibicaoViewModel.CodAlunoMatricula = codAlunoMatricula;
            boletimExibicaoViewModel.CodAluno = alunoMatriculaUnidades[0].AlunoMatricula.CodAluno;
            Turma turma;
            using (RepositorioTurma _repositorioLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repositorioLocal.SelecionarPorId(alunoMatriculaUnidades[0].AlunoMatricula.CodTurma);
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turma.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            #endregion

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            #region Tratamento dos dados

            List<DisciplinaExibicaoViewModel> DisciplinasNoBoletim = new List<DisciplinaExibicaoViewModel>();

            for (int i = 1; i <= 4; i++)
            {
                foreach (var itemUnidades in alunoMatriculaUnidades.Where(p => p.NumeroUnidade == i))
                {
                    AlunoMatriculaUnidadeExibicaoViewModel alunoMatriculaUnidade = new AlunoMatriculaUnidadeExibicaoViewModel();

                    alunoMatriculaUnidade = mapper.Map<AlunoMatriculaUnidadeExibicaoViewModel>(itemUnidades);
                    alunoMatriculaUnidade.Media = Math.Round((alunoMatriculaUnidade.NotaPC ?? 0)
                                + (alunoMatriculaUnidade.NotaProva ?? 0)
                                + (alunoMatriculaUnidade.NotaTeste ?? 0)
                                + (alunoMatriculaUnidade.NotaConselhoClasse ?? 0)
                                + (alunoMatriculaUnidade.NotaRecuperacao ?? 0), 2);
                    alunoMatriculaUnidade.Funcionario = mapper.Map<FuncionarioExibicaoViewModel>(itemUnidades.Funcionario);
                    alunoMatriculaUnidade.Faltas = ListarFaltaAlunoPorUnidade(itemUnidades.CodAlunoMatricula, i, itemUnidades.AlunoMatricula.Turma.AnoLetivo, itemUnidades.CodDisciplina);
                    alunoMatriculaUnidade.NumeroUnidade = i;
                    alunoMatriculaUnidade.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(itemUnidades.Disciplina);
                    if (!DisciplinasNoBoletim.Where(p => p.CodDisciplina == alunoMatriculaUnidade.Disciplina.CodDisciplina).Any())
                    {
                        DisciplinasNoBoletim.Add(alunoMatriculaUnidade.Disciplina);
                    }
                    alunoMatriculaUnidade.AlunoMatricula = mapper.Map<AlunoMatriculaExibicaoViewModel>(itemUnidades.AlunoMatricula);
                    alunoMatriculaUnidade.AlunoMatricula.Aluno = mapper.Map<AlunoExibicaoViewModel>(itemUnidades.AlunoMatricula.Aluno);
                    alunoMatriculaUnidade.AlunoMatricula.Turma = mapper.Map<TurmaExibicaoViewModel>(itemUnidades.AlunoMatricula.Turma);

                    if (alunoMatriculaUnidade.NumeroUnidade == 1)
                    {
                        boletimExibicaoViewModel.PrimeiraUnidade.Add(alunoMatriculaUnidade);
                    }
                    if (alunoMatriculaUnidade.NumeroUnidade == 2)
                    {
                        boletimExibicaoViewModel.SegundaUnidade.Add(alunoMatriculaUnidade);
                    }
                    if (alunoMatriculaUnidade.NumeroUnidade == 3)
                    {
                        boletimExibicaoViewModel.TerceiraUnidade.Add(alunoMatriculaUnidade);
                    }
                    if (alunoMatriculaUnidade.NumeroUnidade == 4)
                    {
                        boletimExibicaoViewModel.QuartaUnidade.Add(alunoMatriculaUnidade);
                    }
                }
            }

            boletimExibicaoViewModel.DisciplinaExibicaoViewModels = DisciplinasNoBoletim;


            #endregion

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.CodTurma = turma.CodTurma;
            return View(boletimExibicaoViewModel);
        }


        public void EnviarEmailParaProfessorDisciplina(int codAlunoMatricula)
        {
            AlunoMatricula alunoMatricula;
            using (RepositorioAlunoMatricula _repoLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
            {
                alunoMatricula = _repoLocal.SelecionarPorId(codAlunoMatricula);
            }

            Turma turma;
            using (RepositorioTurma _repoLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repoLocal.SelecionarPorId(codAlunoMatricula);
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turma.CodEscola)
            {
                CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            //var funcionario = _repositorioGenericoFuncionario.SelecionarPorId(alunoMatricula.);
            Models.SendEmail.EnviarNotificacaoProfessor(null);
        }

        // GET: AlunoMatriculaUnidades/Create
        public ActionResult Create(int numeroUnidade, int codTurma)
        {
            #region Validação de Entrada na Tela

            Turma turma;
            using (RepositorioTurma _repoLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repoLocal.SelecionarPorId(codTurma);
            }

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turma.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            List<Unidade> listaUnidade;
            using (RepositorioUnidade _repositorioLocal = new RepositorioUnidade(new CeagDbContext()))
            {
                listaUnidade = _repositorioLocal.SelecionarPorAnoEscola(turma.AnoLetivo, UsuarioEscola.ResgatarCodigoEscolaSelecionada()).ToList();
            }

            if (!listaUnidade.Where(p => p.Numero == numeroUnidade).Any())
            {
                return CarregarMensagemDeErro("Dados errados.");
            }

            if (!turma.TurmaFuncionarioDisciplinas.Any())
            {
                return RedirectToAction("Index", "AlunoMatriculaUnidades", new { codTurma, messageError = "ERRO: Sem professor na turma." });
            }

            #endregion

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            #region Tratamento dos dados e Consulta

            List<AlunoMatricula> listaAlunos;// = _repositorioGenericoAlunoMatricula.SelecionarPorIdTurma(codTurma);
            using (RepositorioAlunoMatricula _repoLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
            {
                listaAlunos = _repoLocal.SelecionarPorIdTurma(codTurma);
            }

            List<AlunoMatriculaUnidadeViewModel> listaAlunoMatriculaUnidade = new List<AlunoMatriculaUnidadeViewModel>();

            List<AlunoMatriculaUnidade> alunoMatriculaUnidadeBanco;// = _repositorioAlunoMatriculaUnidade.SelecionarPorTurma(codTurma);
            using (RepositorioAlunoMatriculaUnidade _repoLocal = new RepositorioAlunoMatriculaUnidade(new CeagDbContext()))
            {
                alunoMatriculaUnidadeBanco = _repoLocal.SelecionarPorTurma(codTurma);
            }

            using (RepositorioAlunoMatricula _repoLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
            {
                foreach (var item in listaAlunos)
                {
                    item.Turma = turma;
                    if (UsuarioEscola.ResgatarUsuarioLogado().AcessoRoles[0].Role.Descricao.Equals("PROFESSOR"))
                    {
                        foreach (var itemFuncionarioDisiciplinas in item.Turma.TurmaFuncionarioDisciplinas
                            .Where(p => UsuarioEscola.ResgatarUsuarioLogado().TurmaFuncionarioDisciplinas
                            .Select(s => s.CodDisciplina).Contains(p.CodDisciplina)))
                        {
                            AlunoMatriculaUnidadeViewModel alunoMatriculaUnidadeviewModel = new AlunoMatriculaUnidadeViewModel();

                            if (!listaAlunoMatriculaUnidade.Where(p => p.CodDisciplina == itemFuncionarioDisiciplinas.CodDisciplina
                            && p.CodFuncionario == itemFuncionarioDisiciplinas.CodFuncionario
                            && p.CodAlunoMatricula == item.CodAlunoMatricula).Any())
                            {
                                List<AlunoMatricula> alunoMatriculalist = _repoLocal.SelecionarPorIdAluno(item.CodAluno).Where(p => p.CodTurma == item.CodTurma).ToList();
                                foreach (var itemAlunoMatriculaUnidade in alunoMatriculaUnidadeBanco
                                    .Where(p => p.CodAlunoMatricula == item.CodAlunoMatricula
                                    && p.NumeroUnidade == numeroUnidade
                                    && p.CodDisciplina == itemFuncionarioDisiciplinas.CodDisciplina))
                                {
                                    alunoMatriculaUnidadeviewModel.NotaPC = itemAlunoMatriculaUnidade.NotaPC;
                                    alunoMatriculaUnidadeviewModel.NotaProva = itemAlunoMatriculaUnidade.NotaProva;
                                    alunoMatriculaUnidadeviewModel.NotaTeste = itemAlunoMatriculaUnidade.NotaTeste;
                                    alunoMatriculaUnidadeviewModel.NotaRecuperacao = itemAlunoMatriculaUnidade.NotaRecuperacao;
                                    alunoMatriculaUnidadeviewModel.NotaConselhoClasse = itemAlunoMatriculaUnidade.NotaConselhoClasse;
                                    alunoMatriculaUnidadeviewModel.CodAlunoMatriculaUnidade = itemAlunoMatriculaUnidade.CodAlunoMatriculaUnidade;
                                }

                                alunoMatriculaUnidadeviewModel.AlunoMatricula = mapper.Map<AlunoMatriculaViewModel>(alunoMatriculalist[0]);
                                alunoMatriculaUnidadeviewModel.AlunoMatricula.Aluno = mapper.Map<AlunoViewModel>(item.Aluno);
                                alunoMatriculaUnidadeviewModel.AlunoMatricula.Turma = mapper.Map<TurmaViewModel>(item.Turma);
                                alunoMatriculaUnidadeviewModel.CodAlunoMatricula = alunoMatriculaUnidadeviewModel.AlunoMatricula.CodAlunoMatricula;
                                alunoMatriculaUnidadeviewModel.CodFuncionario = itemFuncionarioDisiciplinas.CodFuncionario;
                                alunoMatriculaUnidadeviewModel.CodDisciplina = itemFuncionarioDisiciplinas.CodDisciplina;
                                alunoMatriculaUnidadeviewModel.Funcionario = mapper.Map<FuncionarioViewModel>(itemFuncionarioDisiciplinas.Funcionario);
                                alunoMatriculaUnidadeviewModel.Disciplina = mapper.Map<DisciplinaViewModel>(itemFuncionarioDisiciplinas.Disciplina);
                                alunoMatriculaUnidadeviewModel.NumeroUnidade = numeroUnidade;

                                listaAlunoMatriculaUnidade.Add(alunoMatriculaUnidadeviewModel);
                            }
                        }
                    }
                    else
                    {
                        foreach (var itemFuncionarioDisiciplinas in item.Turma.TurmaFuncionarioDisciplinas)
                        {
                            AlunoMatriculaUnidadeViewModel alunoMatriculaUnidadeviewModel = new AlunoMatriculaUnidadeViewModel();

                            if (!listaAlunoMatriculaUnidade.Where(p => p.CodDisciplina == itemFuncionarioDisiciplinas.CodDisciplina
                            && p.CodFuncionario == itemFuncionarioDisiciplinas.CodFuncionario
                            && p.CodAlunoMatricula == item.CodAlunoMatricula).Any())
                            {
                                List<AlunoMatricula> alunoMatriculalist = _repoLocal.SelecionarPorIdAluno(item.CodAluno).Where(p => p.CodTurma == item.CodTurma).ToList();
                                foreach (var itemAlunoMatriculaUnidade in alunoMatriculaUnidadeBanco
                                    .Where(p => p.CodAlunoMatricula == item.CodAlunoMatricula
                                    && p.NumeroUnidade == numeroUnidade
                                    && p.CodDisciplina == itemFuncionarioDisiciplinas.CodDisciplina))
                                {
                                    alunoMatriculaUnidadeviewModel.NotaPC = itemAlunoMatriculaUnidade.NotaPC;
                                    alunoMatriculaUnidadeviewModel.NotaProva = itemAlunoMatriculaUnidade.NotaProva;
                                    alunoMatriculaUnidadeviewModel.NotaTeste = itemAlunoMatriculaUnidade.NotaTeste;
                                    alunoMatriculaUnidadeviewModel.NotaConselhoClasse = itemAlunoMatriculaUnidade.NotaConselhoClasse;
                                    alunoMatriculaUnidadeviewModel.NotaRecuperacao = itemAlunoMatriculaUnidade.NotaRecuperacao;
                                    alunoMatriculaUnidadeviewModel.CodAlunoMatriculaUnidade = itemAlunoMatriculaUnidade.CodAlunoMatriculaUnidade;
                                }

                                alunoMatriculaUnidadeviewModel.AlunoMatricula = mapper.Map<AlunoMatriculaViewModel>(alunoMatriculalist[0]);
                                alunoMatriculaUnidadeviewModel.AlunoMatricula.Aluno = mapper.Map<AlunoViewModel>(item.Aluno);
                                alunoMatriculaUnidadeviewModel.AlunoMatricula.Turma = mapper.Map<TurmaViewModel>(item.Turma);
                                alunoMatriculaUnidadeviewModel.CodAlunoMatricula = alunoMatriculaUnidadeviewModel.AlunoMatricula.CodAlunoMatricula;
                                alunoMatriculaUnidadeviewModel.CodFuncionario = itemFuncionarioDisiciplinas.CodFuncionario;
                                alunoMatriculaUnidadeviewModel.CodDisciplina = itemFuncionarioDisiciplinas.CodDisciplina;
                                alunoMatriculaUnidadeviewModel.Funcionario = mapper.Map<FuncionarioViewModel>(itemFuncionarioDisiciplinas.Funcionario);
                                alunoMatriculaUnidadeviewModel.Disciplina = mapper.Map<DisciplinaViewModel>(itemFuncionarioDisiciplinas.Disciplina);
                                alunoMatriculaUnidadeviewModel.NumeroUnidade = numeroUnidade;

                                listaAlunoMatriculaUnidade.Add(alunoMatriculaUnidadeviewModel);
                            }
                        }
                    }

                }
            }
            AlunoMatriculaUnidadeComLista alunoMatriculaUnidade = new AlunoMatriculaUnidadeComLista
            {
                AlunoMatriculaUnidadeViewModels = listaAlunoMatriculaUnidade.OrderBy(p => p.AlunoMatricula.Aluno.Nome).ToList(),
                CodTurma = codTurma
            };

            #endregion

            ViewBag.NumeroUnidade = numeroUnidade;
            ViewBag.QuantidadeRegistros = alunoMatriculaUnidade.AlunoMatriculaUnidadeViewModels.Count();
            return View(alunoMatriculaUnidade);
        }

        // POST: AlunoMatriculaUnidades/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlunoMatriculaUnidadeComLista alunoMatriculaUnidadeComLista)
        {
            foreach (var itemAlunoMatriculaUnidadeViewModel in alunoMatriculaUnidadeComLista.AlunoMatriculaUnidadeViewModels)
            {
                AlunoMatriculaUnidade alunoMatriculaUnidadeBanco = new AlunoMatriculaUnidade();
                if (itemAlunoMatriculaUnidadeViewModel.CodAlunoMatriculaUnidade > 0)
                {
                    using (RepositorioAlunoMatriculaUnidade _repoLocal = new RepositorioAlunoMatriculaUnidade(new CeagDbContext()))
                    {
                        alunoMatriculaUnidadeBanco = _repoLocal.SelecionarPorId(itemAlunoMatriculaUnidadeViewModel.CodAlunoMatriculaUnidade);
                    }
                }

                if (itemAlunoMatriculaUnidadeViewModel.NotaTeste.HasValue)
                {
                    alunoMatriculaUnidadeBanco.NotaTeste = itemAlunoMatriculaUnidadeViewModel.NotaTeste.Value;
                }
                if (itemAlunoMatriculaUnidadeViewModel.NotaProva.HasValue)
                {
                    alunoMatriculaUnidadeBanco.NotaProva = itemAlunoMatriculaUnidadeViewModel.NotaProva.Value;
                }
                if (itemAlunoMatriculaUnidadeViewModel.NotaPC.HasValue)
                {
                    alunoMatriculaUnidadeBanco.NotaPC = itemAlunoMatriculaUnidadeViewModel.NotaPC.Value;
                }
                if (itemAlunoMatriculaUnidadeViewModel.NotaRecuperacao.HasValue)
                {
                    alunoMatriculaUnidadeBanco.NotaRecuperacao = itemAlunoMatriculaUnidadeViewModel.NotaRecuperacao.Value;
                }

                if (itemAlunoMatriculaUnidadeViewModel.NotaConselhoClasse.HasValue)
                {
                    alunoMatriculaUnidadeBanco.NotaConselhoClasse = itemAlunoMatriculaUnidadeViewModel.NotaConselhoClasse.Value;
                }

                if (itemAlunoMatriculaUnidadeViewModel.NotaPC.HasValue || itemAlunoMatriculaUnidadeViewModel.NotaProva.HasValue || itemAlunoMatriculaUnidadeViewModel.NotaTeste.HasValue)
                {
                    alunoMatriculaUnidadeBanco.CodAlunoMatricula = itemAlunoMatriculaUnidadeViewModel.CodAlunoMatricula;
                    alunoMatriculaUnidadeBanco.CodDisciplina = itemAlunoMatriculaUnidadeViewModel.CodDisciplina;
                    alunoMatriculaUnidadeBanco.CodFuncionario = itemAlunoMatriculaUnidadeViewModel.CodFuncionario;
                    alunoMatriculaUnidadeBanco.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                    alunoMatriculaUnidadeBanco.NumeroUnidade = itemAlunoMatriculaUnidadeViewModel.NumeroUnidade;
                    alunoMatriculaUnidadeBanco.Inclusao = DateTime.Now;
                    var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                    alunoMatriculaUnidadeBanco.CodAcesso = usuario.CodAcesso;

                    using (RepositorioAlunoMatriculaUnidade _repoLocal = new RepositorioAlunoMatriculaUnidade(new CeagDbContext()))
                    {
                        if (itemAlunoMatriculaUnidadeViewModel.CodAlunoMatriculaUnidade > 0)
                        {
                            _repoLocal.Alterar(alunoMatriculaUnidadeBanco);
                        }
                        else
                        {
                            _repoLocal.Inserir(alunoMatriculaUnidadeBanco);
                        }
                    }
                }
            }

            return RedirectToAction("Index", "AlunoMatriculaUnidades", new { codTurma = alunoMatriculaUnidadeComLista.CodTurma });
        }

        private int ListarFaltaAlunoPorUnidade(int codAlunoMatricula, int unidade, int ano, int codDisciplina)
        {
            int faltasRetorno = 0;
            AlunoMatricula alunoMatricula;
            using (RepositorioAlunoMatricula _repoLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
            {
                alunoMatricula = _repoLocal.SelecionarPorId(codAlunoMatricula);
            }

            List<Unidade> listaUnidade;
            using (RepositorioUnidade _repositorioLocal = new RepositorioUnidade(new CeagDbContext()))
            {
                listaUnidade = _repositorioLocal.SelecionarPorAnoEscola(DateTime.Now.Year, UsuarioEscola.ResgatarCodigoEscolaSelecionada()).ToList();
            }

            Unidade unidadeSelecionada = listaUnidade.Where(p => p.Numero == unidade).SingleOrDefault();
            if (unidadeSelecionada == null)
            {
                return 0;
            }
            List<Aula> aula;
            using (RepositorioAula _repoLocal = new RepositorioAula(new CeagDbContext()))
            {
                aula = _repoLocal.SelecionarPorIdTurma(alunoMatricula.CodTurma);
            }

            foreach (var item in aula.Where(p => p.Realizada >= unidadeSelecionada.Abertura
            && p.Realizada <= unidadeSelecionada.Fechamento
            && p.CodDisciplina == codDisciplina
            && p.Cancelamento == null))
            {
                if (item.AulaAlunos.Where(p => p.CodAlunoMatricula == codAlunoMatricula && p.Situacao.Equals(EnumComum.Chamada.AUSENTE.ToString())).Any())
                {
                    faltasRetorno++;
                }
            }
            return faltasRetorno;
        }

        public ActionResult ListaUnidade(int codTurma)
        {
            #region Validacao de Entrada na Tela

            Turma turma;
            using (RepositorioTurma _repoLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repoLocal.SelecionarPorId(codTurma);
            }

            if (turma == null)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != turma.CodEscola)
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            #endregion

            #region Método de consulta

            List<Unidade> listaUnidade;
            using (RepositorioUnidade _repositorioLocal = new RepositorioUnidade(new CeagDbContext()))
            {
                listaUnidade = _repositorioLocal.SelecionarPorAnoEscola(turma.AnoLetivo, UsuarioEscola.ResgatarCodigoEscolaSelecionada()).ToList();
            }

            List<int> numeroUnidades = listaUnidade.Select(p => p.Numero).ToList();

            List<AlunoMatriculaUnidade> alunoMatriculas;
            using (RepositorioAlunoMatriculaUnidade _repoLocal = new RepositorioAlunoMatriculaUnidade(new CeagDbContext()))
            {
                alunoMatriculas = _repoLocal.SelecionarPorTurma(codTurma).ToList();
            }
            #endregion

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.CodTurma = codTurma;

            return View(numeroUnidades);
        }

        #region Métodos de Apoio

        private void Paginacao(string sortOrder, string currentFilter, ref string searchString, ref int? page, out int pageSize, out int pageNumber)
        {
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
            pageSize = Constantes.PAGE_SIZE;
            pageNumber = (page ?? 1);
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

        #endregion
    }
}
