using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class TurmaFuncionarioDisciplinaTypeConfiguration : CeagEntityAbstractConfig<TurmaFuncionarioDisciplina>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodTurmaFuncionarioDisciplina)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .HasColumnName("COD_IN_T_F_D");

            Property(p => p.CodTurma)
            .HasColumnName("COD_IN_TURMA");

            Property(p => p.CodFuncionario)
           .HasColumnName("COD_IN_FUNCIONARIO");

            Property(p => p.CodAcesso)
              .IsOptional()
              .HasColumnName("COD_IN_ACESSO");

            Property(p => p.CodHorarioAula)
           .HasColumnName("COD_IN_HORARIO_AULA");

            Property(p => p.CodDisciplina)
          .HasColumnName("COD_IN_DISCIPLINA");

            Property(p => p.DiaSemana)
         .HasColumnName("DIA_SEMANA_IN_T_F_D");

            Property(p => p.CargaHoraria)
           .IsOptional()
           .HasColumnName("CARGA_HORARIA_IN_T_F_D");

            Property(p => p.Inclusao)
           .HasColumnName("INCLUSAO_DT_T_F_D");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Turma)
                .WithMany(p => p.TurmaFuncionarioDisciplinas)
                .HasForeignKey(fk => fk.CodTurma);

            HasRequired(p => p.Funcionario)
                .WithMany(p => p.TurmaFuncionarioDisciplinas)
                .HasForeignKey(fk => fk.CodFuncionario);

            HasRequired(p => p.Disciplina)
                .WithMany(p => p.TurmaFuncionarioDisciplinas)
                .HasForeignKey(fk => fk.CodDisciplina);

            HasRequired(p => p.HorarioAula)
              .WithMany(p => p.TurmaFuncionarioDisciplinas)
              .HasForeignKey(fk => fk.CodHorarioAula);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodTurmaFuncionarioDisciplina);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_TURMA_FUNCIONARIO_DISCIPLINA");
        }
    }
}
