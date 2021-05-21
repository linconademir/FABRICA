using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class LogradouroTypeConfiguration : CeagEntityAbstractConfig<Logradouro>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodLogradouro)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .HasColumnName("COD_IN_LOGRADOURO");

            Property(p => p.CodAcesso)
           .IsOptional()
           .HasColumnName("COD_IN_ACESSO");

            Property(p => p.Bairro)
             .IsRequired()
             .HasColumnName("BAIRRO_VC_LOGRADOURO")
             .HasMaxLength(100)
             .HasColumnType("varchar");

            Property(p => p.Municipio)
             .IsRequired()
             .HasColumnName("MUNICIPIO_VC_LOGRADOURO")
             .HasMaxLength(100)
             .HasColumnType("varchar");

            Property(p => p.Numero)
             .IsRequired()
             .HasColumnName("NUMERO_VC_LOGRADOURO")
             .HasMaxLength(10)
             .HasColumnType("varchar");
            
            Property(p => p.Referencia)
             .HasColumnName("REFERENCIA_VC_LOGRADOURO")
             .HasMaxLength(200)
             .HasColumnType("varchar");

            Property(p => p.Rua)
             .IsRequired()
             .HasColumnName("RUA_VC_LOGRADOURO")
             .HasMaxLength(200)
             .HasColumnType("varchar");

            Property(p => p.Uf)
             .IsRequired()
             .HasColumnName("UF_VC_LOGRADOURO")
             .HasMaxLength(2)
             .HasColumnType("varchar");

            Property(p => p.Complemento)
             .HasColumnName("COMPLEMENTO_VC_LOGRADOURO")
             .HasMaxLength(200)
             .HasColumnType("varchar");

            Property(p => p.Cep)
             .IsRequired()
             .HasColumnName("CEP_VC_LOGRADOURO")
             .HasMaxLength(8)
             .HasColumnType("varchar");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
       
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodLogradouro);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_LOGRADOURO");
        }
    }
}
