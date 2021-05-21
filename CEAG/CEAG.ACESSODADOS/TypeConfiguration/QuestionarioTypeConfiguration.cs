using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.TypeConfiguration
{
    public class QuestionarioTypeConfiguration : CeagEntityAbstractConfig<Questionario>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.CodQuestionario)
             .IsRequired()
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
             .HasColumnName("COD_IN_QUESTIONARIO");

            Property(p => p.Descricao)
             .IsRequired()
             .HasColumnName("DESCRICAO_VC_QUESTIONARIO")
             .HasMaxLength(200)
             .HasColumnType("varchar");

            Property(p => p.PerguntaComplemento)
            .HasColumnName("PERGUNTA_COMPLEMENTO_VC_QUESTIONARIO")
            .HasMaxLength(100)
            .HasColumnType("varchar");

            Property(p => p.Tipo)
             .IsRequired()
             .HasColumnName("TIPO_VC_QUESTIONARIO")
             .HasMaxLength(60)
             .HasColumnType("varchar");

            Property(p => p.CodEscola)
            .IsRequired()
            .HasColumnName("COD_IN_ESCOLA");

            Property(p => p.CodAcesso)
            .IsOptional()
            .HasColumnName("COD_IN_ACESSO");


            Property(p => p.Ordem)
            .IsRequired()
            .HasColumnName("ORDEM_IN_QUESTIONARIO");

            Property(p => p.TemComplemento)
            .IsRequired()
            .HasColumnName("TEM_COMPLEMENTO_VC_QUESTIONARIO")
            .HasMaxLength(3)
            .HasColumnType("varchar");
        }

        protected override void ConfigurarChaveEstrangeira()
        {
           
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(pk => pk.CodQuestionario);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("TAB_QUESTIONARIO");
        }
    }
}
