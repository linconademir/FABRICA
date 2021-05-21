using CEAG.DOMINIO;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.AlunoQuestionario;
using CEAG.WEB.ViewModel.Debito;
using CEAG.WEB.ViewModel.Informacao;
using CEAG.WEB.ViewModel.Responsavel;
using CEAG.WEB.ViewModel.Taxa;
using CEAG.WEB.ViewModel.Telefone;
using CEAG.WEB.ViewModel.Turma;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Aluno
{
    public class AlunoExibicaoViewModel
    {
        public int CodAluno { get; set; }

        [Display(Name = "MATRÍCULA")]
        public string Matricula { get; set; }

        [Display(Name = "NOME DO ALUNO")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Display(Name = "CPF")]
        public string CpfComMascara { get; set; }




        [Display(Name = "TIPO SANGUÍNEO")]
        public string TipoSanguineo { get; set; }

        [Display(Name = "FATOR RH")]
        public string FatorRH { get; set; }

        [Display(Name = "RG")]
        public string Rg { get; set; }

        [Display(Name = "UF")]
        public string RgUf { get; set; }

        [Display(Name = "ÓRGÃO EMISSOR")]
        public string RgOrgaoEmissor { get; set; }

        [Display(Name = "NACIONALIDADE")]
        public string Nacionalidade { get; set; }

        [Display(Name = "NATURALIDADE")]
        public string Naturalidade { get; set; }

        [Display(Name = "DATA DE NASCIMENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Nascimento { get; set; }

        [Display(Name = "DATA DO CADASTRO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Inclusao { get; set; }

        [Display(Name = "DATA DE NASCIMENTO")]
        public string NascimentoFormatado { get; set; }

        [Display(Name = "DATA DO CADASTRO")]
        public string InclusaoFormatado { get; set; }


        [Display(Name = "E-MAIL")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "NOME DA MÃE")]
        public string MaeNome { get; set; }

        [Display(Name = "SEXO")]
        public string Sexo { get; set; }


        [Display(Name = "PROFISSÃO DA MÃE")]
        public string MaeProfissao { get; set; }

        [Display(Name = "NOME DO PAI")]
        public string PaiNome { get; set; }

        [Display(Name = "PROFISSÃO DO PAI")]
        public string PaiProfissao { get; set; }

        [Display(Name = "ENDEREÇO")]
        public string LogradouroCompleto { get; set; }


        
        public string FoneMae { get; set; }

        public string FoneMaeTrabalho { get; set; }

        public string FonePaiTrabalho { get; set; }

        public string FonePai { get; set; }

        public int CodLogradouro { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório")]
        public string Cep { get; set; }

        [Display(Name = "LOGRADOURO")]
        [Required(ErrorMessage = "O nome da rua é obrigatório")]
        public string Rua { get; set; }

        [Display(Name = "NUMERO")]
        [Required(ErrorMessage = "O numero é obrigatório")]
        public string Numero { get; set; }

        [Display(Name = "COMPLEMENTO")]
        public string Complemento { get; set; }

        [Display(Name = "BAIRRO")]
        [Required(ErrorMessage = "O bairro é obrigatório")]
        public string Bairro { get; set; }

        [Display(Name = "MUNICÍPIO")]
        [Required(ErrorMessage = "O municipio é obrigatório")]
        public string Municipio { get; set; }

        [Display(Name = "UF")]
        public string Uf { get; set; }

        [Display(Name = "REFERÊNCIA")]
        public string Referencia { get; set; }

        public string NivelParentesco { get; set; }
        public int CodParentescoAluno { get; set; }

        public bool Matriculado { get; set; }

        public List<TurmaExibicaoViewModel> ListaTurmasJaMatriculadas = new List<TurmaExibicaoViewModel>();

        [Display(Name = "PARENTESCO")]
        public List<AlunoExibicaoViewModel> Parentes { get; set; }

        //public int CodLograduro { get; set; }
        public List<ResponsavelExibicaoViewModel> ResponsavelViewModels { get; set; }
        public List<TelefoneExibicaoViewModel> TelefoneViewModels { get; set; }
        public List<InformacaoExibicaoViewModel> InformacoeViewModels { get; set; }
        public List<MensalidadeValor> MensalidadeValor{ get; set; }
        public List<TaxaExibicaoViewModel> TaxaExibicaoViewModel { get; set; }
        //usado no RPT
        public AlunoMatriculaExibicaoViewModel AlunoMatriculaExibicaoViewModel { get; set; }
        public List<AlunoMatriculaExibicaoViewModel> ListaAlunoMatriculaExibicaoViewModel { get; set; }
        public TurmaExibicaoViewModel TurmaExibicaoViewModel { get; set; }

        public List<AlunoQuestionarioExibicaoViewModel> AlunoQuestionarioExibicaoViewModels { get; set; }

        public List<DebitoExibicaoViewModel> ListaDebitosPagosAbertos{ get; set; }

        public Escola Escola { get; set; }


    }
}