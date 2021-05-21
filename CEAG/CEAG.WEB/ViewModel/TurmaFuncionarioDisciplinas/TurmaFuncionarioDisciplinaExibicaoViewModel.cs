using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Funcionario;
using CEAG.WEB.ViewModel.Horario;
using CEAG.WEB.ViewModel.Turma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.TurmaFuncionarioDisciplinas
{
    public class TurmaFuncionarioDisciplinaExibicaoViewModel
    {
        public int CodTurmaFuncionarioDisciplina { get; set; }
        public DateTime Inclusao { get; set; }
        public int? CargaHoraria { get; set; }
        public int CodTurma { get; set; }
        public int CodDisciplina { get; set; }
        public int CodFuncionario { get; set; }
        public int CodHorarioAula { get; set; }
        public int DiaSemana { get; set; }
        public TurmaExibicaoViewModel Turma { get; set; }
        public DisciplinaExibicaoViewModel Disciplina { get; set; }
        public FuncionarioExibicaoViewModel Funcionario { get; set; }
        public List<HorarioExibicaoViewModel> HorarioExibicaoViewModels { get; set; }
        public List<int> DiasChamadasRealizadas { get; set; }
        public List<CEAG.DOMINIO.Feriado> DatasFeriadosImprensados { get; set; }
        public List<int> DiasFeriadosImprensados { get; set; }

    }
}