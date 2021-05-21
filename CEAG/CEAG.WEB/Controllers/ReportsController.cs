using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.REPOSITORIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.WEB.Constants;
using PagedList;
using Rotativa;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa.Options;
using AutoMapper;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.ViewModel.Telefone;
using CEAG.WEB.ViewModel.Responsavel;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.Turma;
using CEAG.WEB.ViewModel.AlunoQuestionario;
using CEAG.WEB.ViewModel.Taxa;
using CEAG.DOMINIO.Procedure;
using System.Data;
using CEAG.WEB.Reports;

namespace CEAG.WEB.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,DIRETOR,COORDENADOR")]
    public class ReportsController : Controller
    {
        // GET: Reports
        private IRepositorioGenerico<Aluno, int> _repositorioGenericoAluno = new RepositorioAluno(new CeagDbContext());
        private RepositorioAlunoMatricula _repositorioAlunoMatricula = new RepositorioAlunoMatricula(new CeagDbContext());
        private RepositorioTurma _repositorioTurma = new RepositorioTurma(new CeagDbContext());
        private RepositorioTelefone _repositorioTelefone = new RepositorioTelefone(new CeagDbContext());
        private RepositorioAlunoQuestionario _repositorioQuestionario = new RepositorioAlunoQuestionario(new CeagDbContext());
        private IRepositorioGenerico<MensalidadeValor, int> _repositorioMensalidadeValor = new RepositorioMensalidadeValor(new CeagDbContext());
        private IRepositorioGenerico<Responsavel, int> _repositorioGenericoResponsavel = new RepositorioResponsavel(new CeagDbContext());
        private IRepositorioGenerico<Taxa, int> _repositorioGenericoTaxa = new RepositorioTaxa(new CeagDbContext());

        public ActionResult ListagemAlunos(int? pagina, Boolean? gerarPDF)
        {
            // List<Aluno> listagemAlunos = _repositorioGenericoAluno.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).OrderBy(n => n.Nome).ToList<Aluno>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            List<AlunoExibicaoViewModel> listagemAlunos = mapper.Map<List<AlunoExibicaoViewModel>>(_repositorioGenericoAluno.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()));

            if (gerarPDF != true)
            {
                //Definindo a paginação              
                int paginaQdteRegistros = 10;
                int paginaNumeroNavegacao = (pagina ?? 1);

                return View(listagemAlunos.ToPagedList(paginaNumeroNavegacao,
                paginaQdteRegistros));
            }
            else
            {
                int paginaNumero = 1;

                var pdf = new ViewAsPdf
                {
                    ViewName = "ListagemAlunos",
                    PageSize = Size.A4,
                    IsGrayScale = true,
                    Model = listagemAlunos.ToPagedList(paginaNumero, listagemAlunos.Count)
                };
                return pdf;
            }
        }

        #region Aluno
       
        #region Emitir Contrato
        public ActionResult EmitirContrato(Boolean? gerarPDF, int? codAlunoMatricula)
        {

            // List<Aluno> listagemAlunos = _repositorioGenericoAluno.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).OrderBy(n => n.Nome).ToList<Aluno>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            var alunoMatricula = _repositorioAlunoMatricula.SelecionarPorId(codAlunoMatricula.Value);
            var aluno = _repositorioGenericoAluno.SelecionarPorId(alunoMatricula.CodAluno);
            var turma = _repositorioTurma.SelecionarPorId(alunoMatricula.CodTurma);
            TurmaExibicaoViewModel turmaExibicao = mapper.Map<TurmaExibicaoViewModel>(turma);
            AlunoExibicaoViewModel alunoEmissao = mapper.Map<AlunoExibicaoViewModel>(aluno);
            AlunoMatriculaExibicaoViewModel alunoMatriculaExibicao = mapper.Map<AlunoMatriculaExibicaoViewModel>(alunoMatricula);
            List<TelefoneExibicaoViewModel> telefoneAlunoConsulta = mapper.Map<List<TelefoneExibicaoViewModel>>(_repositorioTelefone.SelecionarTelefoneAluno(alunoEmissao.CodAluno));
            List<Taxa> taxa = _repositorioGenericoTaxa.Selecionar(aluno.CodEscola);
            List<MensalidadeValor> mensalidadeValors = _repositorioMensalidadeValor.Selecionar(aluno.CodEscola);
            var responsavels = _repositorioGenericoResponsavel.Selecionar(alunoEmissao.CodAluno);
            List<ResponsavelExibicaoViewModel> responsavelAlunoConsulta = mapper.Map<List<ResponsavelExibicaoViewModel>>(responsavels);

            alunoEmissao.MensalidadeValor = mensalidadeValors;
            alunoEmissao.TaxaExibicaoViewModel = mapper.Map<List<TaxaExibicaoViewModel>>(taxa);
            alunoEmissao.AlunoMatriculaExibicaoViewModel = alunoMatriculaExibicao;
            alunoEmissao.AlunoMatriculaExibicaoViewModel.Turma = turmaExibicao;
            alunoEmissao.TelefoneViewModels = telefoneAlunoConsulta;
            alunoEmissao.ResponsavelViewModels = responsavelAlunoConsulta;
            ViewBag.CodAluno = aluno.CodAluno;

            if (gerarPDF != true)
            {
                //Definindo a paginação              
                //int paginaNumeroNavegacao = (pagina ?? 1);
                ViewBag.EmitirContrato = false;
                return View(alunoEmissao);
            }
            else
            {
                ViewBag.EmitirContrato = true;
                var pdf = new ViewAsPdf
                {
                    ViewName = "EmitirContrato",
                    PageSize = Size.Letter,
                    // IsGrayScale = true,
                    Model = alunoEmissao,
                    //Se quiser mostrar na tela, suprimir a linha abaixo
                    FileName = "ContratoAluno" + aluno.Matricula + ".pdf"
                };
                return pdf;
            }
        }

        #endregion

        #region Emitir Ficha do Aluno

        public ActionResult EmitirFichaAluno(Boolean? gerarPDF, int? codAlunoMatricula)
        {

            // List<Aluno> listagemAlunos = _repositorioGenericoAluno.Selecionar(UsuarioEscola.ResgatarCodigoEscolaSelecionada()).OrderBy(n => n.Nome).ToList<Aluno>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            var alunoMatricula = _repositorioAlunoMatricula.SelecionarPorId(codAlunoMatricula.Value);
            var aluno = _repositorioGenericoAluno.SelecionarPorId(alunoMatricula.CodAluno);
            var turma = _repositorioTurma.SelecionarPorId(alunoMatricula.CodTurma);
            
            List<AlunoQuestionario> questionarioAluno = _repositorioQuestionario.SelecionarPorIdAluno(alunoMatricula.CodAluno);
           // TurmaExibicaoViewModel turmaExibicao = mapper.Map<TurmaExibicaoViewModel>(turma);
            AlunoExibicaoViewModel alunoEmissao = mapper.Map<AlunoExibicaoViewModel>(aluno);
            //AlunoMatriculaExibicaoViewModel alunoMatriculaExibicao = mapper.Map<AlunoMatriculaExibicaoViewModel>(alunoMatricula);
            List<TelefoneExibicaoViewModel> telefoneAlunoConsulta = mapper.Map<List<TelefoneExibicaoViewModel>>(_repositorioTelefone.SelecionarTelefoneAluno(alunoEmissao.CodAluno));
           // List<Taxa> taxa = _repositorioGenericoTaxa.Selecionar(aluno.CodEscola);
            //List<MensalidadeValor> mensalidadeValors = _repositorioMensalidadeValor.Selecionar(aluno.CodEscola);
            var responsavels = _repositorioGenericoResponsavel.Selecionar(alunoEmissao.CodAluno);
            //List<ResponsavelExibicaoViewModel> responsavelAlunoConsulta = mapper.Map<List<ResponsavelExibicaoViewModel>>(responsavels);

            foreach (var item in telefoneAlunoConsulta)
            {
                if (item.Pessoa.ToUpper().Equals("MÃE"))
                {
                    if (item.Local.ToUpper().Equals("TRABALHO"))
                    {
                        alunoEmissao.FoneMaeTrabalho = "(" + item.Ddd + ") " + item.Numero;
                    }
                    else
                    {
                        alunoEmissao.FoneMae = "(" + item.Ddd + ") " + item.Numero;
                    }
                }
                if (item.Pessoa.ToUpper().Equals("PAI"))
                {
                    if (item.Local.ToUpper().Equals("TRABALHO"))
                    {
                        alunoEmissao.FonePaiTrabalho = "(" + item.Ddd + ") " + item.Numero;
                    }
                    else
                    {
                        alunoEmissao.FonePai = "(" + item.Ddd + ") " + item.Numero;
                    }
                }
            }



            alunoEmissao.MensalidadeValor = _repositorioMensalidadeValor.Selecionar(aluno.CodEscola); ;
            alunoEmissao.TaxaExibicaoViewModel = mapper.Map<List<TaxaExibicaoViewModel>>(_repositorioGenericoTaxa.Selecionar(aluno.CodEscola));
            alunoEmissao.AlunoMatriculaExibicaoViewModel = mapper.Map<AlunoMatriculaExibicaoViewModel>(alunoMatricula);
            alunoEmissao.AlunoMatriculaExibicaoViewModel.Turma = mapper.Map<TurmaExibicaoViewModel>(turma);
            alunoEmissao.TelefoneViewModels = telefoneAlunoConsulta;
            alunoEmissao.ResponsavelViewModels = mapper.Map<List<ResponsavelExibicaoViewModel>>(responsavels); ;
            alunoEmissao.AlunoQuestionarioExibicaoViewModels = new List<AlunoQuestionarioExibicaoViewModel>();
            foreach (var item in questionarioAluno)
            {
                AlunoQuestionarioExibicaoViewModel questionarioNovo = new AlunoQuestionarioExibicaoViewModel();
                questionarioNovo.Descricao = item.Questionario.Descricao;
                questionarioNovo.TemComplemento = item.Questionario.TemComplemento;
                questionarioNovo.Ordem = item.Questionario.Ordem;
                questionarioNovo.PerguntaComplemento = item.Questionario.PerguntaComplemento;
                questionarioNovo.Tipo = item.Questionario.Tipo;

                questionarioNovo.Resposta = item.Resposta;
                questionarioNovo.Complemento = item.Complemento;
                questionarioNovo.CodAluno = item.CodAluno;
                questionarioNovo.CodQuestionario = item.CodQuestionario;
                questionarioNovo.CodAlunoQuestionario = item.CodAlunoQuestionario;
                questionarioNovo.Inclusao = item.Inclusao;
               
                alunoEmissao.AlunoQuestionarioExibicaoViewModels.Add(questionarioNovo);
            }
            

            ViewBag.CodAluno = aluno.CodAluno;

            if (gerarPDF != true)
            {
                //Definindo a paginação              
                //int paginaNumeroNavegacao = (pagina ?? 1);
                ViewBag.EmitirContrato = false;
                return View(alunoEmissao);
            }
            else
            {
                ViewBag.EmitirContrato = true;
                var pdf = new ViewAsPdf
                {
                    ViewName = "EmitirFichaAluno",
                    PageSize = Size.Letter,
                    // IsGrayScale = true,
                    Model = alunoEmissao,
                    //Se quiser mostrar na tela, suprimir a linha abaixo
                    FileName = "FichaAluno" + aluno.Matricula + ".pdf"
                };
                return pdf;
            }
        }
        #endregion


    

        #endregion

    }
}