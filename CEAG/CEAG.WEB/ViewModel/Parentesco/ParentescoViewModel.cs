using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Parentesco
{
    public class ParentescoViewModel
    {
        public int CodParentesco { get; set; }

        [Display(Name = "PARENTESCO")]
        public string Descricao { get; set; }
        public DateTime Inclusao { get; set; }
        public int CodAcesso { get; set; }
        public int? CodEscola { get; set; }
    }
}