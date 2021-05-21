using AutoMapper;
using CEAG.DOMINIO;
using CEAG.WEB.ViewModel.Acesso;
using CEAG.WEB.ViewModel.Advertencia;
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.AlunoMatriculaUnidade;
using CEAG.WEB.ViewModel.AlunoQuestionario;
using CEAG.WEB.ViewModel.Aula;
using CEAG.WEB.ViewModel.AulaAluno;
using CEAG.WEB.ViewModel.Debito;
using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Feriado;
using CEAG.WEB.ViewModel.Funcionario;
using CEAG.WEB.ViewModel.FuncionarioDisciplina;
using CEAG.WEB.ViewModel.Horario;
using CEAG.WEB.ViewModel.Informacao;
using CEAG.WEB.ViewModel.MensalidadeValores;
using CEAG.WEB.ViewModel.Parentesco;
using CEAG.WEB.ViewModel.PontoAtencao;
using CEAG.WEB.ViewModel.Questionario;
using CEAG.WEB.ViewModel.Responsavel;
using CEAG.WEB.ViewModel.Taxa;
using CEAG.WEB.ViewModel.Telefone;
using CEAG.WEB.ViewModel.Turma;
using CEAG.WEB.ViewModel.TurmaFuncionarioDisciplinas;
using CEAG.WEB.ViewModel.Unidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.AutoMapper
{
    public class ViewModelParaDominioProfile : Profile
    {
        public ViewModelParaDominioProfile()
        {
            CreateMap<AlunoViewModel, Aluno>();
            CreateMap<AlunoExibicaoViewModel, Aluno>();
            CreateMap<ResponsavelViewModel, Responsavel>();
            CreateMap<ResponsavelExibicaoViewModel, Responsavel>();
            CreateMap<FuncionarioViewModel, Funcionario>();
            CreateMap<FuncionarioExibicaoViewModel, Funcionario>();
            CreateMap<AlunoViewModel, Logradouro>();
            CreateMap<FuncionarioViewModel, Logradouro>();
            CreateMap<ResponsavelViewModel, Logradouro>();
            CreateMap<InformacaoViewModel, Informacao>();
            CreateMap<TelefoneViewModel, Telefone>();
            CreateMap<TelefoneExibicaoViewModel, Telefone>();
            CreateMap<InformacaoExibicaoViewModel, Informacao>();
            CreateMap<TurmaExibicaoViewModel, Turma>();
            CreateMap<TurmaViewModel, Turma>();
            CreateMap<AlunoMatriculaViewModel, AlunoMatricula>();
            CreateMap<AlunoMatriculaExibicaoViewModel, AlunoMatricula>();
            CreateMap<DisciplinaViewModel, Disciplina>();
            CreateMap<DisciplinaExibicaoViewModel, Disciplina>();
            CreateMap<FuncionarioDisciplinaExibicaoViewModel, FuncionarioDisciplina>();
            CreateMap<FuncionarioDisciplinaViewModel, FuncionarioDisciplina>();
            CreateMap<TurmaFuncionarioDisciplinaExibicaoViewModel, TurmaFuncionarioDisciplina>();
            CreateMap<TurmaFuncionarioDisciplinaViewModel, TurmaFuncionarioDisciplina>();
            CreateMap<AlunoMatriculaUnidadeViewModel, AlunoMatriculaUnidade>();
            CreateMap<AlunoMatriculaUnidadeExibicaoViewModel, AlunoMatriculaUnidade>();
            CreateMap<AlunoQuestionarioExibicaoViewModel, AlunoQuestionario>();
            CreateMap<AlunoQuestionarioViewModel, AlunoQuestionario>();
            CreateMap<QuestionarioExibicaoViewModel, Questionario>();
            CreateMap<TaxaExibicaoViewModel, Taxa>();
            CreateMap<TaxaViewModel, Taxa>();
            CreateMap<AulaExibicaoViewModel, Aula>();
            CreateMap<AulaViewModel, Aula>();
            CreateMap<AulaAlunoExibicaoViewModel, AulaAluno>();
            CreateMap<AulaAlunoViewModel, AulaAluno>();
            CreateMap<HorarioViewModel, Horario>();
            CreateMap<HorarioExibicaoViewModel, Horario>();
            CreateMap<HorarioAulaViewModel, HorarioAula>();
            CreateMap<FeriadoExibicaoViewModel, Feriado>();
            CreateMap<FeriadoViewModel, Feriado>();
            CreateMap<UnidadeViewModel, Unidade>();
            CreateMap<UnidadeExibicaoViewModel, Unidade>();
            CreateMap<MensalidadeValorExibicaoViewModel, MensalidadeValor>();
            CreateMap<MensalidadeValorViewModel, MensalidadeValor>();
            CreateMap<DebitoExibicaoViewModel, Debito>();
            CreateMap<DebitoViewModel, Debito>();
            CreateMap<DebitoHistoricoExibicaoViewModel, DebitoHistorico>();
            CreateMap<PontoAtencaoViewModel, PontoAtencao>();
            CreateMap<AdvertenciaViewModel, Advertencia>();
            CreateMap<AdvertenciaExibicaoViewModel, Advertencia>();

            CreateMap<AcessoViewModel, Acesso>();
            CreateMap<ParentescoViewModel, Parentesco>();
            CreateMap<ParentescoAlunoViewModel, ParentescoAluno>();
        }
    }
}