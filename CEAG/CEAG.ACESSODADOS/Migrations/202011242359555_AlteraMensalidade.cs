namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraMensalidade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TAB_MENSALIDADE", "COD_IN_CONTA_BANCARIA", "dbo.TAB_CONTA_BANCARIA");
            DropIndex("dbo.TAB_MENSALIDADE", new[] { "COD_IN_CONTA_BANCARIA" });
            AlterColumn("dbo.TAB_MENSALIDADE", "DESCONTO_MO_MENSALIDADE", c => c.Double());
            DropColumn("dbo.TAB_MENSALIDADE", "COD_IN_CONTA_BANCARIA");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TAB_MENSALIDADE", "COD_IN_CONTA_BANCARIA", c => c.Int(nullable: false));
            AlterColumn("dbo.TAB_MENSALIDADE", "DESCONTO_MO_MENSALIDADE", c => c.Double(nullable: false));
            CreateIndex("dbo.TAB_MENSALIDADE", "COD_IN_CONTA_BANCARIA");
            AddForeignKey("dbo.TAB_MENSALIDADE", "COD_IN_CONTA_BANCARIA", "dbo.TAB_CONTA_BANCARIA", "COD_IN_CONTA_BANCARIA");
        }
    }
}
