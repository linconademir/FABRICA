using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class HorarioAulaTypeConfiguration : CeagEntityAbstractConfig<HorarioAula>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodHorarioAula)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("COD_IN_HORARIO_AULA");

            Property(p => p.DiaSemana)
                .IsRequired()
                .HasColumnName("DIA_SEMANA_IN_HORARIO_AULA");

            Property(p => p.Inicio)
                .IsRequired()
                .HasColumnName("INICIO_TM_HORARIO_AULA");

            Property(p => p.Termino)
                .IsRequired()
                .HasColumnName("TERMINO_TM_HORARIO_AULA");

            Property(p => p.Turno)
                .IsRequired()
                .HasColumnName("TURNO_VC_HORARIO_AULA")
                .HasMaxLength(30)
                .HasColumnType("varchar");

            Property(p => p.CodHorario)
              .IsRequired()
              .HasColumnName("COD_IN_HORARIO");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Horario)
              .WithMany(p => p.HorarioAulas)
              .HasForeignKey(fk => fk.CodHorario);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodHorarioAula);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_HORARIO_AULA");
        }
    }
}
