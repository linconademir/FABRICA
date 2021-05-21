using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class InformacaoTypeConfiguration : CeagEntityAbstractConfig<Informacao>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodInformacao)
             .IsRequired()
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
             .HasColumnName("COD_IN_INFORMACAO");

            Property(p => p.Tipo)
              .IsRequired()
              .HasColumnName("TIPO_VC_INFORMACAO")
              .HasMaxLength(100)
              .HasColumnType("varchar");

            Property(p => p.Descricao)
              .IsRequired()
              .HasColumnName("DESCRICAO_VC_INFORMACAO")
              .HasMaxLength(300)
              .HasColumnType("varchar");

            Property(p => p.CodAluno)
              .HasColumnName("COD_IN_ALUNO");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Aluno)
               .WithMany(p => p.Informacaos)
               .HasForeignKey(fk => fk.CodAluno);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodInformacao);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_INFORMACAO");
        }
    }
}
