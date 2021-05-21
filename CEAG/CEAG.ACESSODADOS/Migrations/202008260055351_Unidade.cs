namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unidade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_UNIDADE",
                c => new
                    {
                        COD_IN_UNIDADE = c.Int(nullable: false, identity: true),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                        ANO_IN_UNIDADE = c.Int(nullable: false),
                        NUMERO_IN_UNIDADE = c.Int(nullable: false),
                        ABERTURA_DT_UNIDADE = c.DateTime(nullable: false),
                        INCLUSAO_DT_UNIDADE = c.DateTime(nullable: false),
                        FECHAMENTO_DT_UNIDADE = c.DateTime(),
                    })
                .PrimaryKey(t => t.COD_IN_UNIDADE)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_ESCOLA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_UNIDADE", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_UNIDADE", new[] { "COD_IN_ESCOLA" });
            DropTable("dbo.TAB_UNIDADE");
        }
    }
}
