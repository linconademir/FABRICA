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
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Funcionario;
using CEAG.WEB.ViewModel.FuncionarioDisciplina;
using CEAG.WEB.ViewModel.Horario;
using CEAG.WEB.ViewModel.Turma;
using CEAG.WEB.ViewModel.TurmaFuncionarioDisciplinas;
using PagedList;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class FuncionarioDisciplinasController : Controller
    {
        private IRepositorioGenerico<Disciplina, int> _repositorioGenericoDisciplina = new RepositorioDisciplina(new CeagDbContext());
        private IRepositorioGenerico<Funcionario, int> _repositorioGenericoFuncionario = new RepositorioFuncionario(new CeagDbContext());
        private RepositorioTurmaFuncionarioDisciplina _repositorioTurmaFuncionarioDisciplina = new RepositorioTurmaFuncionarioDisciplina(new CeagDbContext());
        private RepositorioHorario _repositorioHorario = new RepositorioHorario(new CeagDbContext());
        private RepositorioHorarioAula _repositorioHorarioAulas = new RepositorioHorarioAula(new CeagDbContext());

        // GET: FuncionarioDisciplinas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string erro)
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
            var funcionarioDisciplina = new List<FuncionarioDisciplina>();
            using (RepositorioFuncionarioDisciplina _repositorioFuncionarioDisciplina = new RepositorioFuncionarioDisciplina(new CeagDbContext()))
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    funcionarioDisciplina = _repositorioFuncionarioDisciplina.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                    .Where(p =>
                                    p.Disciplina.Descricao.ToUpper().Contains(searchString.ToUpper())
                                    || p.Funcionario.Nome.ToUpper().Contains(searchString.ToUpper())
                        ).ToList();
                }
                else
                {
                    funcionarioDisciplina = _repositorioFuncionarioDisciplina.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
                }
            }



            #endregion

            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<FuncionarioDisciplinaExibicaoViewModel> dto = new List<FuncionarioDisciplinaExibicaoViewModel>();

            foreach (var itemCodFuncionario in funcionarioDisciplina.Select(p => p.CodFuncionario).Distinct())
            {
                foreach (var item in funcionarioDisciplina.Where(p => p.CodFuncionario == itemCodFuncionario).Take(1))
                {
                    var dtoFuncionarioDisciplina = mapper.Map<FuncionarioDisciplinaExibicaoViewModel>(item);

                    dtoFuncionarioDisciplina.Funcionario = mapper.Map<FuncionarioExibicaoViewModel>(item.Funcionario);
                    dtoFuncionarioDisciplina.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(item.Disciplina);
                    foreach (var itemFuncionarioDisciplina in funcionarioDisciplina.Where(p => p.CodFuncionario == itemCodFuncionario))
                    {
                        dtoFuncionarioDisciplina.Disciplinas += (dtoFuncionarioDisciplina.Disciplinas == null ? itemFuncionarioDisciplina.Disciplina.Descricao : " | " + itemFuncionarioDisciplina.Disciplina.Descricao);
                    }

                    dto.Add(dtoFuncionarioDisciplina);
                }
            }
            #endregion
            ViewBag.MessageError = erro;
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(dto.ToPagedList(pageNumber, pageSize));
        }


        // GET: FuncionarioDisciplinas/Create
        public ActionResult Create(int codFuncionario)
        {


            if (codFuncionario == 0)
            {
                return CarregarMensagemDeErro("Professor não existe nesta escola.");
            }
            #region Mapper

            var funcionario = _repositorioGenericoFuncionario.SelecionarPorId(codFuncionario);
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != funcionario.CodEscola)
            {
                return CarregarMensagemDeErro("Professor não existe nesta escola.");
            }


            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);

            FuncionarioExibicaoViewModel dtoFuncionario = mapper.Map<FuncionarioExibicaoViewModel>(funcionario);

            #endregion

            List<FuncionarioDisciplina> listaDisciplinaFuncionario = new List<FuncionarioDisciplina>();
            using (RepositorioFuncionarioDisciplina _repositorioFuncionarioDisciplina = new RepositorioFuncionarioDisciplina(new CeagDbContext()))
            {
                listaDisciplinaFuncionario = _repositorioFuncionarioDisciplina.SelecionarDisciplinasPorProfessor(codFuncionario);
            }

            var disciplinas = _repositorioGenericoDisciplina.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());

            var Disciplinas = mapper.Map<List<DisciplinaViewModel>>(disciplinas);

            ViewBag.ListaDisciplinaFuncionario = mapper.Map<List<FuncionarioDisciplinaExibicaoViewModel>>(listaDisciplinaFuncionario);
            ViewBag.Funcionario = dtoFuncionario;

            //CarregarComboTela(codFuncionario);
            return View(Disciplinas);
        }

        // POST: FuncionarioDisciplinas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<int> Ids, int codFuncionario)
        {

            var usuario = UsuarioEscola.ResgatarUsuarioLogado();
            var funcionario = _repositorioGenericoFuncionario.SelecionarPorId(codFuncionario);
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != funcionario.CodEscola)
            {
                return CarregarMensagemDeErro("Professor não existe nesta escola.");
            }

            using (RepositorioFuncionarioDisciplina _repositorioFuncionarioDisciplina = new RepositorioFuncionarioDisciplina(new CeagDbContext()))
            {
                foreach (var item in Ids)
                {
                    FuncionarioDisciplina funcionarioDisciplina = new FuncionarioDisciplina();
                    funcionarioDisciplina.CodFuncionario = codFuncionario;
                    funcionarioDisciplina.Inclusao = DateTime.Now;
                    funcionarioDisciplina.CodDisciplina = item;
                    funcionarioDisciplina.CodAcesso = usuario.CodAcesso;
                    _repositorioFuncionarioDisciplina.Inserir(funcionarioDisciplina);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: FuncionarioDisciplinas/Edit/5
        public ActionResult Edit(int? codFuncionario)
        {


            if (codFuncionario == null)
            {
                return CarregarMensagemDeErro("Professor não existe nesta escola.");
            }

            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            var funcionario = _repositorioGenericoFuncionario.SelecionarPorId(codFuncionario.Value);

            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != funcionario.CodEscola)
            {
                return CarregarMensagemDeErro("Professor não existe nesta escola.");
            }

            FuncionarioExibicaoViewModel dtoFuncionario = mapper.Map<FuncionarioExibicaoViewModel>(funcionario);

            #endregion

            if (funcionario == null)
            {
                return CarregarMensagemDeErro("Professor não existe nesta escola.");
            }

            List<TurmaFuncionarioDisciplinaHorarioExibicaoViewModel> listaTurmaFuncionarioDisciplinaHorarioExibicaoViewModelTurma = CarregarHorarioDaTurma(codFuncionario.Value);
            List<TurmaFuncionarioDisciplinaHorarioExibicaoViewModel> listaTurmaFuncionarioDisciplinaHorarioExibicaoViewModelProfessor = CarregarHorarioDoProfessor(codFuncionario.Value);


            List<FuncionarioDisciplina> listaDisciplinaFuncionario = new List<FuncionarioDisciplina>();

            using (RepositorioFuncionarioDisciplina _repositorioFuncionarioDisciplina = new RepositorioFuncionarioDisciplina(new CeagDbContext()))
            {
                listaDisciplinaFuncionario = _repositorioFuncionarioDisciplina.SelecionarDisciplinasPorProfessor(codFuncionario.Value);
            }

            var disciplinasBanco = _repositorioGenericoDisciplina.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());

            var disciplinas = mapper.Map<List<DisciplinaViewModel>>(disciplinasBanco);

            foreach (var item in disciplinas)
            {
                item.Marcado = (listaDisciplinaFuncionario.Where(p => p.CodDisciplina == item.CodDisciplina).Any() ? true : false);
            }

            ViewBag.Funcionario = dtoFuncionario;
            ViewBag.ListaTurmaFuncionarioDisciplinaHorarioExibicaoViewModel = listaTurmaFuncionarioDisciplinaHorarioExibicaoViewModelTurma;
            ViewBag.ListaTurmaFuncionarioDisciplinaHorarioExibicaoViewModelProfessor = listaTurmaFuncionarioDisciplinaHorarioExibicaoViewModelProfessor;
            return View(disciplinas);
        }

        //// POST: FuncionarioDisciplinas/Edit/5
        //// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        //// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IEnumerable<int> Ids, int? codFuncionario)
        {


            if (codFuncionario == null)
            {
                return CarregarMensagemDeErro("Professor não existe nesta escola.");
            }

            var funcionario = _repositorioGenericoFuncionario.SelecionarPorId(codFuncionario.Value);
            if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != funcionario.CodEscola)
            {
                return CarregarMensagemDeErro("Professor não existe nesta escola.");
            }

            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            var usuario = UsuarioEscola.ResgatarUsuarioLogado();
            #endregion

            var listaDisciplinaFuncionario = new List<FuncionarioDisciplina>();

            using (RepositorioFuncionarioDisciplina _repositorioFuncionarioDisciplina = new RepositorioFuncionarioDisciplina(new CeagDbContext()))
            {
                listaDisciplinaFuncionario = _repositorioFuncionarioDisciplina.SelecionarDisciplinasPorProfessor(codFuncionario.Value);

                foreach (var item in listaDisciplinaFuncionario)
                {
                    _repositorioFuncionarioDisciplina.Excluir(item);
                }

                if (Ids != null)
                {
                    foreach (var item in Ids)
                    {
                        FuncionarioDisciplina funcionarioDisciplina = new FuncionarioDisciplina();
                        funcionarioDisciplina.CodFuncionario = codFuncionario.Value;
                        funcionarioDisciplina.Inclusao = DateTime.Now;
                        funcionarioDisciplina.CodDisciplina = item;
                        funcionarioDisciplina.CodAcesso = usuario.CodAcesso;
                        _repositorioFuncionarioDisciplina.Inserir(funcionarioDisciplina);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeletarProfessor(int codFuncionario)
        {

            var usuario = UsuarioEscola.ResgatarUsuarioLogado();

            var funcionarioDisciplinas = new List<FuncionarioDisciplina>();
            using (RepositorioFuncionarioDisciplina _repositorioFuncionarioDisciplina = new RepositorioFuncionarioDisciplina(new CeagDbContext()))
            {
                funcionarioDisciplinas = _repositorioFuncionarioDisciplina.SelecionarDisciplinasPorProfessor(codFuncionario);


                var funcionario = _repositorioGenericoFuncionario.SelecionarPorId(codFuncionario);

                if (UsuarioEscola.ResgatarCodigoEscolaSelecionada() != funcionario.CodEscola)
                {
                    return CarregarMensagemDeErro("Professor não existe nesta escola.");
                }

                foreach (var item in funcionarioDisciplinas)
                {
                    item.Cancelamento = DateTime.Now;
                    item.CodAcesso = usuario.CodAcesso;
                    _repositorioFuncionarioDisciplina.Alterar(item);
                }
            }
            return RedirectToAction("Index");
        }

        private List<TurmaFuncionarioDisciplinaHorarioExibicaoViewModel> CarregarHorarioDaTurma(int codFuncionario)
        {
            List<TurmaFuncionarioDisciplinaHorarioExibicaoViewModel> listaRetorno = new List<TurmaFuncionarioDisciplinaHorarioExibicaoViewModel>();

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            var funcionarioDisciplina = _repositorioTurmaFuncionarioDisciplina.SelecionarPorProfessor(codFuncionario);

            foreach (var itemPrincipal in funcionarioDisciplina.Select(p => p.CodTurma).Distinct())
            {

                var turmaFuncionarioDisciplina = _repositorioTurmaFuncionarioDisciplina.SelecionarPorTurma(itemPrincipal);
                List<TurmaFuncionarioDisciplinaExibicaoViewModel> listTurmaFuncionarioDisciplinaExibicaoViewModel = new List<TurmaFuncionarioDisciplinaExibicaoViewModel>();
                Turma turma = new Turma();
                List<HorarioAula> horariosTurma = new List<HorarioAula>();
                TurmaExibicaoViewModel turmaExibicaoViewModel = new TurmaExibicaoViewModel();
                if (turmaFuncionarioDisciplina.Any())
                {
                    turma = turmaFuncionarioDisciplina[0].Turma;
                    turmaExibicaoViewModel = mapper.Map<TurmaExibicaoViewModel>(turma);

                    foreach (var itemTurmaFuncionarioDisciplina in turmaFuncionarioDisciplina)
                    {
                        TurmaFuncionarioDisciplinaExibicaoViewModel turmaFuncionarioDisciplinaExibicaoViewModel = mapper.Map<TurmaFuncionarioDisciplinaExibicaoViewModel>(itemTurmaFuncionarioDisciplina);
                        turmaFuncionarioDisciplinaExibicaoViewModel.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(itemTurmaFuncionarioDisciplina.Disciplina);
                        turmaFuncionarioDisciplinaExibicaoViewModel.Funcionario = mapper.Map<FuncionarioExibicaoViewModel>(itemTurmaFuncionarioDisciplina.Funcionario);
                        listTurmaFuncionarioDisciplinaExibicaoViewModel.Add(turmaFuncionarioDisciplinaExibicaoViewModel);
                    }

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

                listaRetorno.Add(turmaFuncionarioDisciplinaHorarioExibicaoViewModel);

            }
            return listaRetorno;
        }

        public List<TurmaFuncionarioDisciplinaHorarioExibicaoViewModel> CarregarHorarioDoProfessor(int codFuncionario)
        {
            List<TurmaFuncionarioDisciplinaHorarioExibicaoViewModel> listaRetorno = new List<TurmaFuncionarioDisciplinaHorarioExibicaoViewModel>();

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            var funcionarioDisciplina = _repositorioTurmaFuncionarioDisciplina.SelecionarPorProfessor(codFuncionario);
            var turmaFuncionarioDisciplina = _repositorioTurmaFuncionarioDisciplina.SelecionarPorProfessor(codFuncionario);

            List<TurmaFuncionarioDisciplinaExibicaoViewModel> listTurmaFuncionarioDisciplinaExibicaoViewModel = new List<TurmaFuncionarioDisciplinaExibicaoViewModel>();
            List<HorarioAula> horariosTurma = new List<HorarioAula>();
            TurmaExibicaoViewModel turmaExibicaoViewModel = new TurmaExibicaoViewModel();
            if (turmaFuncionarioDisciplina.Any())
            {
                foreach (var itemTurmaFuncionarioDisciplina in turmaFuncionarioDisciplina)
                {
                    TurmaFuncionarioDisciplinaExibicaoViewModel turmaFuncionarioDisciplinaExibicaoViewModel = mapper.Map<TurmaFuncionarioDisciplinaExibicaoViewModel>(itemTurmaFuncionarioDisciplina);
                    turmaFuncionarioDisciplinaExibicaoViewModel.Disciplina = mapper.Map<DisciplinaExibicaoViewModel>(itemTurmaFuncionarioDisciplina.Disciplina);
                    turmaFuncionarioDisciplinaExibicaoViewModel.Funcionario = mapper.Map<FuncionarioExibicaoViewModel>(itemTurmaFuncionarioDisciplina.Funcionario);
                    turmaFuncionarioDisciplinaExibicaoViewModel.Turma = mapper.Map<TurmaExibicaoViewModel>(itemTurmaFuncionarioDisciplina.Turma);
                    listTurmaFuncionarioDisciplinaExibicaoViewModel.Add(turmaFuncionarioDisciplinaExibicaoViewModel);
                }

            }

            List<Horario> horario = new List<Horario>();
            using (RepositorioHorario _repoLocal = new RepositorioHorario(new CeagDbContext()))
            {
                horario = _repoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
                horariosTurma = horario[0].HorarioAulas;
            }

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
                                ,
                                Status = ""
                            };
                            Utils.VerificarNomeDiaSemana(aula);
                            horarioAulaViewModelParaTela.Add(aula);
                        }
                    }

                }


                TurmaFuncionarioDisciplinaHorarioExibicaoViewModel turmaFuncionarioDisciplinaHorarioExibicaoViewModel = new TurmaFuncionarioDisciplinaHorarioExibicaoViewModel();
                turmaFuncionarioDisciplinaHorarioExibicaoViewModel.HorarioAulaViewModels = horarioAulaViewModelParaTela;
                turmaFuncionarioDisciplinaHorarioExibicaoViewModel.TurmaExibicaoViewModel = turmaExibicaoViewModel;
                turmaFuncionarioDisciplinaHorarioExibicaoViewModel.TurmaFuncionarioDisciplinaExibicaoViewModels = listTurmaFuncionarioDisciplinaExibicaoViewModel;

                listaRetorno.Add(turmaFuncionarioDisciplinaHorarioExibicaoViewModel);

            }
            return listaRetorno;
        }

        private void CarregarComboTela(int codFuncionario)
        {

            var listaDisciplinaFuncionario = new List<FuncionarioDisciplina>();

            using (RepositorioFuncionarioDisciplina _repositorioFuncionarioDisciplina = new RepositorioFuncionarioDisciplina(new CeagDbContext()))
            {
                listaDisciplinaFuncionario = _repositorioFuncionarioDisciplina.SelecionarDisciplinasPorProfessor(codFuncionario);
            }

            var listaDisciplinaTotal = _repositorioGenericoDisciplina.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);


            ViewBag.ListaDisciplinaTotal = mapper.Map<List<DisciplinaExibicaoViewModel>>(listaDisciplinaTotal);
            ViewBag.ListaDisciplinaFuncionario = mapper.Map<List<FuncionarioDisciplinaExibicaoViewModel>>(listaDisciplinaFuncionario);
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
