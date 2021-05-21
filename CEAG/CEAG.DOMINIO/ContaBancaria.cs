using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class ContaBancaria
    {
        [Key]
        public int CodContaBancaria { get; set; }
        public string Conta { get; set; }
        public string DigitoConta { get; set; }
        public string Agencia { get; set; }
        public string DigitoAgencia { get; set; }
        public string Banco { get; set; }
        public string Descricao { get; set; }
        public DateTime Inclusao { get; set; }
        public int? CodAcesso { get; set; }
        public int? CodEscola { get; set; }

        public virtual Escola Escola { get; set; }

    }
}
