using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Role
    {
        [Key]
        public int CodRole { get; set; }
        public string Descricao { get; set; }
        public virtual List<AcessoRole> AcessoRoles { get; set; }

    }
}
