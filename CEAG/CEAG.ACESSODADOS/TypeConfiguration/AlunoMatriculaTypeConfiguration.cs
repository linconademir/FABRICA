using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class AlunoMatriculaTypeConfiguration : CeagEntityAbstractConfig<AlunoMatricula>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodAlunoMatricula)
                   .IsRequired()
                   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                   .HasColumnName("COD_IN_ALUNO_MATRICULA");

            Property(p => p.CodAluno)
                 .HasColumnName("COD_IN_ALUNO");

            Property(p => p.CodTurma)
                 .HasColumnName("COD_IN_TURMA");

            Property(p => p.Ano)
                 .HasColumnName("ANO_IN_ALUNO_MATRICULA");

            Property(p => p.Inclusao)
                 .HasColumnName("INCLUSAO_DT_ALUNO_MATRICULA");

            Property(p => p.Status)
                 .HasMaxLength(60)
                 .HasColumnType("varchar")
                 .HasColumnName("STATUS_VC_ALUNO_MATRICULA");

            Property(p => p.IdenficadorHexa)
                 .HasMaxLength(100)
                 .HasColumnType("varchar")
                 .HasColumnName("IDENTIFICADOR_VC_ALUNO_MATRICULA");

            Property(p => p.DescontoMensalidade)
               .HasMaxLength(3)
               .HasColumnType("varchar")
               .HasColumnName("DESCONTO_MENSALIDADE_VC_ALUNO_MATRICULA");

            Property(p => p.Bolsista)
               .HasMaxLength(15)
               .HasColumnType("varchar")
               .HasColumnName("BOLSISTA_VC_ALUNO_MATRICULA");


            Property(p => p.NotaFinal)
                 .HasColumnName("FINAL_MO_VC_ALUNO_MATRICULA");

            Property(p => p.ValorMatricula)
                .HasColumnName("MATRICULA_MO_ALUNO_MATRICULA");

            Property(p => p.ValorMensalidade)
                .HasColumnName("MENSALIDADE_MO_ALUNO_MATRICULA");

            Property(p => p.ValorTaxaMaterial)
                .HasColumnName("TAXA_MATERIAL_MO_ALUNO_MATRICULA");

            Property(p => p.Observação)
                .HasMaxLength(200)
                .HasColumnType("varchar")
                .HasColumnName("OBS_VC_ALUNO_MATRICULA");

            Property(p => p.FormaPagamento)
                .HasMaxLength(100)
                .HasColumnType("varchar")
                .HasColumnName("FORMA_PAGAMENTO_VC_ALUNO_MATRICULA");

            Property(p => p.ParcelasMensalidade)
                .HasColumnName("PARCELAS_IN_ALUNO_MATRICULA");

            Property(p => p.CodAcesso)
                .IsOptional()
                .HasColumnName("COD_IN_ACESSO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Aluno)
               .WithMany(p => p.AlunoMatriculas)
               .HasForeignKey(fk => fk.CodAluno);

            HasRequired(p => p.Turma)
                .WithMany(p => p.AlunoMatriculas)
                .HasForeignKey(fk => fk.CodTurma);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodAlunoMatricula);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_ALUNO_MATRICULA");
        }
    }
}
