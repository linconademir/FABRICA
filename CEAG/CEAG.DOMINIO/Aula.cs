using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Aula
    {
        [Key]
        public int CodAula { get; set; }
        public DateTime Realizada { get; set; }
        public DateTime Inclusao { get; set; }
        public DateTime? Cancelamento { get; set; }
        public string Observacao { get; set; }
        public string Assunto { get; set; }
        public int? CodAcesso { get; set; }
        public int CodHorarioAula { get; set; }
        public virtual HorarioAula HorarioAula { get; set; }
        public int CodTurma { get; set; }
        public virtual Turma Turma { get; set; }
        public int CodDisciplina { get; set; }
        public virtual Disciplina Disciplina { get; set; }
        public virtual List<AulaAluno> AulaAlunos { get; set; }
        public virtual List<Advertencia> Advertencias { get; set; }
    }
}
