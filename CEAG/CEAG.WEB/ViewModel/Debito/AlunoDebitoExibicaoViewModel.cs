using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.Responsavel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Debito
{
    public class AlunoDebitoExibicaoViewModel
    {
        public AlunoExibicaoViewModel AlunoExibicaoViewModel { get; set; }
        public List<ResponsavelExibicaoViewModel> ResponsavelExibicaoViewModels { get; set; }
        public List<DebitoExibicaoViewModel> DebitoExibicaoViewModels { get; set; }
        public int DebitosEmAberto { get; set; }
        public int DebitosEmAtraso { get; set; }
        public string TotalDebitosEmAberto { get; set; }
        public string TotalDebitosEmAtraso { get; set; }

    }
}