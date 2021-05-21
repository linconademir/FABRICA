using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.TurmaFuncionarioDisciplinas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Turma
{
    public class TurmaExibicaoViewModel
    {
        public int CodTurma { get; set; }

        [Display(Name = "TURMA")]
        public string Descricao { get; set; }

        [Display(Name = "SEGMENTO")]
        public string Segmento { get; set; }

        [Display(Name = "VAGAS")]
        public int Vagas { get; set; }

        [Display(Name = "ANO LETIVO")]
        public int AnoLetivo { get; set; }

        [Display(Name = "FECHAMENTO")]
        public DateTime Fechamento { get; set; }

        [Display(Name = "ABERTURA")]
        public DateTime Abertura { get; set; }

        [Display(Name = "STATUS")]
        public string Status { get; set; }

        [Display(Name = "NÍVEL")]
        public string Nivel { get; set; }

        [Display(Name = "PERÍODO")]
        public string Periodo { get; set; }

        [Display(Name = "PORTARIA")]
        public string Portaria { get; set; }

        [Display(Name = "DATA INCLUSÃO")]
        public DateTime Inclusao { get; set; }

        [Display(Name = "MODELO DE HORÁRIO")]
        public int? CodHorario { get; set; }

        [Display(Name = "ALUNOS MATRICULADOS")]
        public int AlunosMatriculados { get; set; }

        [Display(Name = "DESCONTO POR ANTECIPAÇÃO")]
        [DataType(DataType.Currency)]
        public double? DescontoMensalidade { get; set; }

        public List<AlunoMatriculaExibicaoViewModel> AlunoMatriculaExibicaoViewModels { get; set; }
        public List<TurmaFuncionarioDisciplinaExibicaoViewModel> TurmaFuncionarioDisciplinas { get; set; }
        public int CodEscola { get; set; }
    }
}