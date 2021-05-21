using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Unidade
{
    public class UnidadeExibicaoViewModel
    {
        public int CodUnidade { get; set; }
        public int? CodEscola { get; set; }

        [Display(Name = "ANO")]
        public int Ano { get; set; }

        [Display(Name = "UNIDADE")]
        public int Numero { get; set; }

        [Display(Name = "ABERTURA")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Abertura { get; set; }

        public DateTime Inclusao { get; set; }
        
        [Display(Name = "FECHAMENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Fechamento { get; set; }

        public Escola Escola { get; set; }

    }
}