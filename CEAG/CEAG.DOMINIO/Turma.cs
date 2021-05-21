using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Turma
    {
        [Key]
        public int CodTurma { get; set; }
        public string Descricao { get; set; }
        public string Segmento { get; set; }
        public string Nivel { get; set; }
        public string Portaria { get; set; }
        public int Vagas { get; set; }
        public int AnoLetivo { get; set; }
        public DateTime? Fechamento { get; set; }
        public DateTime? Abertura { get; set; }
        public string Periodo { get; set; }
        public DateTime Inclusao { get; set; }
        public int? CodAcesso { get; set; }
        public int CodEscola { get; set; }
        public int? CodHorario { get; set; }
        public double? DescontoMensalidade { get; set; }

        public virtual Horario Horario { get; set; }
        public virtual Escola Escola { get; set; }
        public virtual List<AlunoMatricula> AlunoMatriculas { get; set; }
        public virtual List<TurmaFuncionarioDisciplina> TurmaFuncionarioDisciplinas { get; set; }
        public virtual List<Aula> Aulas { get; set; }

    }
}
