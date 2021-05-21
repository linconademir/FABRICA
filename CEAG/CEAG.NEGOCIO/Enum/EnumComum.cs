using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.NEGOCIO.Enum
{
    public class EnumComum
    {
        public enum Chamada
        {
            PRESENTE,
            AUSENTE
        };

        public enum TipoDebito
        {
            MENSALIDADE,
            MATERIAL,
            MATRICULA,
            RENEGOCIAÇÃO,
            FARDAMENTO,
            LIVRO,
            DOCUMENTAL,
            AVULSO
        };

        public enum FormaPagamento
        {
            BOLETO,
            CARNÊ,
            DINHEIRO,
            CRÉDITO,
            DÉBITO,
            CHEQUE,
            TRANSFERÊNCIA
        }

        public enum TiposAdvertencia
        {
            VERBAL,
            ESCRITA
        }

        public enum TiposUrgenciaAdvertencia
        {
            ALTA,
            MEDIA,
            BAIXA,
            COMUM
        }

        public enum MotivoAdvertencia
        {
            AUSENCIA,
            ATENÇÃO,
            MATERIAL,
            FARDAMENTO,
            AGRESSÃO,
            COMPORTAMENTO,
            DESEMPENHO
        }

        public enum TipoParentesco
        {
            MÃE,
            PAI,
            IRMÃ,
            IRMÃO,
            PRIMA,
            PRIMO,
            TIA,
            TIO,
            AVÓ,
            AVÔ,
            FILHA,
            FILHO,
            ENTEADA,
            ENTEADO
        }

        public enum TipoLocalTelefone
        {
            CASA,
            TRABALHO,
            RECADO,
            VIZINHO,
            FAMILAR,
            OUTRO
        }

        public enum TipoTelefone
        {
            CELULAR,
            FIXO
        }

        public enum TipoContato
        {
            NORMAL,
            EMERGÊNCIA,
            RECADO
        }

        public enum TipoPessoaContato
        {
            MÃE,
            PAI,
            TIA,
            TIO,
            AVÓ,
            AVÔ,
            MADRINHA,
            PADRINHO,
            VIZINHO,
            FAMILIAR,
            TRANSPORTADOR,
            CONDUÇÃO,
            OUTRO
        }

        public enum Sexo
        {
            MASCULINO,
            FEMININO
        }

        
        public enum SimNao
        {
            SIM,
            NÃO
        }

        public enum Bolsista
        {
            EDUCANDO,
            SIM,
            NÃO
        }

        public enum TipoResponsavel
        {
            FINANCEIRO,
            TRANSPORTE,
            LEGAL
        }

        public enum TipoFeriado
        {
            NACIONAL,
            ESTADUAL,
            MUNICIPAL,
            RECESSO,
            OUTRO
        }

        public enum Titulacao
        {
            GRADUAÇÃO,
            ESPECIALIZAÇÃO,
            MESTRADO,
            DOUTORADO,
            OUTRO
        }

        public enum Turno
        {
            MANHÃ,
            TARDE,
            NOITE
        }

        public enum TipoTaxa
        {
            DOCUMENTAL,
            DEPREDAÇÃO
        }
    }
}
