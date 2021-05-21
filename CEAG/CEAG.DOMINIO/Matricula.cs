using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Matricula
    {
        [Key]
        public int CodMatricula { get; set; }
        public string NumeroMatricula { get; set; }
        public int Ano { get; set; }
        public string Status { get; set; }
        public DateTime Inclusao { get; set; }
        public int Ordem { get; set; }
        public int? CodAcesso { get; set; }
        public int CodEscola { get; set; }
        public virtual Escola Escola { get; set; }
    }
}
