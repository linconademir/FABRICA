using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class EscolaGrupoTypeConfiguration : CeagEntityAbstractConfig<EscolaGrupo>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodEscolaGrupo)
           .IsRequired()
           .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
           .HasColumnName("COD_IN_ESCOLA_GRUPO");

            Property(p => p.Cnpj)
              .HasMaxLength(14)
              .HasColumnType("varchar")
              .HasColumnName("CNPJ_VC_ESCOLA_GRUPO");

            Property(p => p.Fantasia)
              .HasMaxLength(200)
              .HasColumnType("varchar")
              .HasColumnName("FANTASIA_VC_ESCOLA_GRUPO");

            Property(p => p.Razao)
              .HasMaxLength(200)
              .HasColumnType("varchar")
              .HasColumnName("RAZAO_VC_ESCOLA_GRUPO");

            Property(p => p.Logo)
              .HasMaxLength(200)
              .HasColumnType("varchar")
              .HasColumnName("LOGO_VC_ESCOLA_GRUPO");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
            
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodEscolaGrupo);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_ESCOLA_GRUPO");
        }
    }
}
