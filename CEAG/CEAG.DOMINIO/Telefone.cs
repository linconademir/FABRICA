using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Telefone
    {
        [Key]
        public int CodTelefone { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public string Ddd { get; set; }
        public string Local { get; set; }
        public string TipoContato { get; set; }
        public string Pessoa { get; set; }
        public string Observacao { get; set; }
        public DateTime Inclusao { get; set; }
        public int? CodAluno { get; set; }
        public int? CodResponsavel { get; set; }
        public int? CodFuncionario { get; set; }
        public int? CodAcesso { get; set; }
        public virtual Aluno Aluno { get; set; }
        public virtual Responsavel Responsavel { get; set; }
        public virtual Funcionario Funcionario { get; set; }

    }
}
