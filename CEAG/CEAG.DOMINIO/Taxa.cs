using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Taxa
    {
        [Key]
        public int CodTaxa { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public string Tipo { get; set; }
        public int ViasGratuitas { get; set; }
        public int? CodAcesso { get; set; }
        public int CodEscola { get; set; }
        public DateTime? Cancelamento { get; set; }
        public DateTime Inclusao { get; set; }

        public virtual Escola Escola { get; set; }

    }
}
