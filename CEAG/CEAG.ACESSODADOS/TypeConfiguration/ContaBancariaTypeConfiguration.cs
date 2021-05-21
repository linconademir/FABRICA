using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class ContaBancariaTypeConfiguration : CeagEntityAbstractConfig<ContaBancaria>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodContaBancaria)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .HasColumnName("COD_IN_CONTA_BANCARIA");

            Property(p => p.CodEscola)
            .IsRequired()
            .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

            Property(p => p.Descricao)
             .IsRequired()
             .HasColumnType("varchar")
             .HasMaxLength(160)
             .HasColumnName("DESCRICAO_VC_CONTA_BANCARIA");


            Property(p => p.Agencia)
             .IsRequired()
             .HasColumnType("varchar")
             .HasMaxLength(30)
             .HasColumnName("NUMERO_AGENCIA_VC_CONTA_BANCARIA");

            Property(p => p.DigitoAgencia)
             .IsRequired()
             .HasColumnType("varchar")
             .HasMaxLength(2)
             .HasColumnName("DIGITO_AGENCIA_VC_CONTA_BANCARIA");

            Property(p => p.Conta)
             .IsRequired()
             .HasColumnType("varchar")
             .HasMaxLength(60)
             .HasColumnName("NUMERO_VC_CONTA_BANCARIA");

            Property(p => p.DigitoConta)
             .IsRequired()
             .HasColumnType("varchar")
             .HasMaxLength(2)
             .HasColumnName("DIGITO_VC_CONTA_BANCARIA");

            Property(p => p.Inclusao)
             .IsRequired()
             .HasColumnName("INCLUSAO_DT_CONTA_BANCARIA");


            Property(p => p.Banco)
             .IsRequired()
             .HasColumnType("varchar")
             .HasMaxLength(160)
             .HasColumnName("BANCO_VC_CONTA_BANCARIA");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Escola)
             .WithMany(p => p.ContaBancarias)
             .HasForeignKey(fk => fk.CodEscola);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodContaBancaria);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_CONTA_BANCARIA");
        }
    }
}
