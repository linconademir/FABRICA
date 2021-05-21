using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class AlunoQuestionarioTypeConfiguration : CeagEntityAbstractConfig<AlunoQuestionario>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodAlunoQuestionario)
             .IsRequired()
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
             .HasColumnName("COD_IN_ALUNO_QUESTIONARIO");

            Property(p => p.CodAluno)
             .IsRequired()
             .HasColumnName("COD_IN_ALUNO");

            Property(p => p.CodQuestionario)
             .IsRequired()
             .HasColumnName("COD_IN_QUESTIONARIO");

            Property(p => p.Resposta)
             .IsRequired()
             .HasColumnName("RESPOSTA_VC_ALUNO_QUESTIONARIO")
             .HasMaxLength(3)
             .HasColumnType("varchar");

            Property(p => p.Complemento)
             .HasColumnName("COMPLEMENTO_VC_ALUNO_QUESTIONARIO")
             .HasMaxLength(200)
             .HasColumnType("varchar");

            Property(p => p.Inclusao)
             .IsRequired()
             .HasColumnName("INCLUSAO_DT_ALUNO_QUESTIONARIO");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Aluno)
               .WithMany(p => p.AlunoQuestionarios)
               .HasForeignKey(fk => fk.CodAluno);

            HasRequired(p => p.Questionario)
               .WithMany(p => p.AlunoQuestionarios)
               .HasForeignKey(fk => fk.CodQuestionario);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodAlunoQuestionario);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_ALUNO_QUESTIONARIO");
        }
    }
}
