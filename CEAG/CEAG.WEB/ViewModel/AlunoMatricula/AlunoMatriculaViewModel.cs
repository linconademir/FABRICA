using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.Turma;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.AlunoMatricula
{
    public class AlunoMatriculaViewModel
    {
        public int CodAlunoMatricula { get; set; }
        public DateTime Inclusao { get; set; }
        public int CodAluno { get; set; }
        public AlunoViewModel Aluno{ get; set; }
        public int CodTurma { get; set; }
        public TurmaViewModel Turma { get; set; }

        public string IdenficadorHexa { get; set; }

        public int CodNovaTurma { get; set; }

        public string DescontoMensalidade { get; set; }

        [Display(Name = "BOLSISTA")]
        public string Bolsista { get; set; }

        [Display(Name = "ANO")]
        public int Ano { get; set; }

        public string Status { get; set; }

        [Display(Name = "VALOR DA MATRICULA")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O valor da matricula é obrigatório")]
        public double ValorMatricula { get; set; }

        [Display(Name = "VALOR DA MENSALIDADE")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O valor da mensalidade é obrigatório")]
        public double ValorMensalidade { get; set; }

        [Display(Name = "TAXA DE MATERIAL")]
        [DataType(DataType.Currency)]
        public double ValorTaxaMaterial { get; set; }

        [Display(Name = "QUANTIDADE DE PARCELAS")]
        public int ParcelasMensalidade { get; set; }

        [Display(Name = "FORMA DE PAGAMENTO")]
        public string FormaPagamento { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        public string Observação { get; set; }

    }
}