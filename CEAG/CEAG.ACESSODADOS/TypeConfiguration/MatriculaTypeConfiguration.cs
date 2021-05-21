using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class MatriculaTypeConfiguration : CeagEntityAbstractConfig<Matricula>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodMatricula)
              .IsRequired()
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("COD_IN_MATRICULA");

            Property(p => p.Ano)
              .IsRequired()
              .HasColumnName("ANO_IN_MATRICULA_INDICE");

            Property(p => p.Ordem)
             .IsRequired()
             .HasColumnName("ORDEM_IN_MATRICULA_INDICE");

            Property(p => p.NumeroMatricula)
               .IsRequired()
               .HasColumnName("MATRICULA_VC_MATRICULA_INDICE")
               .HasMaxLength(50)
               .HasColumnType("varchar");

            Property(p => p.Status)
               .IsRequired()
               .HasColumnName("STATUS_VC_MATRICULA_INDICE")
               .HasMaxLength(30)
               .HasColumnType("varchar");

            Property(p => p.Inclusao)
               .IsRequired()
               .HasColumnName("INCLUSAO_DT_MATRICULA_INDICE");

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
                .WithMany(p => p.Matriculas)
                .HasForeignKey(fk => fk.CodEscola);

     
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodMatricula);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_MATRICULA_INDICE");
        }
    }
}
