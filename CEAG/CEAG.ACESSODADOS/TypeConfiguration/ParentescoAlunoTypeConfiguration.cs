using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class ParentescoAlunoTypeConfiguration : CeagEntityAbstractConfig<ParentescoAluno>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodParentescoAluno)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .HasColumnName("COD_IN_PARENTESCO_ALUNO");

            Property(p => p.Inclusao)
            .IsRequired()
            .HasColumnName("INCLUSAO_DT_PARENTESCO_ALUNO");

            Property(p => p.CodParentesco)
            .HasColumnName("COD_IN_PARENTESCO");

            Property(p => p.CodAluno)
            .HasColumnName("COD_IN_ALUNO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasOptional(p => p.Parentesco)
              .WithMany(p => p.ParentescoAlunos)
              .HasForeignKey(fk => fk.CodParentesco);

            HasOptional(p => p.Aluno)
              .WithMany(p => p.ParentescoAlunos)
              .HasForeignKey(fk => fk.CodAluno);
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodParentescoAluno);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_PARENTESCO_ALUNO");
        }
    }
}
