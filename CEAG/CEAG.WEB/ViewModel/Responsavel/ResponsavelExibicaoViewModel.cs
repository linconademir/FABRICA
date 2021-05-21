using CEAG.WEB.ViewModel.Telefone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Responsavel
{
    public class ResponsavelExibicaoViewModel
    {
        public int CodResponsavel { get; set; }

        [Display(Name = "NOME")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Display(Name = "CPF")]
        public string CpfComMascara { get; set; }

        [Display(Name = "TIPO DE RESPONSÁVEL")]
        public string Tipo { get; set; }

        [Display(Name = "RG")]
        public string Rg { get; set; }

        [Display(Name = "ORGÃO")]
        public string RgOrgaoEmissor { get; set; }

        [Display(Name = "UF")]
        public string RgUf { get; set; }

        [Display(Name = "PROFISSÃO")]
        public string Profissao { get; set; }

        [Display(Name = "E-MAIL")]
        public string Email { get; set; }

       [Display(Name = "ESTADO CIVIL")]
        public string EstadoCivil { get; set; }

        [Display(Name = "NATURALIDADE")]
        public string Naturalidade { get; set; }

        [Display(Name = "SEXO")]
        public string Sexo { get; set; }

        [Display(Name = "NACIONALIDADE")]
        public string Nacionalidade { get; set; }


        [Display(Name = "RECEBE E-MAIL")]
        public string RecebeEmail { get; set; }

        public int CodAluno { get; set; }

        [Display(Name = "DECLARA IR?")]
        public string DeclaraIR { get; set; }

        public int CodLogradouro { get; set; }

        [Display(Name = "ENDEREÇO")]
        public string LogradouroCompleto { get; set; }


        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Display(Name = "LOGRADOURO")]
        public string Rua { get; set; }

        [Display(Name = "NUMERO")]
        public string Numero { get; set; }

        [Display(Name = "COMPLEMENTO")]
        public string Complemento { get; set; }

        [Display(Name = "BAIRRO")]
        public string Bairro { get; set; }

        [Display(Name = "MUNICÍPIO")]
        public string Municipio { get; set; }

        [Display(Name = "UF")]
        public string Uf { get; set; }

        [Display(Name = "REFERÊNCIA")]
        public string Referencia { get; set; }

        public List<TelefoneExibicaoViewModel> Telefones { get; set; }
        public int CodTelefone { get; set; }
    }
}