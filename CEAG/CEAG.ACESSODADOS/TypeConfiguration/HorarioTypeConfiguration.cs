using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class HorarioTypeConfiguration : CeagEntityAbstractConfig<Horario>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodHorario)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("COD_IN_HORARIO");

            Property(p => p.Descricao)
                .IsRequired()
                .HasColumnName("DESCRICAO_VC_HORARIO")
                .HasMaxLength(150)
                .HasColumnType("varchar");

            Property(p => p.Inclusao)
               .IsRequired()
               .HasColumnName("INCLUSAO_DT_HORARIO");

            Property(p => p.Cancelamento)
               .IsOptional()
               .HasColumnName("CANCELAMENTO_DT_HORARIO");

            Property(p => p.CodEscola)
               .IsRequired()
               .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
              .WithMany(p => p.Horarios)
              .HasForeignKey(fk => fk.CodEscola);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodHorario);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_HORARIO");
        }
    }
}
