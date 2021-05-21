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
using CEAG.NEGOCIO.Classes;
using CEAG.NEGOCIO.Enum;
using CEAG.NEGOCIO.Utils;
using CEAG.REPOSITORIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Acesso;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.MensalidadeValores;
using CEAG.WEB.ViewModel.Turma;
using PagedList;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class AlunoMatriculasController : Controller
    {
        #region Métodos padrão scallfolding

        #region Index e Detalhes


        // GET: AlunoMatriculas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, string searchAno, int? page, string erro)
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

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            #region Método de consulta


            var alunoMatriculas = new List<AlunoMatricula>();
            using (RepositorioAlunoMatricula _repositorioGenericoAlunoMatriculaLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
            {

                if (!string.IsNullOrEmpty(searchString))
                {
                    alunoMatriculas = _repositorioGenericoAlunoMatriculaLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                    .Where(p =>
                                    p.Turma.Descricao.ToUpper().Contains(searchString.ToUpper())
                                    || p.Aluno.Nome.ToUpper().Contains(searchString.ToUpper())
                                    || (p.Aluno.Cpf != null && p.Aluno.Cpf.ToUpper().Contains(searchString.ToUpper()))
                        ).ToList();
                }
                else
                {
                    alunoMatriculas = _repositorioGenericoAlunoMatriculaLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
                }

            }
            if (!string.IsNullOrEmpty(searchAno) && !searchAno.Equals("TODOS"))
            {
                alunoMatriculas = alunoMatriculas.Where(p => p.Turma.AnoLetivo == Convert.ToInt32(searchAno)).ToList();
            }

            #endregion

            List<AlunoMatriculaExibicaoViewModel> dto = mapper.Map<List<AlunoMatriculaExibicaoViewModel>>(alunoMatriculas);

            ViewBag.MessageError = erro;
            ViewBag.QtdMat = dto.Count();
            CarregarComboTela();
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            return View(dto.ToPagedList(pageNumber, pageSize));
        }

        #endregion

        #region Create e Edit
        // GET: AlunoMatriculas/Create
        public ActionResult Create(int codAluno, int codTurma)
        {
            Aluno aluno = new Aluno();
            using (RepositorioAluno _repositorioGenericoAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                aluno = _repositorioGenericoAlunoLocal.SelecionarPorId(codAluno);
            }

            Turma turma = new Turma();
            using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repositorioGenericoTurmaLocal.SelecionarPorId(codTurma);
            }


            MensalidadeValor mensalidadeValor = new MensalidadeValor();
            using (RepositorioMensalidadeValor _repositorioGenericoMensalidadeValoresLocal = new RepositorioMensalidadeValor(new CeagDbContext()))
            {
                mensalidadeValor = _repositorioGenericoMensalidadeValoresLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                    .Where(p => p.Turma.ToUpper().Equals(turma.Segmento.ToUpper()))
                    .SingleOrDefault();
            }

            if (turma.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            if (aluno.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Turma não existe nesta escola.");
            }

            if (AlunoJaTemMatricula(codAluno, codTurma).CodTurma > 0)
            {
                return RedirectToAction("Index", "AlunoMatriculas", new { erro = "Impossível matricular aluno." });
            }

            if (!aluno.Responsavels.Any())
            {
                return RedirectToAction("Index", "AlunoMatriculas", new { erro = "Impossivel matricular aluno sem cadastro de um responsável pelo Aluno." });
            }


            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            AlunoExibicaoViewModel dtoAluno = mapper.Map<AlunoExibicaoViewModel>(aluno);
            TurmaExibicaoViewModel dtoTurma = mapper.Map<TurmaExibicaoViewModel>(turma);
            MensalidadeValorViewModel dtoMensalidadeValor = mapper.Map<MensalidadeValorViewModel>(mensalidadeValor);

            ViewBag.ValorMensal = dtoMensalidadeValor.ValorMensal.ToString("N2");
            ViewBag.MensalidadeValor = dtoMensalidadeValor;
            ViewBag.Turma = dtoTurma;
            ViewBag.Aluno = dtoAluno;

            CarregarComboTela();
            return View();
        }

        // POST: AlunoMatriculas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlunoMatriculaViewModel alunoMatriculaViewModel)
        {
            //Incluindo a data de matricula do aluno
            alunoMatriculaViewModel.Inclusao = DateTime.Now;

            //Verificando se o modelo de dados está valido, com campos preenchidos corretamente.
            if (ModelState.IsValid)
            {
                #region Mapper ## Mapeando os objetos de modelo para o dominio
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion

                //Resgatando o usuário logando.
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();


                #region Inserindo a Matricula

                //Consultando a turma para auxilio nos preenchimentos dos dados da matricula, periodo, ano letivo, etc
                var turmaCode = new Turma();
                using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
                {
                    turmaCode = _repositorioGenericoTurmaLocal.SelecionarPorId(alunoMatriculaViewModel.CodTurma);
                }

                //Consultando os valores de mensalidade padrões para apoio nos calculos
                MensalidadeValor mensalidadeValor = new MensalidadeValor();
                using (RepositorioMensalidadeValor _repositorioGenericoMensalidadeValoresLocal = new RepositorioMensalidadeValor(new CeagDbContext()))
                {
                    mensalidadeValor = _repositorioGenericoMensalidadeValoresLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).Where(p => p.Turma.ToUpper().Equals(turmaCode.Segmento.ToUpper())).SingleOrDefault();
                }

                //Verificando se foi marcada a opção de "BOLSISTA" em tela e comparando se o valor da matricula e mensalidade tem desconto
                if ("NÃO".Equals(alunoMatriculaViewModel.Bolsista))
                {
                    double valorMensalidadeCalculada = 0;
                    double valorMensalParaCalculo = mensalidadeValor.ValorAnual / 12;
                    valorMensalidadeCalculada = (mensalidadeValor.ValorAnual - valorMensalParaCalculo) / (alunoMatriculaViewModel.ParcelasMensalidade - 1);

                    alunoMatriculaViewModel.DescontoMensalidade = (alunoMatriculaViewModel.ValorMensalidade < valorMensalidadeCalculada) ? "SIM" : "NÃO";
                }

                //Mapeando para o tipo de dominio a ser inserido no banco de dados
                var alunoMatricula = mapper.Map<AlunoMatriculaViewModel, AlunoMatricula>(alunoMatriculaViewModel);
                alunoMatricula.CodAcesso = usuario.CodAcesso;
                using (NEGOCIO.Utils.Security _security = new NEGOCIO.Utils.Security())
                {
                    alunoMatricula.IdenficadorHexa = _security.GeradorIdentificador(20, "MAT", turmaCode.Nivel.Substring(1, 4), UsuarioEscola.ResgatarEscolaLogada());
                }

                //Inserindo a matricula do aluno no sistema
                using (RepositorioAlunoMatricula _repositorioAlunoMatriculaLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
                {
                    _repositorioAlunoMatriculaLocal.Inserir(alunoMatricula);
                }

                List<Responsavel> responsavelFinanceiro = new List<Responsavel>();
                using (RepositorioAluno _repositorioGenericoAlunoLocal = new RepositorioAluno(new CeagDbContext()))
                {
                    responsavelFinanceiro = _repositorioGenericoAlunoLocal.SelecionarPorId(alunoMatricula.CodAluno).Responsavels.Where(p => p.Tipo.Equals("FINANCEIRO") || p.Tipo.Equals("LEGAL")).ToList();
                }

                #endregion

                #region Region de Débitos

                List<Debito> debitos = new List<Debito>();

                #region Valor de Material

                if (alunoMatricula.ValorTaxaMaterial > 0)
                {
                    Debito debitoMaterial = new Debito();
                    debitoMaterial.CodAlunoMatricula = alunoMatricula.CodAlunoMatricula;
                    debitoMaterial.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;

                    debitoMaterial.ValorDebito = alunoMatricula.ValorTaxaMaterial;
                    debitoMaterial.Descricao = @"VALOR REFERENTE AO MATERIAL : 1/" + turmaCode.AnoLetivo.ToString();
                    debitoMaterial.Observacao = @"DÉBITO DE MATERIAL INCLUÍDO AUTOMATICAMENTE NO SISTEMA. [" + UsuarioEscola.ResgatarUsuarioLogado().Nome + " '" + DateTime.Now.ToShortDateString() + "'] ";

                    debitoMaterial.Vencimento = new DateTime(turmaCode.AnoLetivo, 1, DateTime.DaysInMonth(turmaCode.AnoLetivo, 1));
                    debitoMaterial.Periodo = new DateTime(turmaCode.AnoLetivo, 1, 1);
                    debitoMaterial.TipoDebito = EnumComum.TipoDebito.MATERIAL.ToString();
                    debitoMaterial.Inclusao = DateTime.Now;
                    debitoMaterial.FormaPagamento = alunoMatriculaViewModel.FormaPagamento;
                    if (responsavelFinanceiro.Where(p => p.Tipo.Equals("FINANCEIRO")).Any())
                    {
                        debitoMaterial.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("FINANCEIRO")).Nome;
                    }
                    else
                    {
                        debitoMaterial.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("LEGAL")).Nome;
                    }

                    debitos.Add(debitoMaterial);
                }

                #endregion

                //Verificando se o Aluno é bolsista e finalizando o metodo de inserir.
                if ("SIM".Equals(alunoMatricula.Bolsista))
                {
                    return RedirectToAction("Index", "AlunoMatriculas");
                }

                if (alunoMatriculaViewModel.ParcelasMensalidade > 1)
                {

                    #region Valor de Matrícula

                    Debito debitoMatricula = new Debito();
                    debitoMatricula.CodAlunoMatricula = alunoMatricula.CodAlunoMatricula;
                    debitoMatricula.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                    debitoMatricula.Descricao = @"VALOR REFERENTE À MATRICULA : 1/" + turmaCode.AnoLetivo.ToString();
                    debitoMatricula.ValorDebito = alunoMatricula.ValorMatricula;
                    debitoMatricula.Vencimento = new DateTime(turmaCode.AnoLetivo, 1, DateTime.DaysInMonth(turmaCode.AnoLetivo, 1));
                    debitoMatricula.Periodo = new DateTime(turmaCode.AnoLetivo, 1, 1);
                    debitoMatricula.TipoDebito = EnumComum.TipoDebito.MATRICULA.ToString();
                    debitoMatricula.Inclusao = DateTime.Now;
                    debitoMatricula.Observacao = "DÉBITO DE MATRICULA INCLUÍDO AUTOMATICAMENTE NO SISTEMA. [" + UsuarioEscola.ResgatarUsuarioLogado().Nome + " '" + DateTime.Now.ToShortDateString() + "'] ";
                    debitoMatricula.FormaPagamento = alunoMatriculaViewModel.FormaPagamento;
                    if (responsavelFinanceiro.Where(p => p.Tipo.Equals("FINANCEIRO")).Any())
                    {
                        debitoMatricula.Responsavel = responsavelFinanceiro.First(p => p.Tipo.Equals("FINANCEIRO")).Nome;
                    }
                    else
                    {
                        debitoMatricula.Responsavel = responsavelFinanceiro.First(p => p.Tipo.Equals("LEGAL")).Nome;
                    }

                    debitos.Add(debitoMatricula);
                    #endregion

                    #region Geração de Mensalidades

                    for (int i = 2; i <= alunoMatricula.ParcelasMensalidade; i++)
                    {
                        Debito debitoMensalidade = new Debito();
                        debitoMensalidade.CodAlunoMatricula = alunoMatricula.CodAlunoMatricula;
                        debitoMensalidade.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                        debitoMensalidade.Descricao = "VALOR REFERENTE À MENSALIDADE NÚMERO " + (i - 1).ToString() + @"/" + turmaCode.AnoLetivo.ToString();
                        debitoMensalidade.ValorDebito = alunoMatricula.ValorMensalidade;

                        debitoMensalidade.Vencimento = new DateTime(turmaCode.AnoLetivo, i, DateTime.DaysInMonth(turmaCode.AnoLetivo, i));
                        debitoMensalidade.Periodo = new DateTime(turmaCode.AnoLetivo, i, 1);
                        debitoMensalidade.TipoDebito = EnumComum.TipoDebito.MENSALIDADE.ToString();
                        debitoMensalidade.Inclusao = DateTime.Now;
                        debitoMensalidade.Observacao = "DÉBITO DE MENSALIDADE INCLUÍDO AUTOMATICAMENTE NO SISTEMA. [" + UsuarioEscola.ResgatarUsuarioLogado().Nome + " '" + DateTime.Now.ToShortDateString() + "'] ";
                        debitoMensalidade.FormaPagamento = alunoMatriculaViewModel.FormaPagamento;
                        if (responsavelFinanceiro.Where(p => p.Tipo.Equals("FINANCEIRO")).Any())
                        {
                            debitoMensalidade.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("FINANCEIRO")).Nome;
                        }
                        else
                        {
                            debitoMensalidade.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("LEGAL")).Nome;
                        }

                        debitos.Add(debitoMensalidade);
                    }

                    InserirMensalidades(debitos);

                    #endregion
                }
                else
                {
                    #region Geração de Mensalidades

                    int quantidadeParcelas = (alunoMatricula.ParcelasMensalidade == 0 ? 1 : alunoMatricula.ParcelasMensalidade);
                    for (int i = 1; i <= quantidadeParcelas; i++)
                    {
                        Debito debitoMensalidade = new Debito();
                        debitoMensalidade.CodAlunoMatricula = alunoMatricula.CodAlunoMatricula;
                        debitoMensalidade.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                        debitoMensalidade.Descricao = "VALOR REFERENTE À PAGAMENTO TOTAL À VISTA " + quantidadeParcelas.ToString() + @"/" + turmaCode.AnoLetivo.ToString();
                        debitoMensalidade.ValorDebito = alunoMatricula.ValorMensalidade;//alunoMatriculaViewModel.ParcelasMensalidade != 0 ? mensalidadeValor.ValorAnualVista / alunoMatriculaViewModel.ParcelasMensalidade : mensalidadeValor.ValorAnual; ;
                        debitoMensalidade.Vencimento = new DateTime(turmaCode.AnoLetivo, quantidadeParcelas, DateTime.DaysInMonth(turmaCode.AnoLetivo, (quantidadeParcelas)));
                        debitoMensalidade.Periodo = new DateTime(turmaCode.AnoLetivo, quantidadeParcelas, 1);
                        debitoMensalidade.TipoDebito = EnumComum.TipoDebito.MENSALIDADE.ToString();
                        debitoMensalidade.Inclusao = DateTime.Now;
                        debitoMensalidade.Observacao = "DÉBITO DE PAGAMENTO DE ANUIDADE INCLUÍDO AUTOMATICAMENTE NO SISTEMA. [" + UsuarioEscola.ResgatarUsuarioLogado().Nome + " '" + DateTime.Now.ToShortDateString() + "'] ";
                        debitoMensalidade.FormaPagamento = alunoMatriculaViewModel.FormaPagamento;
                        if (responsavelFinanceiro.Where(p => p.Tipo.Equals("FINANCEIRO")).Any())
                        {
                            debitoMensalidade.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("FINANCEIRO")).Nome;
                        }
                        else
                        {
                            debitoMensalidade.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("LEGAL")).Nome;
                        }

                        debitos.Add(debitoMensalidade);
                    }

                    InserirMensalidades(debitos);

                    #endregion
                }

                #endregion

                return RedirectToAction("Index", "AlunoMatriculas");

            }


            #region Mapper
            Mapper mapper1 = Constants.Utils.DominioParaViewModel();
            #endregion

            var aluno = new Aluno();
            using (RepositorioAluno _repositorioGenericoAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                aluno = _repositorioGenericoAlunoLocal.SelecionarPorId(alunoMatriculaViewModel.CodAluno);
            }
            var turma = new Turma();
            using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repositorioGenericoTurmaLocal.SelecionarPorId(alunoMatriculaViewModel.CodTurma);
            }

            MensalidadeValor mensalidadeValor1 = new MensalidadeValor();
            using (RepositorioMensalidadeValor _repositorioGenericoMensalidadeValoresLocal = new RepositorioMensalidadeValor(new CeagDbContext()))
            {
                mensalidadeValor1 = _repositorioGenericoMensalidadeValoresLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                    .Where(p => p.Turma.ToUpper().Equals(turma.Segmento.ToUpper()))
                    .SingleOrDefault();
            }


            AlunoExibicaoViewModel dtoAluno = mapper1.Map<AlunoExibicaoViewModel>(aluno);
            TurmaExibicaoViewModel dtoTurma = mapper1.Map<TurmaExibicaoViewModel>(turma);
            MensalidadeValorViewModel dtoMensalidadeValor1 = mapper1.Map<MensalidadeValorViewModel>(mensalidadeValor1);

            ViewBag.ValorMensal = dtoMensalidadeValor1.ValorMensal.ToString("N2");
            ViewBag.MensalidadeValor = dtoMensalidadeValor1;
            ViewBag.Turma = dtoTurma;
            ViewBag.Aluno = dtoAluno;


            CarregarComboTela();
            // ViewBag.CodEscola = new SelectList(db.Escolas, "CodEscola", "Razao", aluno.CodEscola);
            return View(alunoMatriculaViewModel);
        }

        // GET: AlunoMatriculas/Edit/5
        public ActionResult Edit(int? codAlunoMatricula, int? codNovaTurma)
        {
            if (codAlunoMatricula == null)
            {
                return CarregarMensagemDeErro("Matricula Inexistente.");
            }

            var alunoMatricula = new AlunoMatricula();
            using (RepositorioAlunoMatricula _repositorioGenericoAlunoMatriculaLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
            {
                alunoMatricula = _repositorioGenericoAlunoMatriculaLocal.SelecionarPorId(codAlunoMatricula.Value);
            }

            if (alunoMatricula == null)
            {
                return CarregarMensagemDeErro("Matricula Inexistente.");
            }


            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            var turma = new Turma();

            Aluno alunoConsulta = new Aluno();
            using (RepositorioAluno _repositorioGenericoAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                alunoConsulta = _repositorioGenericoAlunoLocal.SelecionarPorId(alunoMatricula.CodAluno);
            }

            if (alunoConsulta.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                return CarregarMensagemDeErro("Aluno não existe nesta escola.");
            }


            if (codNovaTurma.HasValue)
            {
                var turmaNova = new Turma();

                using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
                {
                    turmaNova = _repositorioGenericoTurmaLocal.SelecionarPorId(codNovaTurma.Value);
                }


                if (turmaNova.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                {
                    return CarregarMensagemDeErro("Aluno não existe nesta escola.");
                }

                turma = turmaNova;
            }
            else
            {
                using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
                {
                    turma = _repositorioGenericoTurmaLocal.SelecionarPorId(alunoMatricula.CodTurma);
                }

            }
            MensalidadeValor mensalidadeValor = new MensalidadeValor();
            using (RepositorioMensalidadeValor _repositorioGenericoMensalidadeValoresLocal = new RepositorioMensalidadeValor(new CeagDbContext()))
            {
                mensalidadeValor = _repositorioGenericoMensalidadeValoresLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                    .Where(p => p.Turma.ToUpper().Equals(turma.Segmento.ToUpper())).SingleOrDefault();
            }

            AlunoExibicaoViewModel dtoAluno = mapper.Map<AlunoExibicaoViewModel>(alunoConsulta);
            TurmaExibicaoViewModel dtoTurma = mapper.Map<TurmaExibicaoViewModel>(turma);
            AlunoMatriculaViewModel dtoAlunoMatricula = mapper.Map<AlunoMatriculaViewModel>(alunoMatricula);
            MensalidadeValorViewModel dtoMensalidadeValor = mapper.Map<MensalidadeValorViewModel>(mensalidadeValor);

            CarregarComboTela();
            ViewBag.CodAlunoMatricula = dtoAlunoMatricula.CodAlunoMatricula;
            ViewBag.ValorMensal = dtoMensalidadeValor.ValorMensal.ToString("N2");
            ViewBag.MensalidadeValor = dtoMensalidadeValor;

            ViewBag.Turma = dtoTurma;
            ViewBag.Aluno = dtoAluno;

            return View(dtoAlunoMatricula);
        }

        // POST: AlunoMatriculas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlunoMatriculaViewModel alunoMatriculaViewModel)
        {
            if (ModelState.IsValid)
            {
                bool temBoletoPago = true;
                #region Mapper
                Mapper mapper = Constants.Utils.ViewModelParaDominio();
                #endregion

                if (alunoMatriculaViewModel.CodNovaTurma > 0)
                {
                    alunoMatriculaViewModel.CodTurma = alunoMatriculaViewModel.CodNovaTurma;
                }

                #region Alterando a Turma

                var alunoMatricula = mapper.Map<AlunoMatriculaViewModel, AlunoMatricula>(alunoMatriculaViewModel);
                var usuario = UsuarioEscola.ResgatarUsuarioLogado();
                alunoMatricula.CodAcesso = usuario.CodAcesso;
                using (RepositorioAlunoMatricula _repositorioGenericoAlunoMatriculaLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
                {
                    _repositorioGenericoAlunoMatriculaLocal.Alterar(alunoMatricula);
                }
                #endregion
                var turmaCode = new Turma();
                using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
                {
                    turmaCode = _repositorioGenericoTurmaLocal.SelecionarPorId(alunoMatriculaViewModel.CodTurma);
                }
                #region Region de Débitos

                #region Esclusão de todos os débitos para regerear
                List<Debito> debitosParaExcluir = new List<Debito>();

                using (RepositorioDebito _repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
                {
                    debitosParaExcluir = _repositorioDebitoLocal.SelecionarPorAlunoMatricula(alunoMatricula.CodAlunoMatricula);
                    if (!debitosParaExcluir.Where(p => p.Pagamento.HasValue).Any())
                    {
                        temBoletoPago = false;
                        foreach (var item in debitosParaExcluir)
                        {
                            _repositorioDebitoLocal.Excluir(item);
                        }
                    }
                }

                #endregion
                if ("SIM".Equals(alunoMatricula.Bolsista))
                {
                    return RedirectToAction("Index", "AlunoMatriculas");
                }

                MensalidadeValor mensalidadeValor = new MensalidadeValor();
                using (RepositorioMensalidadeValor _repositorioGenericoMensalidadeValoresLocal = new RepositorioMensalidadeValor(new CeagDbContext()))
                {
                    mensalidadeValor = _repositorioGenericoMensalidadeValoresLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).Where(p => p.Turma.Equals(turmaCode.Segmento)).SingleOrDefault();
                }

                List<Responsavel> responsavelFinanceiro = new List<Responsavel>();
                using (RepositorioAluno _repositorioGenericoAlunoLocal = new RepositorioAluno(new CeagDbContext()))
                {
                    responsavelFinanceiro = _repositorioGenericoAlunoLocal.SelecionarPorId(alunoMatricula.CodAluno).Responsavels.Where(p => p.Tipo.Equals("FINANCEIRO") || p.Tipo.Equals("LEGAL")).ToList();
                }


                List<Debito> debitos = new List<Debito>();
                #region Valor de Material

                if (alunoMatricula.ValorTaxaMaterial > 0 && !temBoletoPago)
                {
                    Debito debitoMaterial = new Debito();
                    debitoMaterial.CodAlunoMatricula = alunoMatricula.CodAlunoMatricula;
                    debitoMaterial.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;

                    debitoMaterial.ValorDebito = alunoMatricula.ValorTaxaMaterial;
                    debitoMaterial.Descricao = @"VALOR REFERENTE AO MATERIAL : 1/" + turmaCode.AnoLetivo.ToString();
                    debitoMaterial.Observacao = @"DÉBITO DE MATERIAL INCLUÍDO AUTOMATICAMENTE NO SISTEMA. [" + UsuarioEscola.ResgatarUsuarioLogado().Nome + " '" + DateTime.Now.ToShortDateString() + "'] ";

                    debitoMaterial.Vencimento = new DateTime(turmaCode.AnoLetivo, 1, DateTime.DaysInMonth(turmaCode.AnoLetivo, 1));
                    debitoMaterial.Periodo = new DateTime(turmaCode.AnoLetivo, 1, 1);
                    debitoMaterial.TipoDebito = EnumComum.TipoDebito.MATERIAL.ToString();
                    debitoMaterial.Inclusao = DateTime.Now;
                    debitoMaterial.FormaPagamento = alunoMatriculaViewModel.FormaPagamento;
                    if (responsavelFinanceiro.Where(p => p.Tipo.Equals("FINANCEIRO")).Any())
                    {
                        debitoMaterial.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("FINANCEIRO")).Nome;
                    }
                    else
                    {
                        debitoMaterial.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("LEGAL")).Nome;
                    }

                    debitos.Add(debitoMaterial);
                }

                #endregion

                double valorMensalidade = 0;
                if (!temBoletoPago)
                {
                    if (alunoMatriculaViewModel.ParcelasMensalidade > 1)
                    {
                        //double valorMensal = mensalidadeValor.ValorAnual / 12;
                        //valorMensalidade = (mensalidadeValor.ValorAnual - valorMensal) / (alunoMatriculaViewModel.ParcelasMensalidade - 1);

                        #region Valor de Matrícula

                        Debito debitoMatricula = new Debito();
                        debitoMatricula.CodAlunoMatricula = alunoMatricula.CodAlunoMatricula;
                        debitoMatricula.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                        debitoMatricula.Descricao = @"Valor referente à matricula : 1/" + turmaCode.AnoLetivo.ToString();

                        //TODO: CONSULTAR SE MATRICULA FOI PAGA PARA PODER MODIFICAR OU NÃO
                        debitoMatricula.ValorDebito = alunoMatriculaViewModel.ValorMatricula;

                        debitoMatricula.Vencimento = new DateTime(turmaCode.AnoLetivo, 1, DateTime.DaysInMonth(turmaCode.AnoLetivo, 1));
                        debitoMatricula.Periodo = new DateTime(turmaCode.AnoLetivo, 1, 1);
                        debitoMatricula.TipoDebito = "MATRICULA";
                        debitoMatricula.Inclusao = DateTime.Now;
                        debitoMatricula.Observacao = "Débito de matricula incluído automaticamente no sistema. [" + UsuarioEscola.ResgatarUsuarioLogado().Nome + " '" + DateTime.Now.ToShortDateString() + "'] ";
                        debitoMatricula.FormaPagamento = alunoMatriculaViewModel.FormaPagamento;
                        if (responsavelFinanceiro.Where(p => p.Tipo.Equals("FINANCEIRO")).Any())
                        {
                            debitoMatricula.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("FINANCEIRO")).Nome;
                        }
                        else
                        {
                            debitoMatricula.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("LEGAL")).Nome;
                        }

                        debitos.Add(debitoMatricula);
                        #endregion

                        #region Geração de Mensalidades

                        for (int i = 2; i <= alunoMatricula.ParcelasMensalidade; i++)
                        {
                            Debito debitoMensalidade = new Debito();
                            debitoMensalidade.CodAlunoMatricula = alunoMatricula.CodAlunoMatricula;
                            debitoMensalidade.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                            debitoMensalidade.Descricao = "valor referente à mensalidade número " + i.ToString() + @"/" + turmaCode.AnoLetivo.ToString();

                            //TODO: Verificar se já tem alguma mensalidade paga, para proibir a mudança
                            debitoMensalidade.ValorDebito = alunoMatricula.ValorMensalidade;

                            debitoMensalidade.Vencimento = new DateTime(turmaCode.AnoLetivo, i, DateTime.DaysInMonth(turmaCode.AnoLetivo, i));
                            debitoMensalidade.Periodo = new DateTime(turmaCode.AnoLetivo, i, 1);
                            debitoMensalidade.TipoDebito = "MENSALIDADE";
                            debitoMensalidade.Inclusao = DateTime.Now;
                            debitoMensalidade.Observacao = "Débito de mensalidade incluído automaticamente no sistema. [" + UsuarioEscola.ResgatarUsuarioLogado().Nome + " '" + DateTime.Now.ToShortDateString() + "'] ";
                            debitoMensalidade.FormaPagamento = alunoMatriculaViewModel.FormaPagamento;
                            if (responsavelFinanceiro.Where(p => p.Tipo.Equals("FINANCEIRO")).Any())
                            {
                                debitoMensalidade.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("FINANCEIRO")).Nome;
                            }
                            else
                            {
                                debitoMensalidade.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("LEGAL")).Nome;
                            }

                            debitos.Add(debitoMensalidade);
                        }

                        InserirMensalidades(debitos);

                        #endregion
                    }
                    else
                    {
                        valorMensalidade = alunoMatriculaViewModel.ParcelasMensalidade != 0 ? mensalidadeValor.ValorAnualVista / alunoMatriculaViewModel.ParcelasMensalidade : mensalidadeValor.ValorAnual;

                        #region Geração de Mensalidades

                        int quantidadeParcelas = (alunoMatricula.ParcelasMensalidade == 0 ? 1 : alunoMatricula.ParcelasMensalidade);
                        for (int i = 1; i <= quantidadeParcelas; i++)
                        {
                            Debito debitoMensalidade = new Debito();
                            debitoMensalidade.CodAlunoMatricula = alunoMatricula.CodAlunoMatricula;
                            debitoMensalidade.CodAcesso = UsuarioEscola.ResgatarUsuarioLogado().CodAcesso;
                            debitoMensalidade.Descricao = "valor referente à pagamento total à vista " + quantidadeParcelas.ToString() + @"/" + turmaCode.AnoLetivo.ToString();
                            //TODO: Verificar se já tem alguma mensalidade paga, para proibir a mudança
                            debitoMensalidade.ValorDebito = alunoMatricula.ValorMensalidade;
                            debitoMensalidade.Vencimento = new DateTime(turmaCode.AnoLetivo, quantidadeParcelas, DateTime.DaysInMonth(turmaCode.AnoLetivo, (quantidadeParcelas)));
                            debitoMensalidade.Periodo = new DateTime(turmaCode.AnoLetivo, quantidadeParcelas, 1);
                            debitoMensalidade.TipoDebito = "MENSALIDADE";
                            debitoMensalidade.Inclusao = DateTime.Now;
                            debitoMensalidade.Observacao = "Débito de pagamento de anuidade incluído automaticamente no sistema. [" + UsuarioEscola.ResgatarUsuarioLogado().Nome + " '" + DateTime.Now.ToShortDateString() + "'] ";
                            debitoMensalidade.FormaPagamento = alunoMatriculaViewModel.FormaPagamento;
                            if (responsavelFinanceiro.Where(p => p.Tipo.Equals("FINANCEIRO")).Any())
                            {
                                debitoMensalidade.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("FINANCEIRO")).Nome;
                            }
                            else
                            {
                                debitoMensalidade.Responsavel = responsavelFinanceiro.SingleOrDefault(p => p.Tipo.Equals("LEGAL")).Nome;
                            }

                            debitos.Add(debitoMensalidade);
                        }

                        InserirMensalidades(debitos);

                        #endregion
                    }
                }
                #endregion

                return RedirectToAction("Index", "AlunoMatriculas");
            }
            return View(alunoMatriculaViewModel);
        }



        #endregion

        #endregion

        #region Métodos fora do padrão do scallfolding

        public ActionResult MatricularTurma(int? codAluno, string sortOrder, string currentFilter, string searchAno, string searchString, int? page)
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

            #region Método de consulta

            var turmas = new List<Turma>();
            using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    turmas = _repositorioGenericoTurmaLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                            .Where(p =>
                                        p.Descricao.ToUpper().Contains(searchString.ToUpper())
                                        || p.Nivel.ToUpper().Contains(searchString.ToUpper())
                                        || p.Segmento.ToUpper().Contains(searchString.ToUpper())

                            ).ToList();
                }
                else
                {
                    turmas = _repositorioGenericoTurmaLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
                }
            }

            if (!string.IsNullOrEmpty(searchAno) && !searchAno.Equals("TODOS"))
            {
                turmas = turmas.Where(p => p.AnoLetivo == Convert.ToInt32(searchAno)).ToList();
            }

            #endregion

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            List<TurmaExibicaoViewModel> dto = mapper.Map<List<TurmaExibicaoViewModel>>(turmas);
            CarregarComboTela();
            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.CodAluno = codAluno;
            return View(dto.ToPagedList(pageNumber, pageSize));

        }

        // GET: Matricular
        public ActionResult Matricular(int? codTurma, int? codAluno)
        {
            //Caso já exista o código da turma, se entende que falta selecionar o aluno
            if (codTurma.HasValue)
            {
                return RedirectToAction("MatricularAluno", "AlunoMatriculas", new { codturma = codTurma.Value });
            }

            return RedirectToAction("MatricularTurma", "AlunoMatriculas", new { codAluno });

        }

        public ActionResult MatricularAluno(int codturma, string sortOrder, string currentFilter, string searchString, int? page)
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

            Turma turma = new Turma();
            using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repositorioGenericoTurmaLocal.SelecionarPorId(codturma);
            }

            if (turma.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
            {
                Erro erroParametro = new Erro
                {
                    CodErro = 404,
                    statusCode = HttpStatusCode.BadRequest,
                    MensagemErro = "Turma não existe nesta escola.",
                    UrlChamada = ""//System.Web.HttpContext.Current.Request.UrlReferrer.ToString()
                };
                return RedirectToAction("ErroFinal", "Home", new { erro = erroParametro });
            }

            var alunos = new List<Aluno>();
            using (RepositorioAluno _repositorioGenericoAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    alunos = _repositorioGenericoAlunoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                            .Where(p =>
                                        p.Nome.ToUpper().Contains(searchString.ToUpper())
                                        || p.Cpf.ToUpper().Contains(searchString.ToUpper())
                                        || p.MaeNome.ToUpper().Contains(searchString.ToUpper())

                            ).ToList();
                }
                else
                {
                    alunos = _repositorioGenericoAlunoLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada());
                }
            }
            #endregion

            #region Mapper
            Mapper mapper = Constants.Utils.DominioParaViewModel();
            #endregion

            List<AlunoExibicaoViewModel> dto = new List<AlunoExibicaoViewModel>();
            foreach (var item in alunos)
            {
                AlunoExibicaoViewModel alunoExibicaoViewModel = mapper.Map<AlunoExibicaoViewModel>(item);
                alunoExibicaoViewModel.Matriculado = AlunoJaTemMatricula(item.CodAluno, turma.CodTurma).CodTurma > 0;
                alunoExibicaoViewModel.TurmaExibicaoViewModel = mapper.Map<TurmaExibicaoViewModel>(AlunoJaTemMatricula(item.CodAluno, turma.CodTurma));
                dto.Add(mapper.Map<AlunoExibicaoViewModel>(alunoExibicaoViewModel));
            }

            TurmaExibicaoViewModel dtoTurma = mapper.Map<TurmaExibicaoViewModel>(turma);

            ViewBag.CodEscola = UsuarioEscola.ResgatarCodigoEscolaSelecionada();
            ViewBag.Turma = dtoTurma;
            return View(dto.ToPagedList(pageNumber, pageSize));

        }

        #endregion

        #region Métodos de apoio

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


        private void InserirMensalidades(List<Debito> mensalidades)
        {
            using (RepositorioDebito _repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                foreach (var item in mensalidades)
                {
                    _repositorioDebitoLocal.Inserir(item);
                }
            }
        }

        private void CarregarComboTela()
        {
            ViewBag.DropdownFormaPagamento = Utils.CarregarFormaPagamento();
            ViewBag.DropdownAnoLetivo = Utils.CarregarAno();
            ViewBag.DropdownQuantidadeParcelas = Utils.CarregarQuantidadeParcelasMatricula();
            ViewBag.DropdownSimNao = Utils.CarregarListaBolsista();
        }

        [HttpPost]
        public JsonResult DividirValorAnual(int qtdParcelas, int codTurma)
        {
            Turma turma = new Turma();
            using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repositorioGenericoTurmaLocal.SelecionarPorId(codTurma);
            }

            MensalidadeValor mensalidadeValor = new MensalidadeValor();
            using (RepositorioMensalidadeValor _repositorioGenericoMensalidadeValoresLocal = new RepositorioMensalidadeValor(new CeagDbContext()))
            {
                mensalidadeValor = _repositorioGenericoMensalidadeValoresLocal.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                    .Where(p => p.Turma.ToUpper().Equals(turma.Segmento.ToUpper()))
                    .SingleOrDefault();
            }


            double retorno = 0;
            if (qtdParcelas > 1)
            {
                double valorMensal = mensalidadeValor.ValorAnual / 12;
                retorno = (mensalidadeValor.ValorAnual - valorMensal) / (qtdParcelas - 1);
            }
            else
            {
                retorno = qtdParcelas != 0 ? mensalidadeValor.ValorAnualVista / qtdParcelas : mensalidadeValor.ValorAnual;
            }

            return Json(String.Format("{0:f}", retorno));
        }

        /// <summary>
        /// Método que valida se o aluno já tem matricula na mesma turma ou periodo letivo
        /// </summary>
        /// <param name="codAluno">Código do aluno para consulta</param>
        /// <param name="codTurma">Código da turma para consulta</param>
        /// <returns></returns>
        private Turma AlunoJaTemMatricula(int codAluno, int codTurma)
        {
            Turma retorno = new Turma(); ;
            var matriculasPorAluno = new List<AlunoMatricula>();
            using (RepositorioAlunoMatricula _repositorioGenericoAlunoMatriculaLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
            {
                matriculasPorAluno = _repositorioGenericoAlunoMatriculaLocal.SelecionarPorIdAluno(codAluno);
            }

            Turma turma = new Turma();
            using (RepositorioTurma _repositorioGenericoTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                turma = _repositorioGenericoTurmaLocal.SelecionarPorId(codTurma);
            }

            Aluno aluno = new Aluno();
            using (RepositorioAluno _repositorioGenericoAlunoLocal = new RepositorioAluno(new CeagDbContext()))
            {
                aluno = _repositorioGenericoAlunoLocal.SelecionarPorId(codAluno);
            }

            foreach (var item in matriculasPorAluno)
            {
                //Verificando se tem matricula na mesma turma
                if (item.CodTurma == codTurma)
                {
                    retorno = turma;
                }
                //Verificando se tem matricula em uma outra turma no mesmo periodo letivo
                if (item.Turma.AnoLetivo == turma.AnoLetivo)
                {
                    retorno = item.Turma;
                }
            }
            return retorno;
        }
        #endregion
    }
}
