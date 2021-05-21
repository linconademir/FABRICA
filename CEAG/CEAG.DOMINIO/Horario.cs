using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Horario
    {
        [Key]
        public int CodHorario { get; set; }
        public string Descricao { get; set; }
        public int CodEscola { get; set; }
        public virtual Escola Escola { get; set; }
        public DateTime Inclusao { get; set; }
        public DateTime? Cancelamento { get; set; }
        public int? CodAcesso { get; set; }
        public virtual List<HorarioAula> HorarioAulas { get; set; }
        public virtual List<Turma> Turmas { get; set; }
    }
}
