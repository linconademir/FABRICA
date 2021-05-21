using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class ParentescoTypeConfiguration : CeagEntityAbstractConfig<Parentesco>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodParentesco)
             .IsRequired()
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
             .HasColumnName("COD_IN_PARENTESCO");

            Property(p => p.Descricao)
              .IsRequired()
              .HasColumnName("DESCRICAO_VC_PARENTESCO")
              .HasMaxLength(100)
              .HasColumnType("varchar");

            Property(p => p.Inclusao)
             .IsRequired()
             .HasColumnName("INCLUSAO_DT_PARENTESCO");

            Property(p => p.CodAcesso)
             .IsRequired()
             .HasColumnName("COD_IN_ACESSO");

            Property(p => p.CodEscola)
             .HasColumnName("COD_IN_ESCOLA");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasOptional(p => p.Escola)
              .WithMany(p => p.Parentescos)
              .HasForeignKey(fk => fk.CodEscola);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodParentesco);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_PARENTESCO");
        }
    }
}
