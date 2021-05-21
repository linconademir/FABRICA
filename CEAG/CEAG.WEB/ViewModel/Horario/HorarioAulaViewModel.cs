using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Horario
{
    public class HorarioAulaViewModel
    {
        public int CodHorarioAula { get; set; }

        [Display(Name = "TURNO")]
        [Required(ErrorMessage = "O Turno é obrigatório")]
        public string Turno { get; set; }

        [Display(Name = "INÍCIO")]
        [Required(ErrorMessage = "O Início é obrigatório")]
        public TimeSpan Inicio { get; set; }

        [Display(Name = "TÉRMINO")]
        [Required(ErrorMessage = "O Término é obrigatório")]
        public TimeSpan Termino { get; set; }

        [Display(Name = "DIA DA SEMANA")]
        [Required(ErrorMessage = "O Dia da semana é obrigatório")]
        public int DiaSemana { get; set; }

        public bool HorarioFechado { get; set; }
        public string NomeDiaSemana { get; set; }
        public string Status { get; set; }
        public int CodHorario { get; set; }
    }
}