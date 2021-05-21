using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Taxa
{
    public class TaxaViewModel
    {
        public int CodTaxa { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(200, ErrorMessage = "A descrição não poderá conter mais que 200 caracteres.")]
        public string Descricao { get; set; }

        [Display(Name = "VALOR")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O valor é obrigatório (Digite 'zero' para valores gratuítos")]
        public double Valor { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }


        [Display(Name = "VIAS GRATUITAS")]
        public int ViasGratuitas { get; set; }

        public int? CodEscola { get; set; }


        public DateTime? Cancelamento { get; set; }
        public DateTime Inclusao { get; set; }

        public Escola Escola { get; set; }
    }
}