namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaFuncionario1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_FUNCIONARIO", "COD_IN_ESCOLA", c => c.Int(nullable: false));
            CreateIndex("dbo.TAB_FUNCIONARIO", "COD_IN_ESCOLA");
            AddForeignKey("dbo.TAB_FUNCIONARIO", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA", "COD_IN_ESCOLA");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_FUNCIONARIO", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_FUNCIONARIO", new[] { "COD_IN_ESCOLA" });
            DropColumn("dbo.TAB_FUNCIONARIO", "COD_IN_ESCOLA");
        }
    }
}
