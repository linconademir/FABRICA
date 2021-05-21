using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace CEAG.DOMINIO
{
    public class Debito
    {
        [Key]
        public int CodDebito { get; set; }
        public DateTime Inclusao { get; set; }
        public DateTime? Pagamento { get; set; }
        public DateTime Periodo { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime? Negociacao { get; set; }
        public string TipoDebito { get; set; }

        public string Responsavel { get; set; }
        public string IdenficadorHexa { get; set; }
        public string QrCode { get; set; }
        public double ValorDebito { get; set; }
        public double? ValorPago { get; set; }
        public double? Desconto { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public int? NumeroDebito { get; set; }

        public string FormaPagamento { get; set; }
        public int? CodAcesso { get; set; }

        public int CodAlunoMatricula { get; set; }
        public virtual AlunoMatricula AlunoMatricula { get; set; }

        public virtual List<DebitoHistorico> DebitoHistoricos { get; set; }
    }
}
