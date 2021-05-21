using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class MensagemTypeConfiguration : CeagEntityAbstractConfig<Mensagem>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodMensagem)
              .IsRequired()
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("COD_IN_MENSAGEM");

            Property(p => p.CodAlunoMatricula)
             .IsOptional()
             .HasColumnName("COD_ALUNO_MATRICULA");

            Property(p => p.Prioridade)
             .IsOptional()
             .HasColumnName("PRIORIDADE_IN_MENSAGEM");

            Property(p => p.Inclusao)
             .IsRequired()
             .HasColumnName("INCLUSAO_DT_MENSAGEM");

            Property(p => p.Envio)
             .IsOptional()
             .HasColumnName("ENVIO_DT_MENSAGEM");

            Property(p => p.Descricao)
              .IsRequired()
              .HasColumnName("DESCRICAO_VC_MENSAGEM")
              .HasMaxLength(800)
              .HasColumnType("varchar");

            Property(p => p.Titulo)
              .IsRequired()
              .HasColumnName("TITULO_VC_MENSAGEM")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.Status)
             .IsRequired()
             .HasColumnName("STATUS_VC_MENSAGEM")
             .HasMaxLength(30)
             .HasColumnType("varchar");

            Property(p => p.Tipo)
            .IsRequired()
            .HasColumnName("TIPO_VC_MENSAGEM")
            .HasMaxLength(30)
            .HasColumnType("varchar");

            Property(p => p.Destinatario)
           .IsOptional()
           .HasColumnName("DESTINATARIO_VC_MENSAGEM")
           .HasMaxLength(300)
           .HasColumnType("varchar");

            Property(p => p.DestinatarioCopia)
             .IsOptional()
             .HasColumnName("DESTINATARIO_COPIA_VC_MENSAGEM")
             .HasMaxLength(300)
             .HasColumnType("varchar");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasOptional(p => p.AlunoMatricula)
              .WithMany(p => p.Mensagems)
              .HasForeignKey(fk => fk.CodAlunoMatricula);

            //modelBuilder.Entity<Country>()
            //.HasMany(c => c.Users)
            //.WithOptional(c => c.Country)
            //.HasForeignKey(c => c.CountryId)
            //.WillCascadeOnDelete(false);
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodMensagem);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_MENSAGEM");
        }
    }
}
