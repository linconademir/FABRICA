using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class ParentescoAluno
    {
        [Key]
        public int CodParentescoAluno { get; set; }
        public int? CodParentesco { get; set; }
        public int? CodAluno { get; set; }
        public DateTime Inclusao { get; set; }
        public virtual Aluno Aluno { get; set; }
        public virtual Parentesco Parentesco { get; set; }

    }
}
