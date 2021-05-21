using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class DebitoHistoricoTypeConfiguration : CeagEntityAbstractConfig<DebitoHistorico>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodDebitoHistorico)
              .IsRequired()
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("COD_IN_DEBITO_HISTORICO");

            Property(p => p.Descricao)
              .IsRequired()
              .HasColumnName("DESCRICAO_VC_DEBITO_HISTORICO")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.CodAcesso)
              .IsRequired()
              .HasColumnName("COD_IN_ACESSO");

            Property(p => p.CodDebito)
              .IsRequired()
              .HasColumnName("COD_IN_DEBITO");

            Property(p => p.Observacao)
            .HasMaxLength(400)
            .HasColumnType("varchar")
            .HasColumnName("OBSERVACAO_VC_DEBITO_HISTORICO");

            Property(p => p.Inclusao)
              .IsRequired()
              .HasColumnName("INCLUSAO_DT_DEBITO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Debito)
          .WithMany(p => p.DebitoHistoricos)
          .HasForeignKey(fk => fk.CodDebito);
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodDebitoHistorico);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_DEBITO_HISTORICO");
        }
    }
}
