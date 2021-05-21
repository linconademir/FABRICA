using CEAG.WEB.ViewModel.Questionario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.AlunoQuestionario
{
    public class AlunoQuestionarioViewModel
    {
        public int CodAlunoQuestionario { get; set; }
        public int CodAluno { get; set; }
        public int CodQuestionario { get; set; }

        [Display(Name = "RESPOSTA")]
        [Required(ErrorMessage = "A resposta é obrigatória")]
        public string Resposta { get; set; }

        [Display(Name = "COMPLEMENTO")]
        public string Complemento { get; set; }
        public DateTime Inclusao { get; set; }

        public QuestionarioExibicaoViewModel QuestionarioExibicaoViewModel { get; set; }
    }
}