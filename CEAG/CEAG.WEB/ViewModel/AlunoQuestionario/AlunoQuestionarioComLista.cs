using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.AlunoQuestionario
{
    public class AlunoQuestionarioComLista
    {
        public IList<AlunoQuestionarioViewModel> AlunoQuestionarioViewModels { get; set; }
        public int CodAluno { get; set; }
    }
}