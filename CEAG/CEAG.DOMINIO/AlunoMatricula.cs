using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class AlunoMatricula
    {
        [Key]
        public int CodAlunoMatricula { get; set; }
        public DateTime Inclusao { get; set; }
        public int CodAluno { get; set; }
        public int CodTurma { get; set; }
        public int Ano { get; set; }
        public string Status { get; set; }
        public double ValorMatricula { get; set; }
        public double ValorMensalidade { get; set; }
        public double ValorTaxaMaterial { get; set; }
        public int ParcelasMensalidade { get; set; }
        public string FormaPagamento { get; set; }
        public string Observação { get; set; }
        public double? NotaFinal { get; set; }
        public int? CodAcesso { get; set; }
        public string DescontoMensalidade { get; set; }
        public string Bolsista { get; set; }
        public string IdenficadorHexa { get; set; }


        public virtual Aluno Aluno { get; set; }
        public virtual Turma Turma { get; set; }
        public virtual List<AlunoMatriculaUnidade> AlunoMatriculaUnidades { get; set; }
        public virtual List<AulaAluno> AulaAlunos { get; set; }
        public virtual List<Debito> Debitos { get; set; }
        public virtual List<Mensagem> Mensagems { get; set; }
        public virtual List<Advertencia> Advertencias { get; set; }

    }
}
