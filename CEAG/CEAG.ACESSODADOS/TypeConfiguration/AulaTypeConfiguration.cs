using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class AulaTypeConfiguration : CeagEntityAbstractConfig<Aula>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodAula)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .HasColumnName("COD_IN_AULA");

            Property(p => p.Assunto)
            .IsOptional()
            .HasColumnName("ASSUNTO_VC_AULA")
            .HasMaxLength(500)
            .HasColumnType("varchar");

            Property(p => p.Cancelamento)
            .IsOptional()
            .HasColumnName("CANCELAMENTO_DT_AULA");

            Property(p => p.Inclusao)
            .IsRequired()
            .HasColumnName("INCLUSAO_DT_AULA");

            Property(p => p.Realizada)
            .IsRequired()
            .HasColumnName("REALIZADA_DT_AULA");

            Property(p => p.Observacao)
            .IsOptional()
            .HasColumnName("OBSERVACAO_VC_AULA")
            .HasMaxLength(500)
            .HasColumnType("varchar");

            Property(p => p.CodDisciplina)
            .HasColumnName("COD_IN_DISCIPLINA");

            Property(p => p.CodTurma)
            .HasColumnName("COD_IN_TURMA");

            Property(p => p.CodHorarioAula)
            .HasColumnName("COD_IN_HORARIO_AULA");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Turma)
               .WithMany(p => p.Aulas)
               .HasForeignKey(fk => fk.CodTurma);

            HasRequired(p => p.Disciplina)
               .WithMany(p => p.Aulas)
               .HasForeignKey(fk => fk.CodDisciplina);

            HasRequired(p => p.HorarioAula)
               .WithMany(p => p.Aulas)
               .HasForeignKey(fk => fk.CodHorarioAula);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodAula);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_AULA");
        }
    }
}
