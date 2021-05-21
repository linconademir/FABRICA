using CEAG.WEB.ViewModel.Aluno;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Parentesco
{
    public class ParentescoAlunoViewModel
    {
        public int CodParentescoAluno { get; set; }
        public int? CodParentesco { get; set; }

        [Display(Name = "PARENTE")]
        public int? CodAluno { get; set; }

        [Display(Name = "NÍVEL PARENTESCO")]
        public string Descricao { get; set; }
        public int CodAlunoBase { get; set; }

        public AlunoExibicaoViewModel Aluno { get; set; }
        public DateTime Inclusao { get; set; }
    }
}