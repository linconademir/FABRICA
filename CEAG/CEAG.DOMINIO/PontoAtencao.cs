using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class PontoAtencao
    {
        [Key]
        public int CodPontoAtencao { get; set; }
        public string TipoPontoAtencao { get; set; }
        public string DescricaoPontoAtencao { get; set; }
        public int CodReferencia { get; set; }
        public DateTime? Resolucao { get; set; }
        public DateTime Inclusao { get; set; }
        public int CodEscola { get; set; }
        public virtual Escola Escola { get; set; }
        public int CodAcesso { get; set; }
        public int? CodAcessoResolucao { get; set; }


    }
}
