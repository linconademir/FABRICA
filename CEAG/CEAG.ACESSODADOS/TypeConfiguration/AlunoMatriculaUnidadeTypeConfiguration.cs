using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class AlunoMatriculaUnidadeTypeConfiguration : CeagEntityAbstractConfig<AlunoMatriculaUnidade>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodAlunoMatriculaUnidade)
             .IsRequired()
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
             .HasColumnName("COD_IN_A_M_U");

            Property(p => p.CodAlunoMatricula)
             .IsRequired()
             .HasColumnName("COD_IN_ALUNO_MATRICULA");

            Property(p => p.CodDisciplina)
             .IsRequired()
             .HasColumnName("COD_IN_DISCIPLINA");

            Property(p => p.CodFuncionario)
             .IsRequired()
             .HasColumnName("COD_IN_FUNCIONARIO");

            Property(p => p.Inclusao)
                .HasColumnName("INCLUSAO_DT_A_M_U");

            Property(p => p.NotaTeste)
                .IsOptional()
                .HasColumnName("TESTE_MO_A_M_U");

            Property(p => p.NotaProva)
                .IsOptional()
                .HasColumnName("PROVA_MO_A_M_U");


            Property(p => p.NotaRecuperacao)
                .IsOptional()
                .HasColumnName("RECUPERACAO_MO_A_M_U");

            Property(p => p.NotaPC)
                .IsOptional()
                .HasColumnName("PC_MO_A_M_U");

            Property(p => p.NumeroUnidade)
                .IsRequired()
                .HasColumnName("UNDADE_IN_A_M_U");

            Property(p => p.NotaConselhoClasse)
                .IsOptional()
                .HasColumnName("CONSELHO_IN_A_M_U");

            Property(p => p.Fechamento)
                .IsOptional()
                .HasColumnName("FECHAMENTO_DT_A_M_U");

            Property(p => p.CodAcesso)
                .IsOptional()
                .HasColumnName("COD_IN_ACESSO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Funcionario)
              .WithMany(p => p.AlunoMatriculaUnidades)
              .HasForeignKey(fk => fk.CodFuncionario);

            HasRequired(p => p.Disciplina)
              .WithMany(p => p.AlunoMatriculaUnidades)
              .HasForeignKey(fk => fk.CodDisciplina);

            HasRequired(p => p.AlunoMatricula)
              .WithMany(p => p.AlunoMatriculaUnidades)
              .HasForeignKey(fk => fk.CodAlunoMatricula);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodAlunoMatriculaUnidade);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_ALUNO_MATRICULA_UNIDADE");
        }
    }
}
