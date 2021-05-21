using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class UnidadeTypeConfiguration : CeagEntityAbstractConfig<Unidade>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodUnidade)
             .IsRequired()
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
             .HasColumnName("COD_IN_UNIDADE");

            Property(p => p.Ano)
             .IsRequired()
             .HasColumnName("ANO_IN_UNIDADE");

            Property(p => p.CodEscola)
             .IsRequired()
             .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.CodAcesso)
           .IsOptional()
           .HasColumnName("COD_IN_ACESSO");

            Property(p => p.Abertura)
             .IsRequired()
             .HasColumnName("ABERTURA_DT_UNIDADE");

            Property(p => p.Inclusao)
             .IsRequired()
             .HasColumnName("INCLUSAO_DT_UNIDADE");

            Property(p => p.Fechamento)
             .IsOptional()
             .HasColumnName("FECHAMENTO_DT_UNIDADE");

            Property(p => p.Numero)
             .IsRequired()
             .HasColumnName("NUMERO_IN_UNIDADE");
            
        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
               .WithMany(p => p.Unidades)
               .HasForeignKey(fk => fk.CodEscola);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodUnidade);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_UNIDADE");
        }
    }
}
