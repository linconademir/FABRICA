using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Funcionario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.AlunoMatriculaUnidade
{
    public class AlunoMatriculaUnidadeViewModel
    {
        public int CodAlunoMatriculaUnidade { get; set; }

        [Display(Name = "NOTA TESTE")]
        public double? NotaTeste { get; set; }

        [Display(Name = "NOTA PROVA")]
        public double? NotaProva { get; set; }

        [Display(Name = "NOTA COMPLEMENTAR")]
        public double? NotaPC { get; set; }

        [Display(Name = "NOTA RECUPERAÇÃO")]
        public double? NotaRecuperacao { get; set; }
        public DateTime Inclusao { get; set; }

        [Display(Name = "UNIDADE")]
        public int NumeroUnidade { get; set; }
        public double? Media { get; set; }

        [Display(Name = "CONSELHO")]
        public double? NotaConselhoClasse { get; set; }


        public DateTime? Fechamento { get; set; }


        public int CodFuncionario { get; set; }
        public FuncionarioViewModel Funcionario { get; set; }
        public int CodDisciplina { get; set; }
        public virtual DisciplinaViewModel Disciplina { get; set; }
        public int CodAlunoMatricula { get; set; }
        public virtual AlunoMatriculaViewModel AlunoMatricula { get; set; }
    }
}