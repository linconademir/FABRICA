using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Debito
{
    public class DebitoHistoricoExibicaoViewModel
    {
        [Display(Name = "DESCRIÇÃO")]
        public string Descricao { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        public string Observacao { get; set; }

        [Display(Name = "INCLUSÃO")]
        public DateTime Inclusao { get; set; }

    }
}