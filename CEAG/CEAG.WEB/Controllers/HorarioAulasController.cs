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
using CEAG.WEB.ViewModel.Horario;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class HorarioAulasController : Controller
    {
        private IRepositorioGenerico<HorarioAula, int> _repositorioGenericoHorarioAula = new RepositorioHorarioAula(new CeagDbContext());
        private IRepositorioGenerico<Horario, int> _repositorioGenericoHorario = new RepositorioHorario(new CeagDbContext());

        // GET: HorarioAulas/Create
        public ActionResult Create(int codHorario)
        {
            
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            Horario horarioConsulta = _repositorioGenericoHorario.SelecionarPorId(codHorario);
            if (horarioConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Horário não existe nesta escola.");
            }

            CarregarComboTela();
            return View();
        }

        // POST: HorarioAulas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HorarioAulaViewModel horarioAulaViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
                var mapper = new Mapper(config);

                #region Inserindo as aulas do horario

                var horarioaula = mapper.Map<HorarioAulaViewModel, HorarioAula>(horarioAulaViewModel);
                horarioaula.CodAcesso = usuario.CodAcesso;
                _repositorioGenericoHorarioAula.Inserir(horarioaula);

                #endregion

                return RedirectToAction("Index", "Horarios");
            }
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();

            CarregarComboTela();
            return View(horarioAulaViewModel);
        }

        //POST: HorarioAulas/Delete/5
        
        public ActionResult Delete(int codHorarioAula)
        {
            HorarioAula horarioConsulta = _repositorioGenericoHorarioAula.SelecionarPorId(codHorarioAula);
            if (horarioConsulta.Horario.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Horário não existe nesta escola.");
            }

            var horarioAulas = _repositorioGenericoHorarioAula.SelecionarPorId(codHorarioAula);
            _repositorioGenericoHorarioAula.Excluir(horarioAulas);

            return RedirectToAction("Edit", "Horarios", new { codHorario = horarioAulas.CodHorario});
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
            #region Turno

            var listaTurno = new List<SelectListItem>
            {
                new SelectListItem { Value = "MANHÃ", Text = "MANHÃ" },
                new SelectListItem { Value = "TARDE", Text = "TARDE" },
                new SelectListItem { Value = "NOITE", Text = "NOITE" }
            };

            var dropdownTurno = new SelectList(listaTurno, "Value", "Text");

            #endregion

            #region Dia da Semana

            var listaDiaSemana = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "TODOS OS DIAS" },
                new SelectListItem { Value = "2", Text = "SEGUNDA" },
                new SelectListItem { Value = "3", Text = "TERÇA" },
                new SelectListItem { Value = "4", Text = "QUARTA" },
                new SelectListItem { Value = "5", Text = "QUINTA" },
                new SelectListItem { Value = "6", Text = "SEXTA" }
            };

            var dropdownDiaSemana = new SelectList(listaDiaSemana, "Value", "Text");

            #endregion

            ViewBag.DropdownTurno = dropdownTurno;
            ViewBag.DropdownDiaSemana = dropdownDiaSemana;
        }
        #endregion
    }
}
