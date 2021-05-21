namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EscolaLogradouro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_ESCOLA", "COD_IN_LOGRADOURO", c => c.Int());
            CreateIndex("dbo.TAB_ESCOLA", "COD_IN_LOGRADOURO");
            AddForeignKey("dbo.TAB_ESCOLA", "COD_IN_LOGRADOURO", "dbo.TAB_LOGRADOURO", "COD_IN_LOGRADOURO");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_ESCOLA", "COD_IN_LOGRADOURO", "dbo.TAB_LOGRADOURO");
            DropIndex("dbo.TAB_ESCOLA", new[] { "COD_IN_LOGRADOURO" });
            DropColumn("dbo.TAB_ESCOLA", "COD_IN_LOGRADOURO");
        }
    }
}
