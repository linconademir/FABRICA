using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Telefone
{
    public class TelefoneExibicaoViewModel
    {
        private string _tipo { get; set; }
        private string _pessoa { get; set; }
        private string _observacao { get; set; }
        private string _local { get; set; }
        private string _tipoContato { get; set; }

        public int CodTelefone { get; set; }

        [Display(Name = "TIPO DE TELEFONE")]
        public string Tipo
        {
            get => _tipo;
            set => _tipo = value?.ToUpper();
        }

        [Display(Name = "NUMERO DO TELEFONE")]
        public string Numero { get; set; }

        [Display(Name = "DDD")]
        public string Ddd { get; set; }

        [Display(Name = "LOCAL")]
        public string Local
        {
            get => _local;
            set => _local = value?.ToUpper();
        }

        [Display(Name = "TIPO DE CONTATO")]
        public string TipoContato
        {
            get => _tipoContato;
            set => _tipoContato = value?.ToUpper();
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

        [Display(Name = "DATA DA INCLUSÃO")]
        public DateTime Inclusao { get; set; }
        
        public int? CodAluno { get; set; }
        public int? CodResponsavel { get; set; }

    }
}