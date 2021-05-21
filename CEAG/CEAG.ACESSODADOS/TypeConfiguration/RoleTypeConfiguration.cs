using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class RoleTypeConfiguration : CeagEntityAbstractConfig<Role>
    {
        protected override void ConfigurarCamposTabela()
        {

            Property(p => p.CodRole)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .HasColumnName("COD_IN_ROLE");


            Property(p => p.Descricao)
             .IsRequired()
             .HasColumnName("DESCRICAO_VC_ROLE")
             .HasMaxLength(60)
             .HasColumnType("varchar");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodRole);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_ROLE");
        }
    }
}
