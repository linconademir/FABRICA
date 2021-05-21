using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Unidade
{
    public class UnidadeViewModel
    {
        public int CodUnidade { get; set; }
        public int? CodEscola { get; set; }

        [Display(Name = "ANO")]
        [Required(ErrorMessage = "O Ano é obrigatório")]
        public int Ano { get; set; }

        [Display(Name = "UNIDADE")]
        public int Numero { get; set; }

        [Display(Name = "ABERTURA")]
        [Required(ErrorMessage = "A data de abertura é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Abertura { get; set; }
        public DateTime Inclusao { get; set; }


        [Display(Name = "FECHAMENTO")]
        [Required(ErrorMessage = "A data de fechamento é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Fechamento { get; set; }

        public Escola Escola { get; set; }

    }
}