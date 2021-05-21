using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Disciplina
{
    public class DisciplinaViewModel
    {
        public int CodDisciplina { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(200, ErrorMessage = "A descrição da disciplina não poderá conter mais que 200 caracteres.")]
        public string Descricao { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }
        public DateTime? Inclusao { get; set; }

        [Display(Name = "OBRIGATÓRIA")]
        public string Obrigatoria { get; set; }
        public int? CodEscola { get; set; }

        public bool Marcado { get; set; }

    }
}