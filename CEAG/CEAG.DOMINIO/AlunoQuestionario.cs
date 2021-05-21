using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class AlunoQuestionario
    {
        [Key]
        public int CodAlunoQuestionario { get; set; }
        public int CodAluno { get; set; }
        public int CodQuestionario { get; set; }
        public string Resposta { get; set; }
        public string Complemento { get; set; }
        public DateTime Inclusao { get; set; }
        public int? CodAcesso { get; set; }
        public virtual Aluno Aluno{ get; set; }
        public virtual Questionario Questionario { get; set; }    
    }
}
