using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Mensagem
    {
        [Key]
        public int CodMensagem { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime Inclusao { get; set; }
        public DateTime? Envio { get; set; }
        public string Destinatario { get; set; }
        public string DestinatarioCopia { get; set; }
        public string Tipo { get; set; }
        public int Prioridade { get; set; }

        public int? CodAlunoMatricula { get; set; }
        public virtual AlunoMatricula AlunoMatricula { get; set; }
    }
}
