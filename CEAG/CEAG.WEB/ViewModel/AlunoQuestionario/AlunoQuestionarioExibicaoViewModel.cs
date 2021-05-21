using CEAG.WEB.ViewModel.Aluno;
using System;
using System.ComponentModel.DataAnnotations;

namespace CEAG.WEB.ViewModel.AlunoQuestionario
{
    public class AlunoQuestionarioExibicaoViewModel
    {
        public int CodAlunoQuestionario { get; set; }
        public int CodAluno { get; set; }
        public int CodQuestionario { get; set; }

        [Display(Name = "RESPOSTA")]
        public string Resposta { get; set; }

        [Display(Name = "COMPLEMENTO")]
        public string Complemento { get; set; }
        public DateTime Inclusao { get; set; }

        [Display(Name = "PERGUNTA")]
        public string Descricao { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }

        [Display(Name = "EXISTE COMPLEMENTO")]
        public string TemComplemento { get; set; }
        public string PerguntaComplemento { get; set; }
        public int Ordem { get; set; }

        public AlunoExibicaoViewModel AlunoExibicaoViewModel { get; set; }
        

    }
}