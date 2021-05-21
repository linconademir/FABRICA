using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Questionario
    {
        [Key]
        public int CodQuestionario { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string TemComplemento { get; set; }
        public string PerguntaComplemento { get; set; }

        public int Ordem { get; set; }
        public int? CodAcesso { get; set; }
        public int CodEscola { get; set; }

        public virtual List<AlunoQuestionario> AlunoQuestionarios { get; set; }
    }
}
