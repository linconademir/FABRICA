using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class FuncionarioDisciplinaTypeConfiguration : CeagEntityAbstractConfig<FuncionarioDisciplina>
    {
        protected override void ConfigurarCamposTabela()
        {

            Property(p => p.CodFuncionarioDisciplina)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .HasColumnName("COD_IN_FUNCIONARIO_DISCIPLINA");

            Property(p => p.CodDisciplina)
            .IsRequired()
            .HasColumnName("COD_IN_DISCIPLINA");

            Property(p => p.CodFuncionario)
            .IsRequired()
            .HasColumnName("COD_IN_FUNCIONARIO");

            Property(p => p.Observacao)
             .HasColumnName("OBSERVACAO_VC_FUNCIONARIO_DISCIPLINA")
             .HasMaxLength(200)
             .HasColumnType("varchar");

            Property(p => p.Inclusao)
             .HasColumnName("INCLUSAO_DT_FUNCIONARIO_DISCIPLINA");


            Property(p => p.Cancelamento)
             .HasColumnName("CANCELAMENTO_DT_FUNCIONARIO_DISCIPLINA");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");


        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Funcionario)
             .WithMany(p => p.FuncionarioDisciplinas)
             .HasForeignKey(fk => fk.CodFuncionario);

            HasRequired(p => p.Disciplina)
             .WithMany(p => p.FuncionarioDisciplinas)
             .HasForeignKey(fk => fk.CodDisciplina);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodFuncionarioDisciplina);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_FUNCIONARIO_DISCIPLINA");
        }
    }
}
