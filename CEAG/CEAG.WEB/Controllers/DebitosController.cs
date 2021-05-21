using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.NEGOCIO.Classes;
using CEAG.NEGOCIO.Enum;
using CEAG.NEGOCIO.Utils;
using CEAG.REPOSITORIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.Debito;
using CEAG.WEB.ViewModel.MensalidadeValores;
using CEAG.WEB.ViewModel.Responsavel;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using PagedList;

namespace CEAG.WEB.Controllers
{
    public class DebitosController : Controller
    {
        #region Index e Details

        // GET: Debitos
        public ActionResult Index(int codAlunoMatricula, string sortOrder, string currentFilter, string searchString, int? page)
        {
            #region Paginação

            int pageSize, pageNumber;
            GerarControledeFiltroPagina(sortOrder, currentFilter, ref searchString, ref page, out pageSize, out pageNumber);

            #endregion

            #region Método de consulta

            var debitos = new List<Debito>();
            using (RepositorioDebito _repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                debitos = _repositorioDebitoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                        .Where(p =>
                                    p.CodAlunoMatricula == codAlunoMatricula

                        ).ToList();
            }

            #endregion


            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            List<DebitoExibicaoViewModel> dto = new List<DebitoExibicaoViewModel>();
            foreach (var item in debitos)
            {
                DebitoExibicaoViewModel debitoExibicao = new DebitoExibicaoViewModel();
                debitoExibicao = mapper.Map<DebitoExibicaoViewModel>(item);
                debitoExibicao.AlunoExibicaoViewModel = mapper.Map<AlunoExibicaoViewModel>(item.AlunoMatricula.Aluno);
                dto.Add(debitoExibicao);
            }


            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.CodAlunoMatricula = codAlunoMatricula;

            return View(dto.Any() ? dto.OrderBy(p => p.Vencimento).ToPagedList(pageNumber, pageSize) : dto.ToPagedList(pageNumber, pageSize));
        }

        private void GerarControledeFiltroPagina(string sortOrder, string currentFilter, ref string searchString, ref int? page, out int pageSize, out int pageNumber)
        {
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

            pageSize = Constantes.PAGE_SIZE;
            pageNumber = (page ?? 1);
        }

        #endregion

        #region Create

        // GET: Debitos/Create
        public ActionResult Create(int codAlunoMatricula)
        {

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            DebitoViewModel debito = new DebitoViewModel();
            debito.CodAlunoMatricula = codAlunoMatricula;
            CarregarComboTela();
            return View(debito);
        }

        // POST: Debitos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DebitoViewModel debitoViewModel)
        {


            if (ModelState.IsValid)
            {
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();

                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion


                #region Inserindo as parcelas
                Aluno aluno = new Aluno();
                using (RepositorioAlunoMatricula _repoLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
                {
                    aluno = _repoLocal.SelecionarPorId(debitoViewModel.CodAlunoMatricula).Aluno;
                }

                List<Responsavel> responsavelFinanceiro = new List<Responsavel>();
                using (RepositorioAluno _repositorioGenericoAlunoLocal = new RepositorioAluno(new CeagDbContext()))
                {
                    responsavelFinanceiro = _repositorioGenericoAlunoLocal.SelecionarPorId(aluno.CodAluno).Responsavels.Where(p => p.Tipo.Equals("FINANCEIRO") || p.Tipo.Equals("LEGAL")).ToList();
                }

                List<Debito> debitos = new List<Debito>();

                #region Geração de Mensalidades

                for (int i = 1; i <= debitoViewModel.Parcelas; i++)
                {
                    DateTime dataTrabalhada = debitoViewModel.Vencimento.AddMonths((i - 1));

                    Debito debitoNegociacao = new Debito();
                    debitoNegociacao.CodAlunoMatricula = debitoViewModel.CodAlunoMatricula;
                    debitoNegociacao.CodAcesso = usuario.CodAcesso;
                    debitoNegociacao.Descricao = debitoViewModel.Descricao + " [ " + (i).ToString() + @"/" + debitoViewModel.Parcelas.ToString() + " ] ";
                    debitoNegociacao.ValorDebito = (debitoViewModel.ValorDebito / debitoViewModel.Parcelas);

                    debitoNegociacao.Vencimento = debitoViewModel.Parcelas > 1 ? new DateTime(dataTrabalhada.Year, dataTrabalhada.Month, DateTime.DaysInMonth(dataTrabalhada.Year, dataTrabalhada.Month)) : debitoViewModel.Vencimento;
                    debitoNegociacao.Periodo = new DateTime(dataTrabalhada.Year, dataTrabalhada.Month, 1);
                    debitoNegociacao.TipoDebito = debitoViewModel.TipoDebito;
                    debitoNegociacao.Inclusao = DateTime.Now;
                    debitoNegociacao.Observacao = "INCLUSÃO DE DE BITO AVULSO: " + " [" + usuario.Nome + " '" + DateTime.Now.ToShortDateString() + "'] - PARCELA " + i + @"/" + debitoViewModel.Parcelas.ToString();
                    debitoNegociacao.FormaPagamento = debitoViewModel.FormaPagamento;
                    if (responsavelFinanceiro.Where(p => p.Tipo.Equals("FINANCEIRO")).Any())
                    {
                        debitoNegociacao.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("FINANCEIRO")).Nome;
                    }
                    else
                    {
                        debitoNegociacao.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("LEGAL")).Nome;
                    }

                    debitos.Add(debitoNegociacao);
                }

                InserirDebito(debitos);

                #endregion

                #endregion


                return RedirectToAction("Index", "Debitos", new { codAlunoMatricula = debitoViewModel.CodAlunoMatricula });
            }


            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            DebitoViewModel debitoRetorno = new DebitoViewModel();
            debitoRetorno.CodAlunoMatricula = debitoViewModel.CodAlunoMatricula;
            CarregarComboTela();
            ViewBag.CodAlunoMatricula = debitoViewModel.CodAlunoMatricula;

            return View(debitoViewModel);
        }

        private void InserirDebito(List<Debito> debitoManual)
        {
            using (RepositorioDebito _repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                foreach (var item in debitoManual)
                {
                    _repositorioDebitoLocal.Inserir(item);
                }
            }
        }
        #endregion

        #region Edit
        // GET: Debitos/Edit/5
        public ActionResult Edit(int? codDebito)
        {
            if (!codDebito.HasValue)
            {
                return CarregarMensagemDeErro("Débito Inexistente.");
            }



            Debito debito = new Debito();

            using (RepositorioDebito repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                debito = repositorioDebitoLocal.SelecionarPorId(codDebito.Value);
            }

            if (debito == null)
            {
                return CarregarMensagemDeErro("Débito Inexistente.");
            }

            Turma turma = new Turma();
            using (RepositorioTurma repositorioTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = repositorioTurmaLocal.SelecionarPorId(debito.AlunoMatricula.CodTurma);
            }

            if (turma == null || turma.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Débito Inexistente.");
            }
            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            CarregarComboTela();
            DebitoViewModel debitoViewModel = mapper.Map<DebitoViewModel>(debito);
            return View(debitoViewModel);
        }

        // POST: Debitos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DebitoViewModel debitoViewModel)
        {

            debitoViewModel.Inclusao = DateTime.Now;

            if (ModelState.IsValid)
            {
                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var debito = mapper.Map<DebitoViewModel, Debito>(debitoViewModel);
                debito.CodAcesso = usuario.CodAcesso;

                string valorAnterior = string.Empty;
                string dataAnterior = string.Empty;
                string descontoAnterior = string.Empty;
                string descricaoAnterior = string.Empty;
                string formaPagamentoAnterior = string.Empty;
                string tipoAnterior = string.Empty;

                using (RepositorioDebito repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
                {
                    var debitoLocal = repositorioDebitoLocal.SelecionarPorId(debito.CodDebito);

                    valorAnterior = debitoLocal.ValorDebito.ToString("N2");
                    dataAnterior = debitoLocal.Vencimento.ToString("dd/MM/yyyy");
                    descontoAnterior = debitoLocal.Desconto.HasValue ? debitoLocal.Desconto.Value.ToString("N2") : " Sem desconto ";
                    descricaoAnterior = debitoLocal.Descricao;
                    formaPagamentoAnterior = debitoLocal.FormaPagamento;
                    tipoAnterior = debitoLocal.TipoDebito;
                }

                using (RepositorioDebito repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
                {
                    repositorioDebitoLocal.Alterar(debito);
                }

                string obs = "Valor anterior: " + valorAnterior + " -> Valor novo: " + debito.ValorDebito +
                    " | Data anterior: " + dataAnterior + " -> Data nova: " + debito.Vencimento.ToString("dd/MM/yyyy") +
                    " | Desconto anterior: " + descontoAnterior + " -> Desconto novo: " + (debito.Desconto.HasValue ? debito.Desconto.Value.ToString("N2") : " Sem desconto ") +
                    " | Descrição anterior: ( " + descricaoAnterior + " ) -> Descrição nova: " + debito.Descricao +
                    " | Forma pagamento anterior: ( " + formaPagamentoAnterior + " ) -> Forma pagamento nova: " + debito.FormaPagamento +
                    " | Tipo anterior: " + tipoAnterior + " -> Tipo novo: " + debito.TipoDebito;

                InserirRegistroHistoricoDebito(debito.CodDebito, "Alteração realizada por usuário: " + UsuarioEscola.ResgatarUsuarioLogado().Nome, obs);

                return RedirectToAction("Index", "Debitos", new { codAlunoMatricula = debitoViewModel.CodAlunoMatricula });
            }

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            CarregarComboTela();

            return View(debitoViewModel);
        }

        #endregion

        #region Realizar baixa


        //GET: Debitos/RealizarBaixa/5
        public ActionResult RealizarBaixa(int? codDebito)
        {
            if (!codDebito.HasValue)
            {
                return CarregarMensagemDeErro("Débito Inexistente.");
            }


            Debito debito = new Debito();

            using (RepositorioDebito repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                debito = repositorioDebitoLocal.SelecionarPorId(codDebito.Value);
            }

            if (debito == null)
            {
                return CarregarMensagemDeErro("Débito Inexistente.");
            }

            if (debito.Pagamento.HasValue)
            {
                return CarregarMensagemDeErro("Débito Inexistente.");
            }

            Turma turma = new Turma();
            using (RepositorioTurma repositorioTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = repositorioTurmaLocal.SelecionarPorId(debito.AlunoMatricula.CodTurma);
            }

            if (turma == null || turma.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Débito Inexistente.");
            }

            MensalidadeValor mensalidadeValor = new MensalidadeValor();
            using (RepositorioMensalidadeValor _repoLocal = new RepositorioMensalidadeValor(new CeagDbContext()))
            {
                mensalidadeValor = _repoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).Where(p => p.Turma.ToUpper().Contains(turma.Segmento.ToUpper())).Take(1).SingleOrDefault();
            }



            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            AlunoExibicaoViewModel aluno = new AlunoExibicaoViewModel();
            using (RepositorioAluno _repoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                aluno = mapper.Map<AlunoExibicaoViewModel>(_repoLocal.SelecionarPorId(debito.AlunoMatricula.CodAluno));
            }



            ViewBag.QuintoDiaUtil = Utilitarios.CalculaDiasUteis(debito.Periodo, 4);// Passa 4 como parametro porque o sistema conta após a data inicial, somando 5 dias úteis
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.MensalidadeValor = mapper.Map<MensalidadeValorExibicaoViewModel>(mensalidadeValor);
            ViewBag.Aluno = aluno;
            CarregarComboTela();
            DebitoViewModel debitoViewModel = mapper.Map<DebitoViewModel>(debito);
            return View(debitoViewModel);
        }

        // POST: Debitos/RealizarBaixa/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RealizarBaixa(DebitoViewModel debitoViewModel)
        {
            if (debitoViewModel.Pagamento.HasValue)
            {
                ModelState.AddModelError("", "Pagamento do débito já foi realizado anteriormente.");
            }

            debitoViewModel.Pagamento = DateTime.Now;

            if (ModelState.IsValid)
            {
                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                var debito = mapper.Map<DebitoViewModel, Debito>(debitoViewModel);

                debito.CodAcesso = usuario.CodAcesso;
                debito.Observacao = debito.Observacao + " [" + UsuarioEscola.ResgatarUsuarioLogado().Nome + " '" + DateTime.Now.Date.ToShortDateString() + "']";

                using (NEGOCIO.Utils.Security _security = new NEGOCIO.Utils.Security())
                {
                    debito.IdenficadorHexa = _security.GeradorIdentificador(20, "PAG", debito.TipoDebito.Substring(1, 4), UsuarioEscola.ResgatarEscolaLogada());
                    //debito.QrCode = imagem.ImagemQrCode;
                }

                debito.NumeroDebito = CarregarProximaNumeracao();

                using (RepositorioDebito repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
                {
                    repositorioDebitoLocal.Alterar(debito);
                }

                return RedirectToAction("Index", "Debitos", new { codAlunoMatricula = debitoViewModel.CodAlunoMatricula });
            }

            #region Mapper
            Mapper mapper2 = Constants.Utils.DominioParaViewModel();
            #endregion

            Debito debitoConsulta = new Debito();
            using (RepositorioDebito repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                debitoConsulta = repositorioDebitoLocal.SelecionarPorId(debitoViewModel.CodDebito);
            }

            AlunoExibicaoViewModel aluno = new AlunoExibicaoViewModel();
            using (RepositorioAluno _repoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                aluno = mapper2.Map<AlunoExibicaoViewModel>(_repoLocal.SelecionarPorId(debitoConsulta.AlunoMatricula.CodAluno));
            }

            Turma turma = new Turma();
            using (RepositorioTurma repositorioTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = repositorioTurmaLocal.SelecionarPorId(debitoConsulta.AlunoMatricula.CodTurma);
            }

            MensalidadeValor mensalidadeValor = new MensalidadeValor();
            using (RepositorioMensalidadeValor _repoLocal = new RepositorioMensalidadeValor(new CeagDbContext()))
            {
                mensalidadeValor = _repoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).Where(p => p.Turma.ToUpper().Contains(turma.Segmento.ToUpper())).Take(1).SingleOrDefault();
            }

            ViewBag.QuintoDiaUtil = Utilitarios.CalculaDiasUteis(debitoViewModel.Periodo, 4);// Passa 4 como parametro porque o sistema conta após a data inicial, somando 5 dias úteis
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.MensalidadeValor = mapper2.Map<MensalidadeValorExibicaoViewModel>(mensalidadeValor);
            ViewBag.Aluno = aluno;
            CarregarComboTela();

            return View(debitoViewModel);
        }

        private int CarregarProximaNumeracao()
        {
            int retorno = 0;
            using (RepositorioDebito repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                retorno = repositorioDebitoLocal.SelecionarQuantidadeRegistros(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }
            return retorno;
        }

        #endregion

        //GET: Detalhar debitos por aluno
        public ActionResult DetalharDebitoAluno(string sortOrder, string currentFilter, string searchString, int? page, string searchAno, int? codAluno)
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

            if (!codAluno.HasValue)
            {
                return CarregarMensagemDeErro("Operação ilegal.");
            }

            var debitos = new List<Debito>();
            using (RepositorioDebito _repositorioDebito = new RepositorioDebito(new CeagDbContext()))
            {
                debitos = _repositorioDebito.SelecionarPorAluno(codAluno.Value);
            }

            #endregion

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion
            List<DebitoExibicaoViewModel> dto = new List<DebitoExibicaoViewModel>();

            foreach (var item in debitos)
            {
                DebitoExibicaoViewModel debitoExibicaoViewModel = mapper.Map<DebitoExibicaoViewModel>(item);
                debitoExibicaoViewModel.AlunoExibicaoViewModel = mapper.Map<AlunoExibicaoViewModel>(item.AlunoMatricula.Aluno);
                debitoExibicaoViewModel.AlunoMatriculaExibicaoViewModel = mapper.Map<AlunoMatriculaExibicaoViewModel>(item.AlunoMatricula);
                debitoExibicaoViewModel.DebitoHistoricoExibicaoViewModels = mapper.Map<List<DebitoHistoricoExibicaoViewModel>>(item.DebitoHistoricos);

                dto.Add(debitoExibicaoViewModel);
            }

            return View(dto.OrderBy(p => p.Vencimento).OrderByDescending(p => p.Status).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ValidarQrCode(string searchString)
        {
            #region Método de consulta

            List<Debito> debitos = new List<Debito>();
            using (RepositorioDebito _repoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    debitos = _repoLocal.SelecionarPorCodigoHexa(UsuarioEscola.ResgatarCodigoEscolaSelecionada(), searchString);
                }
            }
            #endregion

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            List<DebitoExibicaoViewModel> debitosExibicao = new List<DebitoExibicaoViewModel>();
            foreach (var item in debitos)
            {
                DebitoExibicaoViewModel debito = new DebitoExibicaoViewModel();
                debito = mapper.Map<DebitoExibicaoViewModel>(item);
                debito.AlunoExibicaoViewModel = mapper.Map<AlunoExibicaoViewModel>(item.AlunoMatricula.Aluno);
                debitosExibicao.Add(debito);
            }

            return View(debitosExibicao);
        }

        public ActionResult IndexCobranca(string tipoGrafico)
        {
            int codEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            DebitoGeralExibicaoViewModel debitoGeralExibicaoViewModel = new DebitoGeralExibicaoViewModel();

            List<Debito> debitosEmAberto;
            List<Debito> debitosAno;
            List<Debito> debitosTodosEmAberto;
            using (RepositorioDebito _repoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                debitosTodosEmAberto = _repoLocal.SelecionarDebitosAberto(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
                debitosEmAberto = _repoLocal.SelecionarDebitosAberto(codEscola).Where(p => p.Vencimento < DateTime.Now).ToList();
                debitosAno = _repoLocal.SelecionarDebitosAno(codEscola, DateTime.Now.Year);
            }

            debitoGeralExibicaoViewModel.QtdDebitosVencidosMesesGeral = debitosEmAberto.Count();
            debitoGeralExibicaoViewModel.QtdDebitosVencidosNoMes = debitosEmAberto.Where(p => p.Periodo.Month == DateTime.Now.Month).Count();
            debitoGeralExibicaoViewModel.QtdAlunoPagamEmDia = debitosAno.Where(p => p.Pagamento.HasValue && p.Pagamento.Value.Month <= p.Periodo.Month).Count();
            debitoGeralExibicaoViewModel.QtdAlunosJaAtrasaram = debitosAno.Where(p => p.Pagamento.HasValue && p.Pagamento.Value.Month > p.Periodo.Month).Count();
            //debitoGeralExibicaoViewModel.ValorAlunoJaAtrasaram = debitosAno.Where(p => p.Pagamento.HasValue && p.Pagamento.Value.Month > p.Periodo.Month).Sum(p => p.ValorPago.Value);
            //debitoGeralExibicaoViewModel.ValorAlunosPagamEmDia = debitosAno.Where(p => p.Pagamento.HasValue && p.Pagamento.Value.Month <= p.Periodo.Month).Sum(p => p.ValorPago.Value);
            debitoGeralExibicaoViewModel.ValorDebitosVencidosNoMes = debitosEmAberto.Where(p => p.Periodo.Month == DateTime.Now.Month).Sum(p => p.ValorDebito);
            debitoGeralExibicaoViewModel.ValorDebitosVencidosMesesGeral = debitosEmAberto.Sum(p => p.ValorDebito);

            debitoGeralExibicaoViewModel.MesPagosEAtrasados = new List<QtdDebitoExibicaoViewModel>();
            debitoGeralExibicaoViewModel.AnoPagosEAtrasados = new List<QtdDebitoExibicaoViewModel>();

            for (int i = 1; i <= 12; i++)
            {
                QtdDebitoExibicaoViewModel mes = new QtdDebitoExibicaoViewModel();
                mes.DescricaoNumeral = i;
                mes.DescricaoNome = DateTimeFormatInfo.CurrentInfo.GetMonthName(i).ToLower();
                //Para saber o valor que tá atrasado mês a mês e JÁ FOI PAGO
                mes.QtdPagoAtrasado = debitosAno.Where(p => p.Periodo.Month == i && (p.Pagamento.HasValue && p.Pagamento.Value > p.Periodo)).Count();
                mes.ValorPagoAtrasado = debitosAno.Where(p => p.Periodo.Month == i && (p.Pagamento.HasValue && p.Pagamento.Value > p.Periodo)).Sum(p => p.ValorPago.Value);

                //Para saber a quantidade de pagamento do mês que foram feitos dentro do Mês
                mes.QtdPagoDoPeriodo = debitosAno.Where(p => p.Periodo.Month == i && p.Pagamento.HasValue && p.Pagamento.Value.Month == i).Count();
                mes.ValorPagoDoPeriodo = debitosAno.Where(p => p.Periodo.Month == i && p.Pagamento.HasValue && p.Vencimento >= p.Pagamento.Value).Sum(p => p.ValorPago.Value);

                //Para saber a quantidade de pagamento dos outros meses passados mas que foram pagos no mês consultado
                mes.QtdPagoGeral = debitosAno.Where(p => p.Pagamento.HasValue && p.Pagamento.Value.Month == i).Count();
                mes.ValorPagoGeral = debitosAno.Where(p => p.Pagamento.HasValue && p.Pagamento.Value.Month == i).Sum(p => p.ValorPago.Value);

                //Para saber a quantidade de debitos em aberto que está atrasado, mês a mês
                mes.QtdAtrasadoEmAberto = debitosAno.Where(p => p.Periodo.Month == i && !p.Pagamento.HasValue).Count();
                mes.ValorAtrasadoEmAberto = debitosAno.Where(p => p.Periodo.Month == i && !p.Pagamento.HasValue).Sum(p => p.ValorDebito);
                debitoGeralExibicaoViewModel.MesPagosEAtrasados.Add(mes);
            }


            foreach (var item in debitosTodosEmAberto.OrderBy(p => p.Periodo.Year).Select(p => p.Periodo.Year).Distinct())
            {
                QtdDebitoExibicaoViewModel ano = new QtdDebitoExibicaoViewModel();
                ano.DescricaoNome = item.ToString();
                ano.DescricaoNumeral = item;

                //Para saber o valor que tá atrasado mês a mês e JÁ FOI PAGO
                ano.QtdPagoAtrasado = debitosTodosEmAberto.Where(p => p.Periodo.Year == item && (p.Pagamento.HasValue && p.Pagamento.Value > p.Periodo)).Count();
                ano.ValorPagoAtrasado = debitosTodosEmAberto.Where(p => p.Periodo.Year == item && (p.Pagamento.HasValue && p.Pagamento.Value > p.Periodo)).Sum(p => p.ValorPago.Value);

                //Para saber a quantidade de pagamento do mês que foram feitos dentro do Mês
                ano.QtdPagoDoPeriodo = debitosTodosEmAberto.Where(p => p.Periodo.Year == item && p.Pagamento.HasValue && p.Pagamento.Value.Month == item).Count();
                ano.ValorPagoDoPeriodo = debitosTodosEmAberto.Where(p => p.Periodo.Year == item && p.Pagamento.HasValue).Sum(p => p.ValorPago.Value);

                //Para saber a quantidade de pagamento dos outros meses passados mas que foram pagos no mês consultado
                ano.QtdPagoGeral = debitosTodosEmAberto.Where(p => p.Pagamento.HasValue && p.Pagamento.Value.Year == item).Count();
                ano.ValorPagoGeral = debitosTodosEmAberto.Where(p => p.Pagamento.HasValue && p.Pagamento.Value.Year == item).Sum(p => p.ValorPago.Value);

                //Para saber a quantidade de debitos em aberto que está atrasado, mês a mês
                ano.QtdAtrasadoEmAberto = debitosTodosEmAberto.Where(p => p.Periodo.Year == item && p.Vencimento <= DateTime.Now).Count();
                ano.ValorAtrasadoEmAberto = debitosTodosEmAberto.Where(p => p.Periodo.Year == item && p.Vencimento <= DateTime.Now).Sum(p => p.ValorDebito);

                debitoGeralExibicaoViewModel.AnoPagosEAtrasados.Add(ano);

            }

            debitoGeralExibicaoViewModel.Graficos = new List<Highcharts>();

            if (!string.IsNullOrEmpty(tipoGrafico) && tipoGrafico.Equals("MENSAL"))
            {
                debitoGeralExibicaoViewModel.Graficos.Add(GraficoColuna(debitoGeralExibicaoViewModel.MesPagosEAtrasados, "mes"));
            }
            else
            {
                debitoGeralExibicaoViewModel.Graficos.Add(GraficoColuna(debitoGeralExibicaoViewModel.AnoPagosEAtrasados, "ano"));
            }
            ViewBag.TipoGrafico = tipoGrafico;
            return View(debitoGeralExibicaoViewModel);

        }

        private Highcharts GraficoColuna(List<QtdDebitoExibicaoViewModel> qtdDebitoExibicaoViewModels, string tipo)
        {
            Highcharts columnChart = new Highcharts("columnchart");
            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.AliceBlue),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 2
            });

            columnChart.SetTitle(new Title()
            {
                Text = UsuarioEscola.ResgatarEscolaLogada().Fantasia + " - Mensal"
            });

            columnChart.SetSubtitle(new Subtitle()
            {
                Text = "Acompanhamento de pagamentos mensais"
            });

            if (tipo.Equals("ano"))
            {
                columnChart.SetTitle(new Title()
                {
                    Text = UsuarioEscola.ResgatarEscolaLogada().Fantasia + " - Anual"
                });

                int cont = 0;
                int tamanho = qtdDebitoExibicaoViewModels.Select(p => p.DescricaoNumeral).Distinct().Count();
                string[] listaAno = new string[tamanho];
                foreach (var item in qtdDebitoExibicaoViewModels.OrderBy(p => p.DescricaoNumeral).Distinct())
                {
                    listaAno[cont] = item.DescricaoNome;
                    cont = cont + 1;
                }

                columnChart.SetSubtitle(new Subtitle()
                {
                    Text = "Acompanhamento de pagamentos anuais"
                });

                columnChart.SetXAxis(new XAxis()
                {
                    Type = AxisTypes.Category,
                    Title = new XAxisTitle() { Text = "Ano", Style = "fontWeight: 'bold', fontSize: '17px'" },
                    Categories = listaAno //Categories = new[] { "JANEIRO", "FEVEREIRO", "MARÇO", "ABRIL", "MAIO", "JUNHO", "JULHO", "AGOSTO", "SETEMBRO", "OUTUBRO", "NOVEMBRO", "DEZEMBRO" }
                });
            }
            else
            {
                columnChart.SetSubtitle(new Subtitle()
                {
                    Text = "Acompanhamento de pagamentos mensais"
                });
                columnChart.SetXAxis(new XAxis()
                {
                    Type = AxisTypes.Category,
                    Title = new XAxisTitle() { Text = "Mês", Style = "fontWeight: 'bold', fontSize: '17px'" },
                    Categories = new[] { "JANEIRO", "FEVEREIRO", "MARÇO", "ABRIL", "MAIO", "JUNHO", "JULHO", "AGOSTO", "SETEMBRO", "OUTUBRO", "NOVEMBRO", "DEZEMBRO" }
                });
            }

            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "Dados",
                    Style = "fontWeight: 'bold', fontSize: '17px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0
            });

            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFADD8E6"))
            });


            object[] emAbertoValor = new object[12];
            object[] pagos = new object[12];

            if (tipo.Equals("ano"))
            {
                int cont = 0;
                int tamanho = qtdDebitoExibicaoViewModels.Select(p => p.DescricaoNumeral).Distinct().Count();
                emAbertoValor = new object[tamanho];
                pagos = new object[tamanho];

                foreach (var item in qtdDebitoExibicaoViewModels)
                {
                    emAbertoValor[cont] = item.ValorAtrasadoEmAberto;
                    pagos[cont] = item.ValorPagoDoPeriodo;
                    cont = cont + 1;
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    emAbertoValor[i] = qtdDebitoExibicaoViewModels.Where(p => p.DescricaoNumeral == i + 1).Select(p => p.ValorAtrasadoEmAberto).SingleOrDefault();
                }

                for (int i = 0; i < 12; i++)
                {
                    pagos[i] = qtdDebitoExibicaoViewModels.Where(p => p.DescricaoNumeral == i + 1).Select(p => p.ValorPagoDoPeriodo).SingleOrDefault();
                }
            }

            columnChart.SetSeries(new Series[]
                {
                    new Series{
                        Name = "PAGOS",
                       Data = new Data(pagos)
                    },

                     new Series()
                    {
                        Name = "EM ABERTO R$",
                        Data = new Data(emAbertoValor)
                    }
                }
            );

            return columnChart;

        }

        //GET: Cobrança de debitos em aberto
        public ActionResult Cobranca(string sortOrder, string currentFilter, string searchString, int? page, string searchAno)
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

            var debitos = new List<Debito>();
            using (RepositorioDebito _repositorioDebito = new RepositorioDebito(new CeagDbContext()))
            {
                debitos = _repositorioDebito.SelecionarDebitosAberto(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                debitos = debitos.Where(p => p.AlunoMatricula.Aluno.Nome.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(searchAno) && !searchAno.Equals("TODOS"))
            {
                debitos = debitos.Where(p => p.Periodo.Year == Convert.ToInt32(searchAno)).ToList();
            }

            #endregion


            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion
            // List<DebitoExibicaoViewModel> dto = new List<DebitoExibicaoViewModel>();
            List<AlunoDebitoExibicaoViewModel> alunoDebitoExibicaoViewModels = new List<AlunoDebitoExibicaoViewModel>();

            using (RepositorioTelefone _repoLocal = new RepositorioTelefone(new CeagDbContext()))
            {
                foreach (var item in debitos.Select(p => p.AlunoMatricula.CodAluno).Distinct())
                {
                    AlunoDebitoExibicaoViewModel alunoDebitoExibicaoViewModel = new AlunoDebitoExibicaoViewModel();
                    alunoDebitoExibicaoViewModel.AlunoExibicaoViewModel = mapper.Map<AlunoExibicaoViewModel>(debitos.Select(p => p.AlunoMatricula.Aluno).Where(p => p.CodAluno == item).Take(1).SingleOrDefault());
                    alunoDebitoExibicaoViewModel.DebitoExibicaoViewModels = mapper.Map<List<DebitoExibicaoViewModel>>(debitos.Where(p => p.AlunoMatricula.CodAluno == item).ToList());
                    alunoDebitoExibicaoViewModel.DebitosEmAberto = debitos.Where(p => p.AlunoMatricula.CodAluno == item).Count();
                    alunoDebitoExibicaoViewModel.DebitosEmAtraso = debitos.Where(p => p.AlunoMatricula.CodAluno == item && p.Vencimento <= DateTime.Now).Count();
                    alunoDebitoExibicaoViewModel.TotalDebitosEmAberto = debitos.Where(p => p.AlunoMatricula.CodAluno == item).Sum(p => p.ValorDebito).ToString("N2");
                    alunoDebitoExibicaoViewModel.TotalDebitosEmAtraso = debitos.Where(p => p.AlunoMatricula.CodAluno == item && p.Vencimento <= DateTime.Now).Sum(p => p.ValorDebito).ToString("N2");
                    List<Responsavel> responsavels = new List<Responsavel>();
                    foreach (var itemAluno in debitos.Where(s => s.AlunoMatricula.CodAluno == item).Select(p => p.AlunoMatricula.Aluno).Distinct())
                    {
                        foreach (var itemResponsavel in itemAluno.Responsavels)
                        {
                            itemResponsavel.Telefones = _repoLocal.SelecionarTelefoneResponsavel(itemResponsavel.CodResponsavel);
                            responsavels.Add(itemResponsavel);
                        }
                    }
                    alunoDebitoExibicaoViewModel.ResponsavelExibicaoViewModels = mapper.Map<List<ResponsavelExibicaoViewModel>>(responsavels);
                    alunoDebitoExibicaoViewModels.Add(alunoDebitoExibicaoViewModel);
                }
            }
            CarregarComboTela();
            return View(alunoDebitoExibicaoViewModels.ToPagedList(pageNumber, pageSize));
        }

        //GET: Cobrança de debitos em aberto
        public ActionResult CobrancaTurma(string sortOrder, string currentFilter, string searchString, int? page, int? codTurma)
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

            var debitos = new List<Debito>();
            using (RepositorioDebito _repositorioDebito = new RepositorioDebito(new CeagDbContext()))
            {
                debitos = _repositorioDebito.SelecionarDebitosAberto(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                debitos = debitos.Where(p => p.AlunoMatricula.Aluno.Nome.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            if (codTurma > 0)
            {
                debitos = debitos.Where(p => p.AlunoMatricula.CodTurma == codTurma).ToList();
            }

            #endregion


            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion
            // List<DebitoExibicaoViewModel> dto = new List<DebitoExibicaoViewModel>();
            List<AlunoDebitoExibicaoViewModel> alunoDebitoExibicaoViewModels = new List<AlunoDebitoExibicaoViewModel>();

            using (RepositorioTelefone _repoLocal = new RepositorioTelefone(new CeagDbContext()))
            {
                foreach (var item in debitos.Select(p => p.AlunoMatricula.CodAluno).Distinct())
                {
                    AlunoDebitoExibicaoViewModel alunoDebitoExibicaoViewModel = new AlunoDebitoExibicaoViewModel();
                    alunoDebitoExibicaoViewModel.AlunoExibicaoViewModel = mapper.Map<AlunoExibicaoViewModel>(debitos.Select(p => p.AlunoMatricula.Aluno).Where(p => p.CodAluno == item).Take(1).SingleOrDefault());
                    alunoDebitoExibicaoViewModel.DebitoExibicaoViewModels = mapper.Map<List<DebitoExibicaoViewModel>>(debitos.Where(p => p.AlunoMatricula.CodAluno == item).ToList());
                    alunoDebitoExibicaoViewModel.DebitosEmAberto = debitos.Where(p => p.AlunoMatricula.CodAluno == item).Count();
                    alunoDebitoExibicaoViewModel.DebitosEmAtraso = debitos.Where(p => p.AlunoMatricula.CodAluno == item && p.Vencimento <= DateTime.Now).Count();
                    alunoDebitoExibicaoViewModel.TotalDebitosEmAberto = debitos.Where(p => p.AlunoMatricula.CodAluno == item).Sum(p => p.ValorDebito).ToString("N2");
                    alunoDebitoExibicaoViewModel.TotalDebitosEmAtraso = debitos.Where(p => p.AlunoMatricula.CodAluno == item && p.Vencimento <= DateTime.Now).Sum(p => p.ValorDebito).ToString("N2");
                    List<Responsavel> responsavels = new List<Responsavel>();
                    foreach (var itemAluno in debitos.Where(s => s.AlunoMatricula.CodAluno == item).Select(p => p.AlunoMatricula.Aluno).Distinct())
                    {
                        foreach (var itemResponsavel in itemAluno.Responsavels)
                        {
                            itemResponsavel.Telefones = _repoLocal.SelecionarTelefoneResponsavel(itemResponsavel.CodResponsavel);
                            responsavels.Add(itemResponsavel);
                        }
                    }
                    alunoDebitoExibicaoViewModel.ResponsavelExibicaoViewModels = mapper.Map<List<ResponsavelExibicaoViewModel>>(responsavels);
                    alunoDebitoExibicaoViewModels.Add(alunoDebitoExibicaoViewModel);
                }
            }
            CarregarComboTela();
            return View(alunoDebitoExibicaoViewModels.ToPagedList(pageNumber, pageSize));
        }

        //// GET: Debitos/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Debito debito = db.Debitos.Find(id);
        //    if (debito == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(debito);
        //}

        //// POST: Debitos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Debito debito = db.Debitos.Find(id);
        //    db.Debitos.Remove(debito);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Métodos de apoio

        private void CarregarComboTela()
        {
            ViewBag.DropdownTipoDebito = Utils.CarregarTipoDebito();
            ViewBag.DropdownFormaPagamento = Utils.CarregarFormasDePagamento();
            ViewBag.DropdownQuantidadeParcelas = Utils.CarregarQuantidadeParcelasPadrao();
            ViewBag.DropDownAno = Utils.CarregarAno();
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

        private void InserirRegistroHistoricoDebito(int codDebito, string descricao, string observacao)
        {
            using (RepositorioDebitoHistorico _repositorioDebitoHistorico = new RepositorioDebitoHistorico(new CeagDbContext()))
            {
                DebitoHistorico debitoHistorico = new DebitoHistorico()
                {
                    CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso,
                    CodDebito = codDebito,
                    Descricao = descricao,
                    Observacao = observacao,
                    Inclusao = DateTime.Now
                };

                _repositorioDebitoHistorico.Inserir(debitoHistorico);
            }

        }

        #endregion
    }
}
