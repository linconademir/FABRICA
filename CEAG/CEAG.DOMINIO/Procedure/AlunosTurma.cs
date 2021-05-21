using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO.Procedure
{
    public class AlunosTurma
    {
        public int COD_IN_TURMA { get; set; }
        public string DESCRICAO_VC_TURMA { get; set; }
        public int COD_IN_ALUNO { get; set; }
        public string NOME_VC_ALUNO { get; set; }
        public string MATRICULA_VC_ALUNO { get; set; }
        public string OBS { get; set; }
        public int ANO_LETIVO_IN_TURMA { get; set; }
        public string PERIODO_VC_TURMA { get; set; }
        public double MENSALIDADE_MO_ALUNO_MATRICULA { get; set; }
    }
}
