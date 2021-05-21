using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO.Procedure
{
    public class AlunoReciboMensal
    {
        public string CNPJ_VC_ESCOLA { get; set; }
        public string FANTASIA_VC_ESCOLA { get; set; }
        public string RAZAO_VC_ESCOLA { get; set; }
        public string INSCRICAO_VC_ESCOLA { get; set; }
        public double PAGO_MO_DEBITO { get; set; }
        public double VALOR_MO_DEBITO { get; set; }
        public string ANO_DEBITO { get; set; }
        public string FORMA_PAGAMENTO_VC_DEBITO { get; set; }
        public string IDENTIFICADOR_VC_DEBITO { get; set; }
        public DateTime PAGAMENTO_DT_DEBITO { get; set; }
        public double DESCONTO_MO_DEBITO { get; set; }
        public string OBSERVACAO_VC_DEBITO { get; set; }
        public string DESCRICAO_VC_DEBITO { get; set; }
        public string TIPO_VC_DEBITO { get; set; }
        public string NUMERO_IN_DEBITO { get; set; }
        public DateTime PERIODO_DT_DEBITO { get; set; }
        public string NOME_VC_ALUNO { get; set; }
        public string SEXO_VC_ALUNO { get; set; }
        public string DESCRICAO_VC_TURMA { get; set; }
        public string NIVEL_VC_TURMA { get; set; }
        public string PERIODO_VC_TURMA { get; set; }
        public string SEGMENTO_VC_TURMA { get; set; }
        public string NOME_VC_FUNCIONARIO { get; set; }
        public string QRCOD { get; set; }
    }
}
