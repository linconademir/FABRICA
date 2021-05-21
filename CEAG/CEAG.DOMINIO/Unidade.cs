using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Unidade
    {
        [Key]
        public int CodUnidade { get; set; }
        public int CodEscola { get; set; }
        public int Ano { get; set; }
        public int Numero { get; set; }

        public DateTime Abertura { get; set; }
        public DateTime Inclusao { get; set; }
        public DateTime? Fechamento { get; set; }
        public int? CodAcesso { get; set; }
        public virtual Escola Escola { get; set; }

    }
}
