using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class AcessoRoleTypeConfiguration : CeagEntityAbstractConfig<AcessoRole>
    {
        protected override void ConfigurarCamposTabela()
        {

            Property(p => p.CodAcessoRole)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .HasColumnName("COD_IN_ACESSO_ROLE");

            Property(p => p.CodAcesso)
            .IsRequired()
            .HasColumnName("COD_IN_ACESSO");

            Property(p => p.CodRole)
            .IsRequired()
            .HasColumnName("COD_IN_ROLE");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Acesso)
             .WithMany(p => p.AcessoRoles)
             .HasForeignKey(fk => fk.CodAcesso);

            HasRequired(p => p.Role)
             .WithMany(p => p.AcessoRoles)
             .HasForeignKey(fk => fk.CodRole);
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodAcessoRole);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_ACESSO_ROLE");
        }
    }
}
