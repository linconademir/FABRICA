using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class HorarioAula
    {
        [Key]
        public int CodHorarioAula { get; set; }
        public string Turno { get; set; }
        public TimeSpan Inicio { get; set; }
        public TimeSpan Termino { get; set; }
        public int DiaSemana { get; set; }
        public int CodHorario { get; set; }
        public int? CodAcesso { get; set; }
        public virtual Horario Horario { get; set; }
        public virtual List<TurmaFuncionarioDisciplina> TurmaFuncionarioDisciplinas { get; set; }
        public virtual List<Aula> Aulas { get; set; }

    }
}
