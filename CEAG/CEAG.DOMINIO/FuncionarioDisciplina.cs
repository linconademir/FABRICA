using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class FuncionarioDisciplina
    {
        [Key]
        public int CodFuncionarioDisciplina { get; set; }
        public int CodFuncionario { get; set; }
        public int CodDisciplina { get; set; }
        public DateTime Inclusao { get; set; }
        public DateTime? Cancelamento { get; set; }
        public string Observacao { get; set; }
        public int? CodAcesso { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual Disciplina Disciplina { get; set; }
        
    }
}
