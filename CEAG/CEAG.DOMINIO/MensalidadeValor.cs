using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class MensalidadeValor
    {
        [Key]
        public int CodMensalidadeValor { get; set; }
        public string Turma { get; set; }
        public double ValorMensal { get; set; }
        public double? ValorComDesconto { get; set; }

        public double ValorDescontoIrmao { get; set; }

        public double ValorAnual { get; set; }
        public double ValorAnualVista { get; set; }
        public double ValorDescontoAntecipacao { get; set; }
        public string Tipo { get; set; }
        public int? CodAcesso { get; set; }
        public int CodEscola { get; set; }
        public virtual Escola Escola { get; set; }
        public double ValorTaxaMaterial { get; set; }


    }
}
