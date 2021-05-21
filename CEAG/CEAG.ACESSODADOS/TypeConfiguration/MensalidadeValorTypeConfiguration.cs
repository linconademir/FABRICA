using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class MensalidadeValorTypeConfiguration : CeagEntityAbstractConfig<MensalidadeValor>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodMensalidadeValor)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("COD_IN_MENSALIDADE_VALOR");

            Property(p => p.Tipo)
              .HasColumnName("TIPO_VC_MENSALIDADE_VALOR")
              .HasMaxLength(60)
              .HasColumnType("varchar");


            Property(p => p.Turma)
              .HasColumnName("TURMA_VC_MENSALIDADE_VALOR")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.ValorDescontoAntecipacao)
              .HasColumnName("VALOR_ANTECIPACAO_MO_MENSALIDADE_VALOR");

            Property(p => p.ValorDescontoIrmao)
             .HasColumnName("VALOR_IRMAO_MO_MENSALIDADE_VALOR");

            Property(p => p.ValorMensal)
             .HasColumnName("VALOR_MENSAL_MO_MENSALIDADE_VALOR");

            Property(p => p.ValorComDesconto)
              .IsOptional()
              .HasColumnName("VALOR_DESCONTO_MENSAL_MO_MENSALIDADE_VALOR");

            Property(p => p.ValorAnualVista)
              .HasColumnName("VALOR_ANUAL_VISTA_MO_MENSALIDADE_VALOR");

            Property(p => p.ValorAnual)
              .HasColumnName("VALOR_ANUAL_MO_MENSALIDADE_VALOR");

            Property(p => p.ValorTaxaMaterial)
             .HasColumnName("VALOR_TAXA_MATERIAL_MO_MENSALIDADE_VALOR");

            Property(p => p.CodAcesso)
                .IsOptional()
                .HasColumnName("COD_IN_ACESSO");


            Property(p => p.CodEscola)
              .HasColumnName("COD_IN_ESCOLA");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
              .WithMany(p => p.MensalidadeValors)
              .HasForeignKey(fk => fk.CodEscola);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodMensalidadeValor);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_MENSALIDADE_VALOR");
        }
    }
}
