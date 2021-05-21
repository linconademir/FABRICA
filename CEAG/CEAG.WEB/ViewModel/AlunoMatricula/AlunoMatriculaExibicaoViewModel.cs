using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.Turma;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.AlunoMatricula
{
    public class AlunoMatriculaExibicaoViewModel
    {
        public int CodAlunoMatricula { get; set; }
        public DateTime Inclusao { get; set; }
        public int CodAluno { get; set; }
        public int CodTurma { get; set; }

        public string IdenficadorHexa { get; set; }

        public string DescontoMensalidade { get; set; }

        [Display(Name = "BOLSISTA")]
        public string Bolsista { get; set; }

        [Display(Name = "ANO")]
        public int Ano { get; set; }

        public string Status { get; set; }

        [Display(Name = "VALOR DA MATRICULA")]
        public double ValorMatricula { get; set; }

        [Display(Name = "VALOR DA MENSALIDADE")]
        public double ValorMensalidade { get; set; }

        [Display(Name = "TAXA DE MATERIAL")]
        public double ValorTaxaMaterial { get; set; }

        [Display(Name = "QUANTIDADE DE PARCELAS")]
        public int ParcelasMensalidade { get; set; }

        [Display(Name = "FORMA DE PAGAMENTO")]
        public string FormaPagamento { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        public string Observação { get; set; }

        public AlunoExibicaoViewModel Aluno { get; set; }

        public TurmaExibicaoViewModel Turma { get; set; }


    }
}