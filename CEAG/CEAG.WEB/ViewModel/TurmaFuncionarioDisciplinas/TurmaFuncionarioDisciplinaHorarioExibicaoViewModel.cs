using CEAG.WEB.ViewModel.Horario;
using CEAG.WEB.ViewModel.Turma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.TurmaFuncionarioDisciplinas
{
    public class TurmaFuncionarioDisciplinaHorarioExibicaoViewModel
    {
        public List<TurmaFuncionarioDisciplinaExibicaoViewModel> TurmaFuncionarioDisciplinaExibicaoViewModels { get; set; }
        public List<HorarioAulaViewModel> HorarioAulaViewModels { get; set; }
        public TurmaExibicaoViewModel TurmaExibicaoViewModel { get; set; }
    }
}