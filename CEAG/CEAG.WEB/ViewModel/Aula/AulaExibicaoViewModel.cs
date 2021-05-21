using CEAG.WEB.ViewModel.AulaAluno;
using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Turma;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Aula
{
    public class AulaExibicaoViewModel
    {
        public int CodAula { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString =@"{0:dd/MM/yyyy}")]
        [Display(Name = "DIA DA AULA")]
        public DateTime Realizada { get; set; }
        public DateTime Inclusao { get; set; }
        public DateTime? Cancelamento { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        public string Observacao { get; set; }


        [Display(Name = "ASSUNTO")]
        public string Assunto { get; set; }
        public int CodTurma { get; set; }
        public TurmaExibicaoViewModel Turma { get; set; }
        public int CodDisciplina { get; set; }
        public DisciplinaExibicaoViewModel Disciplina { get; set; }
        public List<AulaAlunoExibicaoViewModel> AulaAlunos { get; set; }
    }
}