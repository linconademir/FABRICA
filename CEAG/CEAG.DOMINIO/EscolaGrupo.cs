using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class EscolaGrupo
    {
        public int CodEscolaGrupo { get; set; }
        public string Razao { get; set; }
        public string Fantasia { get; set; }
        public string Cnpj { get; set; }
        public string Logo { get; set; }
        public virtual List<Escola> Escolas { get; set; }
        public virtual List<Acesso> Acessos { get; set; }
    }
}
