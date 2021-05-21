namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MensalidadeValor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_MENSALIDADE_VALOR",
                c => new
                    {
                        COD_IN_MENSALIDADE_VALOR = c.Int(nullable: false, identity: true),
                        TURMA_VC_MENSALIDADE_VALOR = c.String(maxLength: 200, unicode: false),
                        VALOR_MENSAL_MO_MENSALIDADE_VALOR = c.Double(nullable: false),
                        VALOR_ANUAL_MO_MENSALIDADE_VALOR = c.Double(nullable: false),
                        TIPO_VC_MENSALIDADE_VALOR = c.String(maxLength: 60, unicode: false),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_MENSALIDADE_VALOR)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_ESCOLA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_MENSALIDADE_VALOR", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_MENSALIDADE_VALOR", new[] { "COD_IN_ESCOLA" });
            DropTable("dbo.TAB_MENSALIDADE_VALOR");
        }
    }
}
