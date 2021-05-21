using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.MensalidadeValores
{
    public class MensalidadeValorViewModel
    {
        public int CodMensalidadeValor { get; set; }

        [Display(Name = "TURMA")]
        [Required(ErrorMessage = "A turma é obrigatória")]
        public string Turma { get; set; }

        [Display(Name = "INTEGRAL")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O valor integral é obrigatório")]
        public double ValorMensal { get; set; }

        [Display(Name = "DESCONTO POR IRMÃO")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O valor de desconto por irmão é obrigatório")]
        public double ValorDescontoIrmao { get; set; }

        //[Display(Name = "COM DESCONTO DE IRMÃOS")]
        //[DataType(DataType.Currency)]
        //[Required(ErrorMessage = "O valor com desconto é obrigatório")]
        //public double ValorComDesconto { get; set; }

        [Display(Name = "ANUIDADE")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O valor anual é obrigatório")]
        public double ValorAnual { get; set; }

        [Display(Name = "ANUIDADE A VISTA")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O valor anual à vista é obrigatório")]
        public double ValorAnualVista { get; set; }


        [Display(Name = "TAXA DE MATERIAL")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O valor de taxa de material é obrigatório")]
        public double ValorTaxaMaterial { get; set; }

        [Display(Name = "DESCONTO POR ATENCIPAÇÃO")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O valor de desconto por atencipação da parcela é obrigatório")]
        public double ValorDescontoAntecipacao { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }
        public int? CodAcesso { get; set; }
        public int? CodEscola { get; set; }

    }
}