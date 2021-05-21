using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.Aula;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.AulaAluno
{
    public class AulaAlunoExibicaoViewModel
    {
        public int CodAulaAluno { get; set; }
        public DateTime Inclusao { get; set; }

        [Display(Name = "SITUAÇÃO")]
        public string Situacao { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        public string Observacao { get; set; }
        public int CodAlunoMatricula { get; set; }
        public AlunoMatriculaExibicaoViewModel AlunoMatriculaExibicaoViewModel { get; set; }
        public int CodAula { get; set; }
        public AulaExibicaoViewModel AulaExibicaoViewModel { get; set; }
    }
}