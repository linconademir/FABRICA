using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Feriado
{
    public class FeriadoExibicaoViewModel
    {
        public int CodFeriado { get; set; }

        [Display(Name = "DATA")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Data { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        public string Descricao { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }

        public int CodEscola { get; set; }
    }
}