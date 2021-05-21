using CEAG.DOMINIO;
using CEAG.WEB.Anotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Aluno
{
    public class AlunoViewModel
    {
        private string _nome;
        private string _rgOrgaoEmissor;
        private string _email;
        private string _maeNome;
        private string _paiNome;
        private string _maeProfissao;
        private string _paiProfissao;
        private string _sexo;
        private string _tipoSanguineo;
        private string _fatorRh;
        private string _naturalidade;
        private string _nacionalidade;
        private string _logradouro;
        private string _complemento;
        private string _numero;
        private string _referencia;
        private string _bairro;
        private string _municipio;
        private string _nivelAlunoParentesco;

        public int CodAluno { get; set; }

        [Display(Name = "MATRÍCULA")]
        public string Matricula { get; set; }

        [Display(Name = "NOME DO ALUNO(A)")]
        [Required(ErrorMessage = "O nome do aluno(a) é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome do aluno não poderá conter mais que 200 caracteres.")]
        public string Nome
        {
            get => _nome;
            set => _nome = value?.ToUpper();
        }

        public int CodAlunoParentesco { get; set; }
        public string NivelAlunoParentesco
        {
            get => _nivelAlunoParentesco;
            set => _nivelAlunoParentesco = value?.ToUpper();
        }

        [Display(Name = "SEXO")]
        [Required(ErrorMessage = "O sexo é obrigatório")]
        [MaxLength(15, ErrorMessage = "O sexo do aluno não poderá conter mais que 15 caracteres.")]
        public string Sexo
        {
            get => _sexo;
            set => _sexo = value?.ToUpper();
        }

        [Display(Name = "CPF")]
        [Cpfvalidade(ErrorMessage = "O CPF não é valido")]
        public string Cpf { get; set; }

        [Display(Name = "RG")]
        [MaxLength(30, ErrorMessage = "O RG não pode ultrapassar 30 caracteres")]
        public string Rg { get; set; }

        [Display(Name = "UF")]
        [MaxLength(2, ErrorMessage = "Não pode ultrapssar 2 caracteres")]
        public string RgUf { get; set; }

        [Display(Name = "ORGÃO")]
        public string RgOrgaoEmissor
        {
            get => _rgOrgaoEmissor;
            set => _rgOrgaoEmissor = value?.ToUpper();
        }

        [Display(Name = "DATA DE NASCIMENTO")]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Nascimento { get; set; }

        public DateTime Inclusao { get; set; }

        [Display(Name = "E-MAIL")]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get => _email;
            set => _email = value?.ToUpper();
        }


        [Display(Name = "NOME DA MÃE")]
        public string MaeNome
        {
            get => _maeNome;
            set => _maeNome = value?.ToUpper();
        }

        [Display(Name = "PROFISSÃO DA MÃE")]
        public string MaeProfissao
        {
            get => _maeProfissao;
            set => _maeProfissao = value?.ToUpper();
        }

        [Display(Name = "NOME DO PAI")]
        public string PaiNome
        {
            get => _paiNome;
            set => _paiNome = value?.ToUpper();
        }

        [Display(Name = "TIPO SANGUÍNEO")]
        public string TipoSanguineo
        {
            get => _tipoSanguineo;
            set => _tipoSanguineo = value?.ToUpper();
        }

        [Display(Name = "FATOR RH")]
        public string FatorRH
        {
            get => _fatorRh;
            set => _fatorRh = value?.ToUpper();
        }

        [Display(Name = "PROFISSÃO DO PAI")]
        public string PaiProfissao
        {
            get => _paiProfissao;
            set => _paiProfissao = value?.ToUpper();
        }

        [Display(Name = "NACIONALIDADE")]
        public string Nacionalidade
        {
            get => _nacionalidade;
            set => _nacionalidade = value?.ToUpper();
        }

        [Display(Name = "NATURALIDADE")]
        public string Naturalidade
        {
            get => _naturalidade;
            set => _naturalidade = value?.ToUpper();
        }

        [Display(Name = "TIPO DE CONTATO")]
        public string TipoContato { get; set; }

        public int CodLogradouro { get; set; }
        public int? CodEscola { get; set; }

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