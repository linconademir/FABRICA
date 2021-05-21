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
    public class AulaViewModel
    {
        private string _assunto { get; set; }
        public int CodAula { get; set; }

        [Display(Name = "DIA DA AULA")]
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Realizada { get; set; }
        public DateTime Inclusao { get; set; }
        public DateTime? Cancelamento { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "O campo de observação não poderá conter mais que 500 caracteres.")]
        public string Observacao { get; set; }


        [Display(Name = "ASSUNTO")]
        [MaxLength(500, ErrorMessage = "O campo assunto não poderá conter mais que 500 caracteres.")]
        public string Assunto
        {
            get => _assunto;
            set => _assunto = value?.ToUpper();
        }

        public int CodTurma { get; set; }
        public TurmaExibicaoViewModel TurmaExibicaoViewModel { get; set; }
        public int CodDisciplina { get; set; }
        public DisciplinaExibicaoViewModel Disciplina { get; set; }
        public int CodHorarioAula { get; set; }
        public List<AulaAlunoExibicaoViewModel> AulaAlunos { get; set; }
    }
}