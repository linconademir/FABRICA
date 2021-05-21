using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class TaxaTypeConfiguration : CeagEntityAbstractConfig<Taxa>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodTaxa)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("COD_IN_TAXA");

            Property(p => p.Tipo)
              .HasColumnName("TIPO_VC_TAXA")
              .HasMaxLength(60)
              .HasColumnType("varchar");

            Property(p => p.CodAcesso)
                .IsOptional()
                .HasColumnName("COD_IN_ACESSO");

            Property(p => p.ViasGratuitas)
                .HasColumnName("VIAS_IN_TAXA");


            Property(p => p.CodEscola)
              .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.Valor)
              .HasColumnName("VALOR_MO_TAXA");

            Property(p => p.Descricao)
              .HasColumnName("DESCRICAO_VC_TAXA")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.Cancelamento)
                .IsOptional()
                .HasColumnName("CANCELAMENTO_DT_TAXA");

            Property(p => p.Inclusao)
                .HasColumnName("INCLUSAO_DT_TAXA");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
              .WithMany(p => p.Taxas)
              .HasForeignKey(fk => fk.CodEscola);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodTaxa);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_TAXA");
        }
    }
}
