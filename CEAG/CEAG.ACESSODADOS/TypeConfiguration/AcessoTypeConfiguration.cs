using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class AcessoTypeConfiguration : CeagEntityAbstractConfig<Acesso>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodAcesso)
             .IsRequired()
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
             .HasColumnName("COD_IN_ACESSO");

            Property(p => p.Email)
             .IsRequired()
             .HasColumnName("EMAIL_VC_ACESSO")
             .HasMaxLength(200)
             .HasColumnType("varchar");

            Property(p => p.Senha)
             .IsRequired()
             .HasColumnName("SENHA_VC_ACESSO")
             .HasMaxLength(200)
             .HasColumnType("varchar");

            Property(p => p.Foto)
            .IsRequired()
            .HasColumnName("FOTO_VC_ACESSO")
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(p => p.Ativo)
            .IsRequired()
            .HasColumnName("ATIVO_VC_ACESSO")
            .HasMaxLength(3)
            .HasColumnType("varchar");

            Property(p => p.Nome)
            .IsRequired()
            .HasColumnName("NOME_VC_ACESSO")
            .HasMaxLength(60)
            .HasColumnType("varchar");

            Property(p => p.Sobrenome)
            .IsRequired()
            .HasColumnName("SOBRENOME_VC_ACESSO")
            .HasMaxLength(60)
            .HasColumnType("varchar");

            Property(p => p.CodEscola)
           .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.CodEscolaGrupo)
           .HasColumnName("COD_IN_ESCOLA_GRUPO");

            Property(p => p.CodFuncionario)
                .IsOptional()
           .HasColumnName("COD_IN_FUNCIONARIO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
               .WithMany(p => p.Acessos)
               .HasForeignKey(fk => fk.CodEscola);

            HasRequired(p => p.Funcionario)
               .WithMany(p => p.Acessos)
               .HasForeignKey(fk => fk.CodFuncionario);

            HasRequired(p => p.EscolaGrupo)
                .WithMany(p => p.Acessos)
                .HasForeignKey(fk => fk.CodEscolaGrupo);
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodAcesso);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_ACESSO");
        }
    }
}
