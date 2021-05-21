namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DebitoHistorico : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_DEBITO_HISTORICO",
                c => new
                    {
                        COD_IN_DEBITO_HISTORICO = c.Int(nullable: false, identity: true),
                        DESCRICAO_VC_DEBITO_HISTORICO = c.String(nullable: false, maxLength: 200, unicode: false),
                        OBSERVACAO_VC_DEBITO_HISTORICO = c.String(maxLength: 400, unicode: false),
                        INCLUSAO_DT_DEBITO = c.DateTime(nullable: false),
                        COD_IN_ACESSO = c.Int(nullable: false),
                        COD_IN_DEBITO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_DEBITO_HISTORICO)
                .ForeignKey("dbo.TAB_DEBITO", t => t.COD_IN_DEBITO)
                .Index(t => t.COD_IN_DEBITO);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_DEBITO_HISTORICO", "COD_IN_DEBITO", "dbo.TAB_DEBITO");
            DropIndex("dbo.TAB_DEBITO_HISTORICO", new[] { "COD_IN_DEBITO" });
            DropTable("dbo.TAB_DEBITO_HISTORICO");
        }
    }
}
