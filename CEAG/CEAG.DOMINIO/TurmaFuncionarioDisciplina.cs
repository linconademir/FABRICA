using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class TurmaFuncionarioDisciplina
    {
        [Key]
        public int CodTurmaFuncionarioDisciplina { get; set; }
        public DateTime Inclusao { get; set; }
        public int? CargaHoraria { get; set; }
        public int CodTurma { get; set; }
        public int CodDisciplina { get; set; }
        public int CodFuncionario { get; set; }
        public int CodHorarioAula { get; set; }
        public int DiaSemana { get; set; }
        public int? CodAcesso { get; set; }
        public virtual Turma Turma { get; set; }
        public virtual Disciplina Disciplina { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual HorarioAula HorarioAula { get; set; }

    }
}
