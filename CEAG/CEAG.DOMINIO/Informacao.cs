using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Informacao
    {
        [Key]
        public int CodInformacao { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }

        public int CodAluno { get; set; }
        public virtual Aluno Aluno { get; set; }
        public int? CodAcesso { get; set; }
    }
}
