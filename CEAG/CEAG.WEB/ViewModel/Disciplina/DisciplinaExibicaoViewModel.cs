using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Disciplina
{
    public class DisciplinaExibicaoViewModel
    {
        public int CodDisciplina { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        public string Descricao { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }

        [Display(Name = "INCLUSÃO")]
        public DateTime Inclusao { get; set; }

        [Display(Name = "OBRIGATÓRIA")]
        public string Obrigatoria { get; set; }
        public int CodEscola { get; set; }

    }
}