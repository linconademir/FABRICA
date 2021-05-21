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
using CEAG.WEB.ViewModel.Horario;
using PagedList;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class HorariosController : Controller
    {
        private IRepositorioGenerico<Horario, int> _repositorioGenericoHorario = new RepositorioHorario(new CeagDbContext());
        private IRepositorioGenerico<HorarioAula, int> _repositorioGenericoHorarioAula = new RepositorioHorarioAula(new CeagDbContext());

        // GET: Horarios
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
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

            var horarios = new List<Horario>();
            if (!string.IsNullOrEmpty(searchString))
            {
                horarios = _repositorioGenericoHorario.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                        .Where(p =>
                                    p.Descricao.ToUpper().Contains(searchString.ToUpper())
                        ).ToList();
            }
            else
            {
                horarios = _repositorioGenericoHorario.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }


            #endregion

            #region Mapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<HorarioExibicaoViewModel> dto = new List<HorarioExibicaoViewModel>();
            foreach (var item in horarios)
            {
                HorarioExibicaoViewModel horarioExibicaoViewModel = mapper.Map<HorarioExibicaoViewModel>(item);
                horarioExibicaoViewModel.HorarioAulaViewModels = mapper.Map<List<HorarioAulaViewModel>>(item.HorarioAulas);
                foreach (var itemAula in horarioExibicaoViewModel.HorarioAulaViewModels)
                {
                    var nomedia = string.Empty;
                    switch (itemAula.DiaSemana)
                    {
                        case 2:
                            nomedia  = "Segunda";
                            break;
                        case 3:
                            nomedia = "Terça";
                            break;
                        case 4:
                            nomedia = "Quarta";
                            break;
                        case 5:
                            nomedia = "Quinta";
                            break;
                        case 6:
                            nomedia = "Sexta";
                            break;
                        default:
                            nomedia = "Todos os dias";
                            break;
                    }

                    itemAula.NomeDiaSemana = nomedia;
                };
                dto.Add(horarioExibicaoViewModel);
            }


            #endregion

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            //return View(dto);
            return View(dto.ToPagedList(pageNumber, pageSize));
        }


        // GET: Horarios/Create
        public ActionResult Create()
        {
            

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View();
        }

        // POST: Horarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HorarioViewModel horarioViewModel)
        {
            
            horarioViewModel.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            horarioViewModel.Inclusao = DateTime.Now;

            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();


                var horario = mapper.Map<HorarioViewModel, Horario>(horarioViewModel);
                horario.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoHorario.Inserir(horario);

                
                return RedirectToAction("Index");
            }

            return View(horarioViewModel);
        }

        // GET: Horarios/Edit/5
        public ActionResult Edit(int? codHorario)
        {
            
            if (codHorario == null)
            {
                return CarregarMensagemDeErro("Horário não existe nesta escola.");
            }

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);

            Horario horarioConsulta = _repositorioGenericoHorario.SelecionarPorId(codHorario.Value);
            if (horarioConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Horário não existe nesta escola.");
            }

            HorarioViewModel dto = mapper.Map<HorarioViewModel>(horarioConsulta);
            dto.HorarioAulaViewModels = mapper.Map<List<HorarioAulaViewModel>>(horarioConsulta.HorarioAulas);

            foreach (var itemAula in dto.HorarioAulaViewModels)
            {
                Utils.VerificarNomeDiaSemana(itemAula);
            };

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            CarregarComboTela();

            if (dto == null)
            {
                return CarregarMensagemDeErro("Horário não existe nesta escola.");
            }

            return View(dto);
        }

       

        // POST: Horarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HorarioViewModel horarioViewModel)
        {
            
            horarioViewModel.Inclusao = DateTime.Now;
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                #region Alterando os dados do Horario

                var horario = mapper.Map<Horario>(horarioViewModel);
                horario.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoHorario.Alterar(horario);

                #endregion

             
                return RedirectToAction("Index");
            }
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            CarregarComboTela();
            return View(horarioViewModel);
        }

        // GET: Horarios/Delete/5
        public ActionResult Delete(int? codHorario)
        {
            
            if (codHorario == null)
            {
                return CarregarMensagemDeErro("Horário não existe nesta escola.");
            }
            var horarioBanco = _repositorioGenericoHorario.SelecionarPorId(codHorario.Value);
            if (horarioBanco.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Horário não existe nesta escola.");
            }

            if (horarioBanco == null)
            {
                return CarregarMensagemDeErro("Horário não existe nesta escola.");
            }

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);

            var horarioViewModel = mapper.Map<Horario, HorarioViewModel>(horarioBanco);
            horarioViewModel.HorarioAulaViewModels = mapper.Map<List<HorarioAulaViewModel>>(horarioBanco.HorarioAulas);

            return View(horarioViewModel);
        }

        // POST: Horarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codHorario)
        {
            
            var horarioBanco = _repositorioGenericoHorario.SelecionarPorId(codHorario);
            if (horarioBanco.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Horário não existe nesta escola.");
            }

            var horarioAulas = _repositorioGenericoHorario.SelecionarPorId(codHorario).HorarioAulas;
            foreach (var item in horarioAulas)
            {
                _repositorioGenericoHorarioAula.Excluir(item);
            }
            _repositorioGenericoHorario.ExcluirPorId(codHorario);
            return RedirectToAction("Index", "Horarios");
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

        #region CombosTela
        private void CarregarComboTela()
        {
            ViewBag.DropdownTurno = Utils.CarregarTurnos();
            ViewBag.DropdownDiaSemana = Utils.CarregarDiasSemana();
        }
        #endregion
    }
}
