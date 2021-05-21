using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class TurmaTypeConfiguration : CeagEntityAbstractConfig<Turma>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodTurma)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("COD_IN_TURMA");

            Property(p => p.CodEscola)
                .HasColumnName("COD_IN_ESCOLA");


            Property(p => p.CodAcesso)
           .IsOptional()
           .HasColumnName("COD_IN_ACESSO");

            Property(p => p.Descricao)
                .IsRequired()
                .HasColumnName("DESCRICAO_VC_TURMA")
                .HasMaxLength(160)
                .HasColumnType("varchar");

            Property(p => p.Nivel)
               .IsRequired()
               .HasColumnName("NIVEL_VC_TURMA")
               .HasMaxLength(100)
               .HasColumnType("varchar");

            Property(p => p.Abertura)
                .IsOptional()
              .HasColumnName("ABERTURA_DT_TURMA");

            Property(p => p.CodHorario)
               .IsOptional()
             .HasColumnName("COD_IN_HORARIO");

            Property(p => p.Fechamento)
                .IsOptional()
              .HasColumnName("FECHAMENTO_DT_TURMA");

            Property(p => p.AnoLetivo)
              .IsRequired()
              .HasColumnName("ANO_LETIVO_IN_TURMA");

            Property(p => p.Vagas)
              .IsRequired()
              .HasColumnName("VAGAS_IN_TURMA");

            Property(p => p.Periodo)
            .HasColumnName("PERIODO_VC_TURMA")
            .HasMaxLength(100)
            .HasColumnType("varchar");

            Property(p => p.DescontoMensalidade)
                .IsOptional()
             .HasColumnName("DESCONTO_MO_TURMA");


            Property(p => p.Inclusao)
                .HasColumnName("INCLUSAO_DT_TURMA");

            Property(p => p.Portaria)
                .HasMaxLength(160)
                .HasColumnName("PORTARIA_VC_TURMA")
                .HasColumnType("varchar");

            Property(p => p.Segmento)
                .HasMaxLength(160)
                .HasColumnName("SEGMENTO_VC_TURMA")
                .HasColumnType("varchar");


        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
                .WithMany(p => p.Turma)
                .HasForeignKey(fk => fk.CodEscola);

            HasRequired(p => p.Horario)
                .WithMany(p => p.Turmas)
                .HasForeignKey(fk => fk.CodHorario);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodTurma);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_TURMA");
        }
    }
}
