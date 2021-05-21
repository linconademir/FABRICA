    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.MensalidadeValores
{
    public class MensalidadeValorExibicaoViewModel
    {
        public int CodMensalidadeValor { get; set; }

        [Display(Name = "TURMA")]
        public string Turma { get; set; }

        [Display(Name = "MENSALIDADE")]
        [DataType(DataType.Currency)]
        public double ValorMensal { get; set; }

        //[Display(Name = "DESCONTO DE IRMÃOS")]
        //[DataType(DataType.Currency)]
        //public double ValorComDesconto { get; set; }

        [Display(Name = "ANUIDADE")]
        [DataType(DataType.Currency)]
        public double ValorAnual { get; set; }

        [Display(Name = "ANUIDADE À VISTA")]
        [DataType(DataType.Currency)]
        public double ValorAnualVista { get; set; }

        [Display(Name = "DESCONTO POR ATENCIPAÇÃO")]
        [DataType(DataType.Currency)]
        public double ValorDescontoAntecipacao { get; set; }

        [Display(Name = "DESCONTO POR IRMÃO")]
        [DataType(DataType.Currency)]
        public double ValorDescontoIrmao { get; set; }

        [Display(Name = "TAXA DE MATERIAL")]
        [DataType(DataType.Currency)]
        public double ValorTaxaMaterial { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }
        public int? CodAcesso { get; set; }
        public int? CodEscola { get; set; }

    }
}