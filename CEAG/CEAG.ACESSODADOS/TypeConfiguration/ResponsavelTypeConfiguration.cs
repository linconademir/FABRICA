using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class ResponsavelTypeConfiguration : CeagEntityAbstractConfig<Responsavel>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodResponsavel)
              .IsRequired()
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("COD_IN_RESPONSAVEL");

            Property(p => p.Cpf)
              .IsRequired()
              .HasColumnName("CPF_VC_RESPONSAVEL")
              .HasMaxLength(11)
              .HasColumnType("varchar");

            Property(p => p.DeclaraIR)
              .IsRequired()
              .HasColumnName("DECLARA_IR_VC_RESPONSAVEL")
              .HasMaxLength(3)
              .HasColumnType("varchar");

            Property(p => p.Nome)
              .IsRequired()
              .HasColumnName("NOME_VC_RESPONSAVEL")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.Email)
                .IsOptional()
            .HasColumnName("EMAIL_VC_RESPONSAVEL")
            .HasMaxLength(150)
            .HasColumnType("varchar");

            Property(p => p.RecebeEmail)
                .IsOptional()
            .HasColumnName("RECEBE_EMAIL_VC_RESPONSAVEL")
            .HasMaxLength(3)
            .HasColumnType("varchar");

            Property(p => p.Sexo)
              .HasColumnName("SEXO_VC_RESPONSAVEL")
              .HasMaxLength(30)
              .HasColumnType("varchar");

            Property(p => p.Tipo)
              .IsRequired()
              .HasColumnName("TIPO_VC_RESPONSAVEL")
              .HasMaxLength(60)
              .HasColumnType("varchar");

            Property(p => p.Rg)
                .IsOptional()
              .HasColumnName("RG_VC_RESPONSAVEL")
              .HasMaxLength(10)
              .HasColumnType("varchar");

            Property(p => p.RgOrgaoEmissor)
                .IsOptional()
              .HasColumnName("RG_ORGAO_VC_RESPONSAVEL")
              .HasMaxLength(10)
              .HasColumnType("varchar");

            Property(p => p.RgUf)
                .IsOptional()
              .HasColumnName("RG_UF_VC_RESPONSAVEL")
              .HasMaxLength(3)
              .HasColumnType("varchar");

            Property(p => p.Profissao)
                .IsOptional()
              .HasColumnName("PROFISSAO_VC_RESPONSAVEL")
              .HasMaxLength(160)
              .HasColumnType("varchar");

            Property(p => p.EstadoCivil)
                .IsOptional()
              .HasColumnName("ESTADO_CIVIL_VC_RESPONSAVEL")
              .HasMaxLength(30)
              .HasColumnType("varchar");

            Property(p => p.Naturalidade)
                .IsOptional()
              .HasColumnName("NATURALIDADE_VC_RESPONSAVEL")
              .HasMaxLength(60)
              .HasColumnType("varchar");

            Property(p => p.Nacionalidade)
                .IsOptional()
              .HasColumnName("NACIONALIDADE_VC_RESPONSAVEL")
              .HasMaxLength(60)
              .HasColumnType("varchar");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

            Property(p => p.CodAluno)
             .IsRequired()
             .HasColumnName("COD_IN_ALUNO");

            Property(p => p.CodLogradouro)
             .HasColumnName("COD_IN_LOGRADOURO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Aluno)
                 .WithMany(p => p.Responsavels)
                 .HasForeignKey(fk => fk.CodAluno);

            HasRequired(p => p.Logradouro)
                .WithMany(p => p.Responsavels)
                .HasForeignKey(fk => fk.CodLogradouro);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodResponsavel);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_RESPONSAVEL");
        }
    }
}
