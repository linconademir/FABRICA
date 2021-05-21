using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class EscolaTypeConfiguration : CeagEntityAbstractConfig<Escola>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodEscola)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.CodEscolaGrupo)
           .IsOptional()
           .HasColumnName("COD_IN_ESCOLA_GRUPO");

            Property(p => p.CodLogradouro)
           .IsOptional()
           .HasColumnName("COD_IN_LOGRADOURO");


            Property(p => p.Cnpj)
             .IsRequired()
             .HasColumnName("CNPJ_VC_ESCOLA")
             .HasMaxLength(14)
             .HasColumnType("varchar");

            Property(p => p.Autorizacao)
             .IsOptional()
             .HasColumnName("AUTORIZACAO_VC_ESCOLA")
             .HasMaxLength(60)
             .HasColumnType("varchar");

            Property(p => p.PublicacaoDiarioOficial)
             .IsOptional()
             .HasColumnName("PUBLICACAO_DT_ESCOLA");

            Property(p => p.Inscricao)
             .IsOptional()
             .HasColumnName("INSCRICAO_VC_ESCOLA")
             .HasMaxLength(60)
             .HasColumnType("varchar");

            Property(p => p.Fantasia)
             .IsRequired()
             .HasColumnName("FANTASIA_VC_ESCOLA")
             .HasMaxLength(200)
             .HasColumnType("varchar");

            Property(p => p.Razao)
             .IsRequired()
             .HasColumnName("RAZAO_VC_ESCOLA")
             .HasMaxLength(200)
             .HasColumnType("varchar");

            Property(p => p.Logo)
           .HasColumnName("LOGO_VC_ESCOLA")
           .HasMaxLength(200)
           .HasColumnType("varchar");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.EscolaGrupo)
                .WithMany(p => p.Escolas)
                .HasForeignKey(fk => fk.CodEscolaGrupo);

            HasRequired(p => p.Logradouro)
               .WithMany(p => p.Escolas)
               .HasForeignKey(fk => fk.CodLogradouro);
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodEscola);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_ESCOLA");
        }
    }
}
