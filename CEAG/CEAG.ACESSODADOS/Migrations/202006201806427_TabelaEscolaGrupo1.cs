namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaEscolaGrupo1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_ACESSO", "COD_IN_ESCOLA", c => c.Int(nullable: true));
            AddColumn("dbo.TAB_ACESSO", "COD_IN_ESCOLA_GRUPO", c => c.Int(nullable: true));
            CreateIndex("dbo.TAB_ACESSO", "COD_IN_ESCOLA");
            CreateIndex("dbo.TAB_ACESSO", "COD_IN_ESCOLA_GRUPO");
            AddForeignKey("dbo.TAB_ACESSO", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA", "COD_IN_ESCOLA");
            AddForeignKey("dbo.TAB_ACESSO", "COD_IN_ESCOLA_GRUPO", "dbo.TAB_ESCOLA_GRUPO", "COD_IN_ESCOLA_GRUPO");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_ACESSO", "COD_IN_ESCOLA_GRUPO", "dbo.TAB_ESCOLA_GRUPO");
            DropForeignKey("dbo.TAB_ACESSO", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_ACESSO", new[] { "COD_IN_ESCOLA_GRUPO" });
            DropIndex("dbo.TAB_ACESSO", new[] { "COD_IN_ESCOLA" });
            DropColumn("dbo.TAB_ACESSO", "COD_IN_ESCOLA_GRUPO");
            DropColumn("dbo.TAB_ACESSO", "COD_IN_ESCOLA");
        }
    }
}
