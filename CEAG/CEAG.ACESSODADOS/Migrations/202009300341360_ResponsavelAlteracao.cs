namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResponsavelAlteracao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_MENSALIDADE",
                c => new
                    {
                        COD_IN_MENSALIDADE = c.Int(nullable: false, identity: true),
                        INCLUSAO_DT_MENSALIDADE = c.DateTime(nullable: false),
                        PAGAMENTO_DT_MENSALIDADE = c.DateTime(),
                        PERIODO_DT_MENSALIDADE = c.DateTime(nullable: false),
                        VENCIMENTO_DT_MENSALIDADE = c.DateTime(nullable: false),
                        NEGOCIACAO_DT_MENSALIDADE = c.DateTime(),
                        RESPONSAVEL_VC_MENSALIDADE = c.String(nullable: false, maxLength: 200, unicode: false),
                        VALOR_MO_MENSALIDADE = c.Double(nullable: false),
                        PAGO_MO_MENSALIDADE = c.Double(),
                        DESCONTO_MO_MENSALIDADE = c.Double(nullable: false),
                        DESCRICAO_VC_MENSALIDADE = c.String(nullable: false, maxLength: 200, unicode: false),
                        OBSERVACAO_VC_MENSALIDADE = c.String(nullable: false, maxLength: 500, unicode: false),
                        FORMA_PAGAMENTO_VC_MENSALIDADE = c.String(nullable: false, maxLength: 100, unicode: false),
                        COD_IN_CONTA_BANCARIA = c.Int(nullable: false),
                        COD_IN_ALUNO_MATRICULA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_MENSALIDADE)
                .ForeignKey("dbo.TAB_ALUNO_MATRICULA", t => t.COD_IN_ALUNO_MATRICULA)
                .ForeignKey("dbo.TAB_CONTA_BANCARIA", t => t.COD_IN_CONTA_BANCARIA)
                .Index(t => t.COD_IN_CONTA_BANCARIA)
                .Index(t => t.COD_IN_ALUNO_MATRICULA);
            
            CreateTable(
                "dbo.TAB_CONTA_BANCARIA",
                c => new
                    {
                        COD_IN_CONTA_BANCARIA = c.Int(nullable: false, identity: true),
                        NUMERO_VC_CONTA_BANCARIA = c.String(nullable: false, maxLength: 60, unicode: false),
                        DIGITO_VC_CONTA_BANCARIA = c.String(nullable: false, maxLength: 2, unicode: false),
                        NUMERO_AGENCIA_VC_CONTA_BANCARIA = c.String(nullable: false, maxLength: 30, unicode: false),
                        DIGITO_AGENCIA_VC_CONTA_BANCARIA = c.String(nullable: false, maxLength: 2, unicode: false),
                        BANCO_VC_CONTA_BANCARIA = c.String(nullable: false, maxLength: 160, unicode: false),
                        DESCRICAO_VC_CONTA_BANCARIA = c.String(nullable: false, maxLength: 160, unicode: false),
                        INCLUSAO_DT_CONTA_BANCARIA = c.DateTime(nullable: false),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_CONTA_BANCARIA)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_ESCOLA);
            
            AddColumn("dbo.TAB_RESPONSAVEL", "EMAIL_VC_RESPONSAVEL", c => c.String(maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_MENSALIDADE", "COD_IN_CONTA_BANCARIA", "dbo.TAB_CONTA_BANCARIA");
            DropForeignKey("dbo.TAB_CONTA_BANCARIA", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropForeignKey("dbo.TAB_MENSALIDADE", "COD_IN_ALUNO_MATRICULA", "dbo.TAB_ALUNO_MATRICULA");
            DropIndex("dbo.TAB_CONTA_BANCARIA", new[] { "COD_IN_ESCOLA" });
            DropIndex("dbo.TAB_MENSALIDADE", new[] { "COD_IN_ALUNO_MATRICULA" });
            DropIndex("dbo.TAB_MENSALIDADE", new[] { "COD_IN_CONTA_BANCARIA" });
            DropColumn("dbo.TAB_RESPONSAVEL", "EMAIL_VC_RESPONSAVEL");
            DropTable("dbo.TAB_CONTA_BANCARIA");
            DropTable("dbo.TAB_MENSALIDADE");
        }
    }
}
