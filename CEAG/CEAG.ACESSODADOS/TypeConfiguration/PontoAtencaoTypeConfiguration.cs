using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class PontoAtencaoTypeConfiguration : CeagEntityAbstractConfig<PontoAtencao>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodPontoAtencao)
             .IsRequired()
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
             .HasColumnName("COD_IN_PONTO_ATENCAO");

            Property(p => p.CodReferencia)
             .IsRequired()
             .HasColumnName("REFERENCIA_IN_PONTO_ATENCAO");

            Property(p => p.CodAcesso)
             .IsRequired()
             .HasColumnName("COD_IN_ACESSO");

            Property(p => p.CodAcessoResolucao)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO_RESOLUCAO");

            Property(p => p.CodEscola)
             .IsRequired()
             .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.DescricaoPontoAtencao)
           .IsRequired()
           .HasColumnType("varchar")
            .HasMaxLength(500)
           .HasColumnName("DESCRICAO_VC_PONTO_ATENCAO");

            Property(p => p.Inclusao)
             .IsRequired()
             .HasColumnName("INCLUSAO_DT_PONTO_ATENCAO");

            Property(p => p.Resolucao)
             .IsOptional()
             .HasColumnName("RESOLUCAO_DT_PONTO_ATENCAO");

            Property(p => p.TipoPontoAtencao)
             .IsRequired()
             .HasColumnType("varchar")
            .HasMaxLength(60)
             .HasColumnName("TIPO_VC_PONTO_ATENCAO");
            
        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
               .WithMany(p => p.PontoAtencaos)
               .HasForeignKey(fk => fk.CodEscola);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodPontoAtencao);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_PONTO_ATENCAO");
        }
    }
}
