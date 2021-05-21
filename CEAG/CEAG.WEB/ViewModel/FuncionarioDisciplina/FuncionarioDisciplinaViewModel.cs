using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Funcionario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.FuncionarioDisciplina
{
    public class FuncionarioDisciplinaViewModel
    {
        public int CodFuncionarioDisciplina { get; set; }
        public int CodFuncionario { get; set; }
        public int CodDisciplina { get; set; }
        public DateTime Inclusao { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        [MaxLength(200, ErrorMessage = "A observação não poderá conter mais que 200 caracteres.")]
        public string Observacao { get; set; }
        public FuncionarioExibicaoViewModel Funcionario { get; set; }
        public DisciplinaExibicaoViewModel Disciplina { get; set; }
    }
}