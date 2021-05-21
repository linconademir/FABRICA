namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Taxa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_TAXA",
                c => new
                    {
                        COD_IN_TAXA = c.Int(nullable: false, identity: true),
                        DESCRICAO_VC_TAXA = c.String(maxLength: 200, unicode: false),
                        VALOR_MO_TAXA = c.Double(nullable: false),
                        TIPO_VC_TAXA = c.String(maxLength: 60, unicode: false),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_TAXA)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_ESCOLA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_TAXA", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_TAXA", new[] { "COD_IN_ESCOLA" });
            DropTable("dbo.TAB_TAXA");
        }
    }
}
