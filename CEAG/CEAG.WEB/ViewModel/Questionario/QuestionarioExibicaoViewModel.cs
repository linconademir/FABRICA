using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Questionario
{
    public class QuestionarioExibicaoViewModel
    {
        public int CodQuestionario { get; set; }

        [Display(Name = "PERGUNTA")]
        public string Descricao { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }

        [Display(Name = "EXISTE COMPLEMENTO")]
        public string TemComplemento { get; set; }
        public string PerguntaComplemento { get; set; }
        public int Ordem { get; set; }
    }
}