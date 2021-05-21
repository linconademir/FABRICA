using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using CEAG.DOMINIO;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class AlunoTypeConfiguration : CeagEntityAbstractConfig<Aluno>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodAluno)
              .IsRequired()
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("COD_IN_ALUNO");

            Property(p => p.Nome)
              .IsRequired()
              .HasColumnName("NOME_VC_ALUNO")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.Sexo)
             .IsRequired()
             .HasColumnName("SEXO_VC_ALUNO")
             .HasMaxLength(15)
             .HasColumnType("varchar");

            Property(p => p.Cpf)
              .HasColumnName("CPF_VC_ALUNO")
              .HasMaxLength(11)
              .IsOptional()
              .HasColumnType("varchar");

            Property(p => p.Nacionalidade)
                .HasColumnName("NACIONALIDADE_VC_ALUNO")
                .HasMaxLength(60)
                .IsOptional()
                .HasColumnType("varchar");

            Property(p => p.Naturalidade)
                .HasColumnName("NATURALIDADE_VC_ALUNO")
                .HasMaxLength(60)
                .IsOptional()
                .HasColumnType("varchar");

            Property(p => p.TipoSanguineo)
             .HasColumnName("TP_SANGUINEO_VC_ALUNO")
             .HasMaxLength(30)
             .IsOptional()
             .HasColumnType("varchar");

            Property(p => p.FatorRH)
             .HasColumnName("FATOR_RH_VC_ALUNO")
             .HasMaxLength(30)
             .IsOptional()
             .HasColumnType("varchar");

            Property(p => p.Email)
              .IsOptional()
              .HasColumnName("EMAIL_VC_ALUNO")
              .HasMaxLength(150)
              .HasColumnType("varchar");

            Property(p => p.Inclusao)
              .IsRequired()
              .HasColumnName("INCLUSAO_DT_ALUNO");

            Property(p => p.MaeNome)
              .IsOptional()
              .HasColumnName("MAE_VC_ALUNO")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.PaiNome)
              .IsOptional()
              .HasColumnName("PAI_VC_ALUNO")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.MaeProfissao)
                .IsOptional()
                .HasColumnName("MAE_PROFISSAO_VC_ALUNO")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.PaiProfissao)
                .IsOptional()
              .HasColumnName("PAI_PROFISSAO_VC_ALUNO")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.Matricula)
              .IsRequired()
              .HasColumnName("MATRICULA_VC_ALUNO")
              .HasMaxLength(30)
              .HasColumnType("varchar");

            Property(p => p.Nascimento)
              .IsRequired()
              .HasColumnName("NASCIMENTO_DT_ALUNO");

            Property(p => p.Rg)
                .IsOptional()
              .HasColumnName("RG_VC_ALUNO")
              .HasMaxLength(30)
              .HasColumnType("varchar");

            Property(p => p.RgUf)
                .IsOptional()
              .HasColumnName("RG_UF_VC_ALUNO")
              .HasMaxLength(2)
              .HasColumnType("varchar");

            Property(p => p.RgOrgaoEmissor)
                .IsOptional()
              .HasColumnName("RG_ORGAO_VC_ALUNO")
              .HasMaxLength(5)
              .HasColumnType("varchar");

            Property(p => p.CodEscola)
             .IsRequired()
             .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.CodLogradouro)
            .HasColumnName("COD_IN_LOGRADOURO");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
                .WithMany(p => p.Alunos)
                .HasForeignKey(fk => fk.CodEscola);

            HasRequired(p => p.Logradouro)
                .WithMany(p => p.Alunos)
                .HasForeignKey(fk => fk.CodLogradouro);


        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodAluno);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_ALUNO");
        }
    }
}
