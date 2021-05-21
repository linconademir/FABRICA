using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class AlunoMatriculaUnidade
    {
        [Key]
        public int CodAlunoMatriculaUnidade { get; set; }

        public double? NotaTeste { get; set; }
        public double? NotaProva { get; set; }
        public double? NotaPC { get; set; }
        public double? NotaRecuperacao { get; set; }
        public double? NotaConselhoClasse { get; set; }
        public DateTime? Fechamento { get; set; }

        public DateTime Inclusao { get; set; }
        public int NumeroUnidade { get; set; }

        public int? CodAcesso { get; set; }

        public int CodFuncionario { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public int CodDisciplina { get; set; }
        public virtual Disciplina Disciplina { get; set; }
        public int CodAlunoMatricula { get; set; }
        public virtual AlunoMatricula AlunoMatricula { get; set; }

    }
}
