using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class AulaAluno
    {
        [Key]
        public int CodAulaAluno { get; set; }
        public DateTime Inclusao { get; set; }
        public string Situacao { get; set; }
        public string Observacao { get; set; }
        public int CodAlunoMatricula { get; set; }
        public int? CodAcesso { get; set; }
        public virtual AlunoMatricula AlunoMatricula { get; set; }
        public int CodAula{ get; set; }
        public virtual Aula Aula { get; set; }

    }
}
