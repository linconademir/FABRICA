using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Debito
{
    public class DebitoViewModel
    {
        private string _tipo;
        private string _observacao;
        private string _responsavel;
        private string _descricao;
        private string _formaPagamento;


        public int CodDebito { get; set; }
        public DateTime Inclusao { get; set; }

        public DateTime? Pagamento { get; set; }

        [Display(Name = "PERÍODO")]
        public DateTime Periodo { get; set; }

        [Display(Name = "VENCIMENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "O valor do vencimento é obrigatório")]
        public DateTime Vencimento { get; set; }

        public DateTime? Negociacao { get; set; }

        [Display(Name = "TIPO")]
        public string TipoDebito 
        {
            get => _tipo;
            set => _tipo = value?.ToUpper();
        }

        [Display(Name = "RESPONSÁVEL")]
        public string Responsavel
        {
            get => _responsavel;
            set => _responsavel = value?.ToUpper();
        }

        public string IdenficadorHexa { get; set; }

        [Display(Name = "VALOR TOTAL")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O valor do débito é obrigatório")]
        public double ValorDebito { get; set; }

        [Display(Name = "VALOR PAGO")]
        [DataType(DataType.Currency)]
        public double? ValorPago { get; set; }

        [Display(Name = "DESCONTO")]
        [DataType(DataType.Currency)]
        public double? Desconto { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao
        {
            get => _descricao;
            set => _descricao = value?.ToUpper();
        }


        [Display(Name = "OBSERVAÇÃO")]
        [DataType(DataType.MultilineText)]
        public string Observacao
        {
            get => _observacao;
            set => _observacao = value?.ToUpper();
        }

        [Display(Name = "FORMA DE PAGAMENTO")]
        [Required(ErrorMessage = "A forma de pagamento é obrigatória")]
        public string FormaPagamento
        {
            get => _formaPagamento;
            set => _formaPagamento = value?.ToUpper();
        }

        [Display(Name = "STATUS")]
        public string Status { get; set; }

        [Display(Name = "PARCELAS")]
        public int Parcelas { get; set; }


        public int? CodAcesso { get; set; }

        public int CodAlunoMatricula { get; set; }
    }
}