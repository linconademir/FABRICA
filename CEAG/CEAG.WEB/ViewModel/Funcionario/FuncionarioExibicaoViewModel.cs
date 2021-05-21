using CEAG.WEB.ViewModel.Telefone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Funcionario
{
    public class FuncionarioExibicaoViewModel
    {
        public int CodFuncionario { get; set; }

        [Display(Name = "NOME")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Display(Name = "CPF")]
        public string CpfComMascara { get; set; }

        [Display(Name = "SEXO")]
        public string Sexo { get; set; }

        [Display(Name = "RG")]
        public string Rg { get; set; }

        [Display(Name = "FUNÇÃO")]
        public string Funcao { get; set; }

        [Display(Name = "TITULAÇÃO")]
        public string Titulacao { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        public string Observacao { get; set; }

        [Display(Name = "NACIONALIDADE")]
        public string Nacionalidade { get; set; }

        [Display(Name = "NATURALIDADE")]
        public string Naturalidade { get; set; }

        [Display(Name = "ORGÃO")]
        public string RgOrgaoEmissor { get; set; }

        [Display(Name = "UF")]
        public string RgUf { get; set; }

        [Display(Name = "DATA DE NASCIMENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Nascimento { get; set; }
        public DateTime Inclusao { get; set; }

        [Display(Name = "DATA DE NASCIMENTO")]
        public string NascimentoFormatado { get; set; }

        [Display(Name = "DATA DO CADASTRO")]
        public string InclusaoFormatado { get; set; }

        [Display(Name = "E-MAIL")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int CodEscola { get; set; }


        [Display(Name = "ENDEREÇO")]
        public string LogradouroCompleto { get; set; }

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

        public List<TelefoneExibicaoViewModel> Telefones { get; set; }

        [Display(Name = "REFERÊNCIA")]
        public string Referencia { get; set; }
        public object CodAluno { get; internal set; }
        public string Disciplinas { get; set; }
        public bool NaTurma { get; set; }
        public bool Professor { get; set; }

    }
}