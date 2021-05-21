using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class AcessoRole
    {
        [Key]
        public int CodAcessoRole { get; set; }
        public int CodRole { get; set; }
        public int CodAcesso { get; set; }
        public virtual Role Role { get; set; }
        public virtual Acesso Acesso { get; set; }

    }
}
