using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class AdvertenciaTypeConfiguration : CeagEntityAbstractConfig<Advertencia>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodAdvertencia)
              .IsRequired()
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("COD_IN_ADVERTENCIA");

            Property(p => p.CodAlunoMatricula)
               .IsRequired()
               .HasColumnName("COD_ALUNO_MATRICULA");

            Property(p => p.CodAula)
              .IsOptional()
              .HasColumnName("COD_AULA");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");


            Property(p => p.Inclusao)
            .IsRequired()
            .HasColumnName("INCLUSAO_DT_ADVERTENCIA");

            Property(p => p.Comparecimento)
              .IsOptional()
              .HasColumnName("COMPARECIMENTO_DT_ADVERTENCIA");

            Property(p => p.Resolucao)
            .IsOptional()
            .HasColumnName("RESOLUCAO_DT_ADVERTENCIA");

            Property(p => p.Descricao)
             .IsRequired()
             .HasColumnName("DESCRICAO_VC_ADVERTENCIA")
             .HasMaxLength(800)
             .HasColumnType("varchar");

            Property(p => p.Motivo)
          .IsRequired()
          .HasColumnName("MOTIVO_VC_ADVERTENCIA")
          .HasMaxLength(100)
          .HasColumnType("varchar");

            Property(p => p.Titulo)
              .IsRequired()
              .HasColumnName("TITULO_VC_ADVERTENCIA")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.Tipo)
              .IsRequired()
              .HasColumnName("TIPO_VC_ADVERTENCIA")
              .HasMaxLength(100)
              .HasColumnType("varchar");

            Property(p => p.Urgencia)
             .IsRequired()
             .HasColumnName("URGENCIA_VC_ADVERTENCIA")
             .HasMaxLength(60)
             .HasColumnType("varchar");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.AlunoMatricula)
             .WithMany(p => p.Advertencias)
             .HasForeignKey(fk => fk.CodAlunoMatricula);

            HasOptional(p => p.Aula)
             .WithMany(p => p.Advertencias)
             .HasForeignKey(fk => fk.CodAula);
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodAdvertencia);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_ADVERTENCIA");
        }
    }
}
