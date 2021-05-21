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
    public class AlunoMatriculaUnidadeExibicaoViewModel
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

        [Display(Name = "FALTAS")]
        public int Faltas { get; set; }

        [Display(Name = "CONSELHO")]
        public double? NotaConselhoClasse { get; set; }


        public DateTime? Fechamento { get; set; }

        public int CodFuncionario { get; set; }
        public FuncionarioExibicaoViewModel Funcionario { get; set; }
        public int CodDisciplina { get; set; }
        public virtual DisciplinaExibicaoViewModel Disciplina { get; set; }
        public int CodAlunoMatricula { get; set; }
        public virtual AlunoMatriculaExibicaoViewModel AlunoMatricula { get; set; }
        public double? Media { get; set; }


    }
}