using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Taxa
{
    public class TaxaExibicaoViewModel
    {
        public int CodTaxa { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        public string Descricao { get; set; }

        [Display(Name = "VALOR")]
        [DataType(DataType.Currency)]
        public double Valor { get; set; }


        [Display(Name = "VIAS GRATUITAS")]
        public int ViasGratuitas { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }
        public int CodEscola { get; set; }


        public DateTime Cancelamento { get; set; }
        public DateTime Inclusao { get; set; }

        public Escola Escola { get; set; }
    }
}