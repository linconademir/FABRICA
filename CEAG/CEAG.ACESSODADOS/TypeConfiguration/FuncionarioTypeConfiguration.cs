using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    class FuncionarioTypeConfiguration : CeagEntityAbstractConfig<Funcionario>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodFuncionario)
              .IsRequired()
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("COD_IN_FUNCIONARIO");

            Property(p => p.Nome)
              .IsRequired()
              .HasColumnName("NOME_VC_FUNCIONARIO")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.Sexo)
             .IsRequired()
             .HasColumnName("SEXO_VC_FUNCIONARIO")
             .HasMaxLength(15)
             .HasColumnType("varchar");

            Property(p => p.Cpf)
              .HasColumnName("CPF_VC_FUNCIONARIO")
              .HasMaxLength(11)
              .HasColumnType("varchar");

            Property(p => p.Nacionalidade)
                .HasColumnName("NACIONALIDADE_VC_FUNCIONARIO")
                .HasMaxLength(60)
                .HasColumnType("varchar");

            Property(p => p.Naturalidade)
                .HasColumnName("NATURALIDADE_VC_FUNCIONARIO")
                .HasMaxLength(60)
                .HasColumnType("varchar");

            Property(p => p.Email)
              .HasColumnName("EMAIL_VC_FUNCIONARIO")
              .HasMaxLength(150)
              .HasColumnType("varchar");

            Property(p => p.Inclusao)
              .HasColumnName("INCLUSAO_DT_FUNCIONARIO");

            Property(p => p.Cancelamento)
                .IsOptional()
                .HasColumnName("CANCELAMENTO_DT_FUNCIONARIO");

            Property(p => p.Nascimento)
             .HasColumnName("NASCIMENTO_DT_FUNCIONARIO");

            Property(p => p.CodLogradouro)
              .HasColumnName("COD_IN_LOGRADOURO");

            Property(p => p.CodEscola)
              .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.CodAcesso)
              .HasColumnName("COD_IN_ACESSO");

            Property(p => p.Funcao)
              .HasColumnName("FUNCAO_VC_FUNCIONARIO")
              .HasMaxLength(150)
              .HasColumnType("varchar");

            Property(p => p.Rg)
               .HasColumnName("RG_VC_FUNCIONARIO")
               .HasMaxLength(30)
               .HasColumnType("varchar");

            Property(p => p.RgUf)
              .HasColumnName("RG_UF_VC_FUNCIONARIO")
              .HasMaxLength(2)
              .HasColumnType("varchar");

            Property(p => p.RgOrgaoEmissor)
              .HasColumnName("RG_ORGAO_VC_FUNCIONARIO")
              .HasMaxLength(5)
              .HasColumnType("varchar");

            Property(p => p.Titulacao)
              .HasColumnName("TITULACAO_VC_FUNCIONARIO")
              .HasMaxLength(60)
              .HasColumnType("varchar");

            Property(p => p.Observacao)
          .HasColumnName("OBSERVACAO_VC_FUNCIONARIO")
          .HasMaxLength(300)
          .HasColumnType("varchar");


        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Logradouro)
               .WithMany(p => p.Funcionarios)
               .HasForeignKey(fk => fk.CodLogradouro);

            HasRequired(p => p.Escola)
               .WithMany(p => p.Funcionarios)
               .HasForeignKey(fk => fk.CodEscola);

            HasRequired(p => p.Acesso)
               .WithMany(p => p.Funcionarios)
               .HasForeignKey(fk => fk.CodAcesso);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodFuncionario);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_FUNCIONARIO");
        }
    }
}
