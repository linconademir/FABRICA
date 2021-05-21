using CEAG.DOMINIO;
using CEAG.WEB.ViewModel.Aluno;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CEAG.WEB.ViewModel.Telefone;
using CEAG.WEB.ViewModel.Informacao;
using CEAG.WEB.ViewModel.Responsavel;
using CEAG.WEB.ViewModel.Turma;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.Funcionario;
using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Acesso;
using CEAG.WEB.ViewModel.FuncionarioDisciplina;
using CEAG.WEB.ViewModel.TurmaFuncionarioDisciplinas;
using CEAG.WEB.ViewModel.AlunoMatriculaUnidade;
using CEAG.WEB.ViewModel.AlunoQuestionario;
using CEAG.WEB.ViewModel.Questionario;
using CEAG.WEB.ViewModel.Taxa;
using CEAG.WEB.ViewModel.Aula;
using CEAG.WEB.ViewModel.AulaAluno;
using CEAG.WEB.ViewModel.Horario;
using CEAG.WEB.ViewModel.Feriado;
using CEAG.WEB.ViewModel.Unidade;
using CEAG.WEB.ViewModel.MensalidadeValores;
using CEAG.WEB.ViewModel.Debito;
using CEAG.WEB.ViewModel.Advertencia;
using CEAG.WEB.ViewModel.PontoAtencao;
using CEAG.WEB.ViewModel.Parentesco;

namespace CEAG.WEB.AutoMapper
{
    public class DominioParaViewModelProfile : Profile
    {
        public DominioParaViewModelProfile()
        {

            // Add as many of these lines as you need to map your objects
            CreateMap<Aluno, AlunoExibicaoViewModel>()
                .ForMember(p => p.NascimentoFormatado, opt =>
                {
                    opt.MapFrom(src =>
                    string.Format("{0}", src.Nascimento.ToString("dd/MM/yyyy"))
                    );
                })
                .ForMember(p => p.InclusaoFormatado, opt =>
                {
                    opt.MapFrom(src =>
                    string.Format("{0}", src.Inclusao.ToString("dd/MM/yyyy"))
                    );
                })
                 .ForMember(p => p.CpfComMascara, opt =>
                 {
                     opt.MapFrom(src =>
                     string.Format("{0}", Convert.ToUInt64(src.Cpf).ToString(@"000\.000\.000\-00"))
                     );
                 })
                 .ForMember(p => p.LogradouroCompleto, opt =>
                 {
                     opt.MapFrom(src =>
                     string.Format("{0}, {1} BAIRRO {2} - {3}{4} - CEP: {5}",
                     src.Logradouro.Rua.ToString(),
                     src.Logradouro.Numero.ToString(),
                     src.Logradouro.Bairro.ToString(),
                     src.Logradouro.Municipio.ToString(),
                     src.Logradouro.Uf.ToString(),
                     Convert.ToUInt64(src.Logradouro.Cep).ToString(@"00 000\-000"))
                     );
                 });

            CreateMap<Aluno, AlunoViewModel>();
            CreateMap<Responsavel, ResponsavelExibicaoViewModel>()
                .ForMember(p => p.LogradouroCompleto, opt =>
                {
                    opt.MapFrom(src =>
                    string.Format("{0}, {1} BAIRRO {2} - {3}{4} - CEP: {5}",
                    src.Logradouro.Rua.ToString() ?? "",
                    src.Logradouro.Numero.ToString() ?? "",
                    src.Logradouro.Bairro.ToString() ?? "",
                    src.Logradouro.Municipio.ToString() ?? "",
                    src.Logradouro.Uf.ToString() ?? "",
                    Convert.ToUInt64(src.Logradouro.Cep).ToString(@"00 000\-000") ?? "")
                    );
                }).ForMember(p => p.CpfComMascara, opt =>
                {
                    opt.MapFrom(src =>
                    string.Format("{0}", Convert.ToUInt64(src.Cpf).ToString(@"000\.000\.000\-00"))
                    );
                });

            CreateMap<Responsavel, ResponsavelViewModel>();

            CreateMap<Funcionario, FuncionarioExibicaoViewModel>()
                .ForMember(p => p.NascimentoFormatado, opt =>
                {
                    opt.MapFrom(src =>
                    string.Format("{0}", src.Nascimento.ToString("dd/MM/yyyy"))
                    );
                })
                .ForMember(p => p.InclusaoFormatado, opt =>
                {
                    opt.MapFrom(src =>
                    string.Format("{0}", src.Inclusao.ToString("dd/MM/yyyy"))
                    );
                })
                 .ForMember(p => p.CpfComMascara, opt =>
                 {
                     opt.MapFrom(src =>
                     string.Format("{0}", Convert.ToUInt64(src.Cpf).ToString(@"000\.000\.000\-00"))
                     );
                 })
                 .ForMember(p => p.LogradouroCompleto, opt =>
                 {
                     opt.MapFrom(src =>
                     string.Format("{0}, {1} BAIRRO {2} - {3}{4} - CEP: {5}",
                     src.Logradouro.Rua.ToString(),
                     src.Logradouro.Numero.ToString(),
                     src.Logradouro.Bairro.ToString(),
                     src.Logradouro.Municipio.ToString(),
                     src.Logradouro.Uf.ToString(),
                     Convert.ToUInt64(src.Logradouro.Cep).ToString(@"00 000\-000"))
                     );
                 });

            CreateMap<Funcionario, FuncionarioViewModel>();
            CreateMap<Logradouro, AlunoViewModel>();
            CreateMap<Logradouro, AlunoExibicaoViewModel>();
            CreateMap<Logradouro, ResponsavelViewModel>();
            CreateMap<Logradouro, ResponsavelExibicaoViewModel>();
            CreateMap<Informacao, InformacaoViewModel>();
            CreateMap<Informacao, InformacaoExibicaoViewModel>();
            CreateMap<Telefone, TelefoneExibicaoViewModel>();
            CreateMap<Turma, TurmaViewModel>();
            CreateMap<Turma, TurmaExibicaoViewModel>()
                .ForMember(p => p.Status, opt =>
                {
                    opt.MapFrom(src =>
                    src.AnoLetivo < DateTime.Now.Year ? "ENCERRADA" : (src.Fechamento != null ? "FECHADA" : "ABERTA" )
                        );
                })
                 .ForMember(p => p.AlunosMatriculados, opt =>
                 {
                     opt.MapFrom(src =>
                     src.AlunoMatriculas.Count()
                         );
                 });
            //.ForMember(p => p.Numero, opt =>
            //{
            //    opt.MapFrom(src =>
            //    string.Format("{0}", Convert.ToUInt64(src.Numero).ToString(@"0000-0000"))
            //    );
            //});
            CreateMap<Telefone, TelefoneViewModel>();

            CreateMap<Disciplina, DisciplinaViewModel>();
            CreateMap<Disciplina, DisciplinaExibicaoViewModel>();
            CreateMap<Acesso, AcessoViewModel>();
            CreateMap<FuncionarioDisciplina, FuncionarioDisciplinaExibicaoViewModel>();
            CreateMap<FuncionarioDisciplina, FuncionarioDisciplinaViewModel>();
            CreateMap<TurmaFuncionarioDisciplina, TurmaFuncionarioDisciplinaExibicaoViewModel>();
            CreateMap<TurmaFuncionarioDisciplina, TurmaFuncionarioDisciplinaViewModel>();
            CreateMap<AlunoMatriculaUnidade, AlunoMatriculaUnidadeViewModel>();
            CreateMap<AlunoMatriculaUnidade, AlunoMatriculaUnidadeExibicaoViewModel>();
            CreateMap<AlunoMatricula, AlunoMatriculaViewModel>();
            CreateMap<AlunoMatricula, AlunoMatriculaExibicaoViewModel>()
                 .ForMember(p => p.Ano, opt =>
                 {
                     opt.MapFrom(src =>
                     src.Turma.AnoLetivo
                         );
                 });
            CreateMap<AlunoQuestionario, AlunoQuestionarioExibicaoViewModel>();
            CreateMap<AlunoQuestionario, AlunoQuestionarioViewModel>();
            CreateMap<Questionario, QuestionarioExibicaoViewModel>();
            CreateMap<Taxa, TaxaExibicaoViewModel>();
            CreateMap<Taxa, TaxaViewModel>();
            CreateMap<Aula, AulaExibicaoViewModel>();
            CreateMap<Aula, AulaViewModel>();
            CreateMap<AulaAluno, AulaAlunoExibicaoViewModel>();
            CreateMap<AulaAluno, AulaAlunoViewModel>();
            CreateMap<Horario, HorarioViewModel>();
            CreateMap<Horario, HorarioExibicaoViewModel>();
            CreateMap<HorarioAula, HorarioAulaViewModel>();
            CreateMap<Feriado, FeriadoExibicaoViewModel>();
            CreateMap<Feriado, FeriadoViewModel>();
            CreateMap<Unidade, UnidadeViewModel>();
            CreateMap<Unidade, UnidadeExibicaoViewModel>();
            CreateMap<MensalidadeValor, MensalidadeValorExibicaoViewModel>();
            CreateMap<MensalidadeValor, MensalidadeValorViewModel>();
            CreateMap<PontoAtencao, PontoAtencaoViewModel>();
            CreateMap<Advertencia, AdvertenciaViewModel>();
            CreateMap<Parentesco, ParentescoViewModel>();
            CreateMap<ParentescoAluno, ParentescoAlunoViewModel>();

            CreateMap<Advertencia, AdvertenciaExibicaoViewModel>()
                    .ForMember(p => p.Status, opt =>
                    {
                        opt.MapFrom(src =>
                        src.Resolucao.HasValue ? "RESOLVIDO" : "PENDENTE"
                            );
                    });

            CreateMap<DebitoHistorico, DebitoHistoricoExibicaoViewModel>();
            CreateMap<Debito, DebitoExibicaoViewModel>()
                 .ForMember(p => p.Status, opt =>
                 {
                     opt.MapFrom(src =>
                     src.Pagamento.HasValue ? "PAGO" : (src.Vencimento < DateTime.Now ? "VENCIDO" : "A VENCER")
                         );
                 });
            CreateMap<Debito, DebitoViewModel>()
                 .ForMember(p => p.Status, opt =>
                 {
                     opt.MapFrom(src =>
                     src.Pagamento.HasValue ? "PAGO" : (src.Vencimento < DateTime.Now ? "VENCIDO" : "A VENCER")
                         );
                 });
        }
    }
}