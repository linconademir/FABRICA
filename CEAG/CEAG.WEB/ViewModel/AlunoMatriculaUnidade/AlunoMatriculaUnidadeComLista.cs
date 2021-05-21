using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.AlunoMatriculaUnidade
{
    public class AlunoMatriculaUnidadeComLista
    {
        public IList<AlunoMatriculaUnidadeViewModel> AlunoMatriculaUnidadeViewModels { get; set; }
        public int CodTurma { get; set; }

    }
}