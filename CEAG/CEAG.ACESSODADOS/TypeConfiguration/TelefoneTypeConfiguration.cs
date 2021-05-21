using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class TelefoneTypeConfiguration : CeagEntityAbstractConfig<Telefone>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodTelefone)
             .IsRequired()
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
             .HasColumnName("COD_IN_TELEFONE");

            Property(p => p.CodAluno)
              .HasColumnName("COD_IN_ALUNO");

            Property(p => p.CodAcesso)
              .IsOptional()
              .HasColumnName("COD_IN_ACESSO");

            Property(p => p.CodResponsavel)
            .HasColumnName("COD_IN_RESPONSAVEL");

            Property(p => p.CodFuncionario)
        .HasColumnName("COD_IN_FUNCIONARIO");

            Property(p => p.Ddd)
             .IsRequired()
             .HasColumnName("DDD_VC_TELEFONE")
             .HasMaxLength(2)
             .HasColumnType("varchar");

            Property(p => p.Numero)
             .IsRequired()
             .HasColumnName("NUMERO_VC_TELEFONE")
             .HasMaxLength(9)
             .HasColumnType("varchar");

            Property(p => p.Tipo)
             .IsRequired()
             .HasColumnName("TIPO_VC_TELEFONE")
             .HasMaxLength(60)
             .HasColumnType("varchar");

            Property(p => p.TipoContato)
            .IsRequired()
            .HasColumnName("TIPO_CONTATO_VC_TELEFONE")
            .HasMaxLength(60)
            .HasColumnType("varchar");

            Property(p => p.Local)
             .IsRequired()
             .HasColumnName("LOCAL_VC_TELEFONE")
             .HasMaxLength(60)
             .HasColumnType("varchar");

            Property(p => p.Pessoa)
            .IsRequired()
            .HasColumnName("PESSOA_VC_TELEFONE")
            .HasMaxLength(60)
            .HasColumnType("varchar");

            Property(p => p.Observacao)
            .IsOptional()
            .HasColumnName("OBSERVACAO_VC_TELEFONE")
            .HasMaxLength(300)
            .HasColumnType("varchar");


            Property(p => p.Inclusao)
             .IsRequired()
             .HasColumnName("INCLUSAO_VC_TELEFONE");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
            HasRequired(p => p.Aluno)
               .WithMany(p => p.Telefones)
               .HasForeignKey(fk => fk.CodAluno);

            HasRequired(p => p.Responsavel)
                .WithMany(p => p.Telefones)
                .HasForeignKey(fk => fk.CodResponsavel);

            HasRequired(p => p.Funcionario)
               .WithMany(p => p.Telefones)
               .HasForeignKey(fk => fk.CodFuncionario);

        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodTelefone);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_TELEFONE");
        }
    }
}
