using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Telefone
{
    public class TelefoneViewModel
    {
        private string _tipo { get; set; }
        private string _pessoa { get; set; }
        private string _observacao { get; set; }
        private string _local { get; set; }
        private string _tipoContato { get; set; }

        public int CodTelefone { get; set; }

        [Display(Name = "TIPO DE TELEFONE")]
        [Required(ErrorMessage = "Selecione o Tipo de Telefone")]
        public string Tipo
        {
            get => _tipo;
            set => _tipo = value?.ToUpper();
        }

        [Display(Name = "PESSOA")]
        public string Pessoa
        {
            get => _pessoa;
            set => _pessoa = value?.ToUpper();
        }

        [Display(Name = "OBSERVAÇÃO")]
        public string Observacao
        {
            get => _observacao;
            set => _observacao = value?.ToUpper();
        }

        [Display(Name = "NUMERO DO TELEFONE")]
        [Required(ErrorMessage = "Digite o numero do telefone")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Digite o DDD do telefone")]
        [Display(Name = "DDD")]
        public string Ddd { get; set; }

        [Required(ErrorMessage = "Digite o local do telefone")]
        [Display(Name = "LOCAL")]
        public string Local
        {
            get => _local;
            set => _local = value?.ToUpper();
        }

        [Required(ErrorMessage = "Digite o tipo do contato")]
        [Display(Name = "TIPO DE CONTATO")]
        public string TipoContato
        {
            get => _tipoContato;
            set => _tipoContato = value?.ToUpper();
        }

        public DateTime Inclusao { get; set; }
        public int CodAluno { get; set; }
    }
}