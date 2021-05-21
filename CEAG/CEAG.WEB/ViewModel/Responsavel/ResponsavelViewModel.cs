using CEAG.WEB.Anotations;
using CEAG.WEB.ViewModel.Telefone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Responsavel
{
    public class ResponsavelViewModel
    {
        private string _nome;
        private string _rgOrgaoEmissor;

        private string _estadoCivil;
        private string _email;
        private string _profissao;
        private string _sexo;
        private string _tipo;
        private string _naturalidade;
        private string _nacionalidade;
        private string _logradouro;
        private string _complemento;
        private string _numero;
        private string _referencia;
        private string _bairro;
        private string _municipio;
        private string _recebeEmail;

        public int CodResponsavel { get; set; }
        public int CodLogradouro { get; set; }
        [Display(Name = "NOME")]
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome do responsável não poderá conter mais que 200 caracteres.")]
        public string Nome
        {
            get => _nome;
            set => _nome = value?.ToUpper();
        }

        [Display(Name = "CPF")]
        [Cpfvalidade(ErrorMessage = "O CPF não é valido")]
        public string Cpf { get; set; }

        [Display(Name = "TIPO DE RESPONSÁVEL")]
        public string Tipo
        {
            get => _tipo;
            set => _tipo = value?.ToUpper();
        }

        [Display(Name = "E-MAIL")]
        public string Email
        {
            get => _email;
            set => _email = value?.ToUpper();
        }

        [Display(Name = "RG")]
        [MaxLength(30, ErrorMessage = "O RG não pode ultrapassar 30 caracteres")]
        public string Rg { get; set; }

        [Display(Name = "ORGÃO")]
        public string RgOrgaoEmissor
        {
            get => _rgOrgaoEmissor;
            set => _rgOrgaoEmissor = value?.ToUpper();
        }

        [Display(Name = "UF")]
        public string RgUf { get; set; }

        [Display(Name = "RECEBE E-MAIL")]
        public string RecebeEmail
        {
            get => _recebeEmail;
            set => _recebeEmail = value?.ToUpper();
        }

        [Display(Name = "SEXO")]
        public string Sexo
        {
            get => _sexo;
            set => _sexo = value?.ToUpper();
        }

        [Display(Name = "PROFISSÃO")]
        public string Profissao
        {
            get => _profissao;
            set => _profissao = value?.ToUpper();
        }

        [Display(Name = "ESTADO CIVIL")]
        public string EstadoCivil
        {
            get => _estadoCivil;
            set => _estadoCivil = value?.ToUpper();
        }

        [Display(Name = "NATURALIDADE")]
        public string Naturalidade
        {
            get => _naturalidade;
            set => _naturalidade = value?.ToUpper();
        }

        [Display(Name = "NACIONALIDADE")]
        public string Nacionalidade
        {
            get => _nacionalidade;
            set => _nacionalidade = value?.ToUpper();
        }

        public int CodAluno { get; set; }

        [Display(Name = "DECLARA IR?")]
        public string DeclaraIR { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório")]
        public string Cep { get; set; }

        [Display(Name = "LOGRADOURO")]
        [Required(ErrorMessage = "O nome da rua é obrigatório")]
        public string Rua
        {
            get => _logradouro;
            set => _logradouro = value?.ToUpper();
        }

        [Display(Name = "NUMERO")]
        [Required(ErrorMessage = "O numero é obrigatório")]
        public string Numero
        {
            get => _numero;
            set => _numero = value?.ToUpper();
        }

        [Display(Name = "COMPLEMENTO")]
        public string Complemento
        {
            get => _complemento;
            set => _complemento = value?.ToUpper();
        }

        [Display(Name = "BAIRRO")]
        [Required(ErrorMessage = "O bairro é obrigatório")]
        public string Bairro
        {
            get => _bairro;
            set => _bairro = value?.ToUpper();
        }

        [Display(Name = "MUNICÍPIO")]
        [Required(ErrorMessage = "O municipio é obrigatório")]
        public string Municipio
        {
            get => _municipio;
            set => _municipio = value?.ToUpper();
        }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "O estado é obrigatório")]
        public string Uf { get; set; }

        [Display(Name = "REFERÊNCIA")]
        public string Referencia
        {
            get => _referencia;
            set => _referencia = value?.ToUpper();
        }

        [Display(Name = "TIPO DE TELEFONE")]
        public string TipoTelefone { get; set; }

        [Display(Name = "NUMERO DO TELEFONE")]
        public string NumeroTelefone { get; set; }

        [Display(Name = "DDD")]
        public string Ddd { get; set; }

        [Display(Name = "LOCAL")]
        public string LocalTelefone { get; set; }

        public DateTime InclusaoTelefone { get; set; }
        public int CodTelefone { get; set; }


    }
}