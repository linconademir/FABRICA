using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Horario
{
    public class HorarioViewModel
    {

        public int CodHorario { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; }
        public int? CodEscola { get; set; }
        public DateTime Inclusao { get; set; }
        public DateTime? Cancelamento { get; set; }
        public List<HorarioAulaViewModel> HorarioAulaViewModels { get; set; }
    }
}