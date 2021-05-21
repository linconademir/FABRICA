using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class AulaAlunoTypeConfiguration : CeagEntityAbstractConfig<AulaAluno>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodAulaAluno)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .HasColumnName("COD_IN_AULA_ALUNO");

            Property(p => p.Observacao)
            .IsOptional()
            .HasColumnName("OBSERVACAO_VC_AULA_ALUNO")
            .HasMaxLength(500)
            .HasColumnType("varchar");

            Property(p => p.Situacao)
            .IsRequired()
            .HasColumnName("SITUACAO_VC_AULA_ALUNO")
            .HasMaxLength(20)
            .HasColumnType("varchar");

            Property(p => p.Inclusao)
            .IsRequired()
            .HasColumnName("INCLUSAO_DT_AULA_ALUNO");

           Property(p => p.CodAula)
            .HasColumnName("COD_IN_AULA");

           Property(p => p.CodAlunoMatricula)
             .HasColumnName("COD_IN_ALUNO_MATRICULA");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Aula)
               .WithMany(p => p.AulaAlunos)
               .HasForeignKey(fk => fk.CodAula);

            HasRequired(p => p.AlunoMatricula)
               .WithMany(p => p.AulaAlunos)
               .HasForeignKey(fk => fk.CodAlunoMatricula);


        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodAulaAluno);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_AULA_ALUNO");
        }
    }
}
