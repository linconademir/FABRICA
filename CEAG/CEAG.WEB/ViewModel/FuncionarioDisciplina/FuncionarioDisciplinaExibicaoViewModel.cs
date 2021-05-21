using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Funcionario;
using CEAG.WEB.ViewModel.Horario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.FuncionarioDisciplina
{
    public class FuncionarioDisciplinaExibicaoViewModel
    {
        public int CodFuncionarioDisciplina { get; set; }
        public int CodFuncionario { get; set; }
        public int CodDisciplina { get; set; }
        public DateTime Inclusao { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        public string Observacao { get; set; }
        public FuncionarioExibicaoViewModel Funcionario { get; set; }
        public DisciplinaExibicaoViewModel Disciplina { get; set; }
        public List<HorarioAulaViewModel> HorarioAulaViewModels { get; set; }

        public string Disciplinas { get; set; }
    


    }
}