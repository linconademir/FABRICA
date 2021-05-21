namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraMensalidade1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TAB_MENSALIDADE", "COD_IN_ALUNO_MATRICULA", "dbo.TAB_ALUNO_MATRICULA");
            DropIndex("dbo.TAB_MENSALIDADE", new[] { "COD_IN_ALUNO_MATRICULA" });
            CreateTable(
                "dbo.TAB_DEBITO",
                c => new
                    {
                        COD_IN_DEBITO = c.Int(nullable: false, identity: true),
                        INCLUSAO_DT_DEBITO = c.DateTime(nullable: false),
                        PAGAMENTO_DT_DEBITO = c.DateTime(),
                        PERIODO_DT_DEBITO = c.DateTime(nullable: false),
                        VENCIMENTO_DT_DEBITO = c.DateTime(nullable: false),
                        NEGOCIACAO_DT_DEBITO = c.DateTime(),
                        TIPO_VC_DEBITO = c.String(nullable: false, maxLength: 50, unicode: false),
                        RESPONSAVEL_VC_DEBITO = c.String(nullable: false, maxLength: 200, unicode: false),
                        VALOR_MO_DEBITO = c.Double(nullable: false),
                        PAGO_MO_DEBITO = c.Double(),
                        DESCONTO_MO_DEBITO = c.Double(),
                        DESCRICAO_VC_DEBITO = c.String(nullable: false, maxLength: 200, unicode: false),
                        OBSERVACAO_VC_DEBITO = c.String(nullable: false, maxLength: 500, unicode: false),
                        FORMA_PAGAMENTO_VC_DEBITO = c.String(nullable: false, maxLength: 100, unicode: false),
                        COD_IN_ACESSO = c.Int(),
                        COD_IN_ALUNO_MATRICULA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_DEBITO)
                .ForeignKey("dbo.TAB_ALUNO_MATRICULA", t => t.COD_IN_ALUNO_MATRICULA)
                .Index(t => t.COD_IN_ALUNO_MATRICULA);
            
            DropTable("dbo.TAB_MENSALIDADE");
        }
        
        public override void Down()
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
                        DESCONTO_MO_MENSALIDADE = c.Double(),
                        DESCRICAO_VC_MENSALIDADE = c.String(nullable: false, maxLength: 200, unicode: false),
                        OBSERVACAO_VC_MENSALIDADE = c.String(nullable: false, maxLength: 500, unicode: false),
                        FORMA_PAGAMENTO_VC_MENSALIDADE = c.String(nullable: false, maxLength: 100, unicode: false),
                        COD_IN_ACESSO = c.Int(),
                        COD_IN_ALUNO_MATRICULA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_MENSALIDADE);
            
            DropForeignKey("dbo.TAB_DEBITO", "COD_IN_ALUNO_MATRICULA", "dbo.TAB_ALUNO_MATRICULA");
            DropIndex("dbo.TAB_DEBITO", new[] { "COD_IN_ALUNO_MATRICULA" });
            DropTable("dbo.TAB_DEBITO");
            CreateIndex("dbo.TAB_MENSALIDADE", "COD_IN_ALUNO_MATRICULA");
            AddForeignKey("dbo.TAB_MENSALIDADE", "COD_IN_ALUNO_MATRICULA", "dbo.TAB_ALUNO_MATRICULA", "COD_IN_ALUNO_MATRICULA");
        }
    }
}
