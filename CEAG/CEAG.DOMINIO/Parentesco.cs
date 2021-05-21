using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Parentesco
    {
        [Key]
        public int CodParentesco { get; set; }
        public string Descricao { get; set; }
        public DateTime Inclusao { get; set; }
        public int CodAcesso { get; set; }
        public int? CodEscola { get; set; }

        public virtual Escola Escola { get; set; }

        public virtual List<ParentescoAluno> ParentescoAlunos { get; set; }

    }
}
