using CEAG.WEB.ViewModel.Funcionario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.PontoAtencao
{
    public class PontoAtencaoViewModel
    {
        public int CodPontoAtencao { get; set; }

        [Display(Name = "TIPO")]
        public string TipoPontoAtencao { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        public string DescricaoPontoAtencao { get; set; }
        public int CodReferencia { get; set; }
        public DateTime? Resolucao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "INCLUSÃO")]
        public DateTime Inclusao { get; set; }
        public int CodEscola { get; set; }
        public string AcessoNome { get; set; }
    }
}