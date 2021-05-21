using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Advertencia
    {
        [Key]
        public int CodAdvertencia { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Urgencia { get; set; }
        public string Motivo { get; set; }

        public DateTime Inclusao { get; set; }
        public DateTime Comparecimento { get; set; }

        public int CodAcesso { get; set; }
        public DateTime? Resolucao { get; set; }

        public int CodAlunoMatricula { get; set; }
        public virtual AlunoMatricula AlunoMatricula { get; set; }

        public int? CodAula { get; set; }
        public virtual Aula Aula { get; set; }

    }
}
