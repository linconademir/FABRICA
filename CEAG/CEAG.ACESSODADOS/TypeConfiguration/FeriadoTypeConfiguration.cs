using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class FeriadoTypeConfiguration : CeagEntityAbstractConfig<Feriado>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodFeriado)
                   .IsRequired()
                   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                   .HasColumnName("COD_IN_FERIADO");

            Property(p => p.Tipo)
              .HasColumnName("TIPO_VC_FERIADO")
              .HasMaxLength(60)
              .HasColumnType("varchar");

            Property(p => p.Descricao)
              .HasColumnName("DESCRICAO_VC_FERIADO")
              .HasMaxLength(160)
              .HasColumnType("varchar");

            Property(p => p.CodEscola)
              .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.Data)
              .HasColumnName("DATA_DT_FERIADO");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
              .WithMany(p => p.Feriados)
              .HasForeignKey(fk => fk.CodEscola);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodFeriado);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_FERIADO");
        }
    }
}
