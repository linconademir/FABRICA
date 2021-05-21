namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resolucaoAcesso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_PONTO_ATENCAO", "COD_IN_ACESSO_RESOLUCAO", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_PONTO_ATENCAO", "COD_IN_ACESSO_RESOLUCAO");
        }
    }
}
