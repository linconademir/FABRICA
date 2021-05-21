using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class DebitoTypeConfiguration : CeagEntityAbstractConfig<Debito>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodDebito)
              .IsRequired()
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("COD_IN_DEBITO");

            Property(p => p.Descricao)
              .IsRequired()
              .HasColumnName("DESCRICAO_VC_DEBITO")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.CodAlunoMatricula)
              .IsRequired()
              .HasColumnName("COD_IN_ALUNO_MATRICULA");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");

            Property(p => p.NumeroDebito)
            .IsOptional()
            .HasColumnName("NUMERO_IN_DEBITO");

            Property(p => p.Inclusao)
              .IsRequired()
              .HasColumnName("INCLUSAO_DT_DEBITO");

            Property(p => p.Pagamento)
              .IsOptional()
              .HasColumnName("PAGAMENTO_DT_DEBITO");

            Property(p => p.Negociacao)
              .IsOptional()
              .HasColumnName("NEGOCIACAO_DT_DEBITO");

            Property(p => p.Vencimento)
              .IsRequired()
              .HasColumnName("VENCIMENTO_DT_DEBITO");

            Property(p => p.Periodo)
              .IsRequired()
              .HasColumnName("PERIODO_DT_DEBITO");

            Property(p => p.Responsavel)
              .IsRequired()
              .HasColumnName("RESPONSAVEL_VC_DEBITO")
              .HasMaxLength(200)
              .HasColumnType("varchar");

            Property(p => p.IdenficadorHexa)
              .HasMaxLength(200)
              .HasColumnType("varchar")
              .HasColumnName("IDENTIFICADOR_VC_DEBITO");

            Property(p => p.QrCode)
              .HasMaxLength(800)
              .HasColumnType("varchar")
              .HasColumnName("QRCODE_VC_DEBITO");

            Property(p => p.Observacao)
              .IsRequired()
              .HasColumnName("OBSERVACAO_VC_DEBITO")
              .HasMaxLength(800)
              .HasColumnType("varchar");

            Property(p => p.TipoDebito)
             .IsRequired()
             .HasColumnName("TIPO_VC_DEBITO")
             .HasMaxLength(50)
             .HasColumnType("varchar");

            Property(p => p.FormaPagamento)
              .IsRequired()
              .HasColumnName("FORMA_PAGAMENTO_VC_DEBITO")
              .HasMaxLength(100)
              .HasColumnType("varchar");

            Property(p => p.ValorDebito)
             .IsRequired()
             .HasColumnName("VALOR_MO_DEBITO");

            Property(p => p.ValorPago)
             .IsOptional()
             .HasColumnName("PAGO_MO_DEBITO");

            Property(p => p.Desconto)
             .IsOptional()
             .HasColumnName("DESCONTO_MO_DEBITO");

        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.AlunoMatricula)
             .WithMany(p => p.Debitos)
             .HasForeignKey(fk => fk.CodAlunoMatricula);
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.CodDebito);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_DEBITO");
        }
    }
}
