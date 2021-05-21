using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class DebitoHistorico
    {
        [Key]
        public int CodDebitoHistorico { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public DateTime Inclusao { get; set; }

        public int CodAcesso { get; set; }

        public int CodDebito { get; set; }

        public virtual Debito Debito { get; set; }

    }
}
