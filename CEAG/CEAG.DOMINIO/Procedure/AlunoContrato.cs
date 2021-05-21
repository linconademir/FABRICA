using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO.Procedure
{
    public class AlunoContrato
    {
        public string CPF_VC_ALUNO { get; set; }
        public string NOME_VC_ALUNO { get; set; }
        public DateTime NASCIMENTO_DT_ALUNO { get; set; }
        public string NACIONALIDADE_VC_ALUNO { get; set; }
        public string NATURALIDADE_VC_ALUNO { get; set; }
        public string DESCRICAO_VC_TURMA { get; set; }
        public string NIVEL_VC_TURMA { get; set; }
        public string SEGMENTO_VC_TURMA { get; set; }
        public string PERIODO_VC_TURMA { get; set; }
        public int ANO_LETIVO_IN_TURMA { get; set; }
        public double DESCONTO_MO_TURMA { get; set; }
        public string DESCONTO_MO_TURMA_EXTENSO { get; set; }
        public string IDENTIFICADOR_VC_ALUNO_MATRICULA { get; set; }

       

        public string ALUNO_TELEFONE { get; set; }
        public string RESPO_TELEFONE { get; set; }

        public string LOALUNO_RUA_VC_LOGRADOURO { get; set; }
        public string LOALUNO_NUMERO_VC_LOGRADOURO { get; set; }
        public string LOALUNO_CEP_VC_LOGRADOURO { get; set; }
        public string LOALUNO_COMPLEMENTO_VC_LOGRADOURO { get; set; }
        public string LOALUNO_REFERENCIA_VC_LOGRADOURO { get; set; }
        public string LOALUNO_UF_VC_LOGRADOURO { get; set; }
        public string LOALUNO_BAIRRO_VC_LOGRADOURO { get; set; }
        public string LOALUNO_MUNICIPIO_VC_LOGRADOURO { get; set; }
        public string LORESP_RUA_VC_LOGRADOURO { get; set; }
        public string LORESP_NUMERO_VC_LOGRADOURO { get; set; }
        public string LORESP_CEP_VC_LOGRADOURO { get; set; }
        public string LORESP_COMPLEMENTO_VC_LOGRADOURO { get; set; }
        public string LORESP_REFERENCIA_VC_LOGRADOURO { get; set; }
        public string LORESP_UF_VC_LOGRADOURO { get; set; }
        public string LORESP_BAIRRO_VC_LOGRADOURO { get; set; }
        public string LORESP_MUNICIPIO_VC_LOGRADOURO { get; set; }

        public string CPF_VC_RESPONSAVEL { get; set; }
        public string NOME_VC_RESPONSAVEL { get; set; }
        public string RG_VC_RESPONSAVEL { get; set; }
        public string RG_ORGAO_VC_RESPONSAVEL { get; set; }
        public string RG_UF_VC_RESPONSAVEL { get; set; }
        public string NATURALIDADE_VC_RESPONSAVEL { get; set; }
        public string ESTADO_CIVIL_VC_RESPONSAVEL { get; set; }
        public string PROFISSAO_VC_RESPONSAVEL { get; set; }
        public string NACIONALIDADE_VC_RESPONSAVEL { get; set; }

        public double DESCONTO_IRMAO { get; set; }
        public string DESCONTO_IRMAO_EXTENSO { get; set; }
        public string TURMA_VC_MENSALIDADE_VALOR { get; set; }

        public double VALOR_TAXA_MATERIAL_MO_MENSALIDADE_VALOR { get; set; }
        public double VALOR_MENSAL_MO_MENSALIDADE_VALOR { get; set; }
        public double VALOR_ANUAL_MO_MENSALIDADE_VALOR { get; set; }
        public double VALOR_DESCONTO_MENSAL_MO_MENSALIDADE_VALOR { get; set; }
        public double VALOR_ANUAL_VISTA_MO_MENSALIDADE_VALOR { get; set; }

        public string DESCRICAO_VC_TAXA { get; set; }
        public string TIPO_VC_TAXA { get; set; }
        public double VALOR_MO_TAXA { get; set; }
        public int VIAS_IN_TAXA { get; set; }

        public int COD_IN_MENSALIDADE_VALOR { get; set; }
        public int COD_IN_ALUNO { get; set; }
        public int COD_IN_ALUNO_MATRICULA { get; set; }
        public int COD_IN_TURMA { get; set; }
        public int COD_IN_TELEFONE { get; set; }
        public int COD_IN_LOGRADOURO { get; set; }
    }

}
