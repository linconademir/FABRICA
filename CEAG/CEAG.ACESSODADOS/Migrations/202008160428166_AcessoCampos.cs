namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcessoCampos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_ACESSO", "COD_IN_FUNCIONARIO", c => c.Int());
            CreateIndex("dbo.TAB_ACESSO", "COD_IN_FUNCIONARIO");
            AddForeignKey("dbo.TAB_ACESSO", "COD_IN_FUNCIONARIO", "dbo.TAB_FUNCIONARIO", "COD_IN_FUNCIONARIO");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_ACESSO", "COD_IN_FUNCIONARIO", "dbo.TAB_FUNCIONARIO");
            DropIndex("dbo.TAB_ACESSO", new[] { "COD_IN_FUNCIONARIO" });
            DropColumn("dbo.TAB_ACESSO", "COD_IN_FUNCIONARIO");
        }
    }
}
