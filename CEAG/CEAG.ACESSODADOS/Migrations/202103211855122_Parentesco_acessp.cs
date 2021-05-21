namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Parentesco_acessp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_PARENTESCO", "COD_IN_ACESSO", c => c.Int(nullable: false));
            AddColumn("dbo.TAB_PARENTESCO", "COD_IN_ESCOLA", c => c.Int());
            CreateIndex("dbo.TAB_PARENTESCO", "COD_IN_ESCOLA");
            AddForeignKey("dbo.TAB_PARENTESCO", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA", "COD_IN_ESCOLA");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_PARENTESCO", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_PARENTESCO", new[] { "COD_IN_ESCOLA" });
            DropColumn("dbo.TAB_PARENTESCO", "COD_IN_ESCOLA");
            DropColumn("dbo.TAB_PARENTESCO", "COD_IN_ACESSO");
        }
    }
}
