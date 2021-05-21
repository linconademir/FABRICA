using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class DisciplinaTypeConfiguration : CeagEntityAbstractConfig<Disciplina>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodDisciplina)
              .IsRequired()
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("COD_IN_DISCIPLINA");

            Property(p => p.CodEscola)
              .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

            Property(p => p.Descricao)
              .IsRequired()
              .HasColumnName("DESCRICAO_VC_DISCIPLINA")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.Inclusao)
              .HasColumnName("INCLUSAO_VC_DISCIPLINA");

            Property(p => p.Obrigatoria)
              .IsRequired()
              .HasColumnName("OBRIGATORIA_VC_DISCIPLINA")
              .HasMaxLength(3)
              .HasColumnType("varchar");

            Property(p => p.Tipo)
              .IsRequired()
              .HasColumnName("TIPO_VC_DISCIPLINA")
              .HasMaxLength(200)
              .HasColumnType("varchar");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
               .WithMany(p => p.Disciplinas)
               .HasForeignKey(fk => fk.CodEscola);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodDisciplina);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_DISCIPLINA");
        }
    }
}
