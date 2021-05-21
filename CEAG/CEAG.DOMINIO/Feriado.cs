using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Feriado
    {
        [Key]
        public int CodFeriado { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int? CodAcesso { get; set; }
        public int CodEscola { get; set; }
        public virtual Escola Escola { get; set; }

    }
}
