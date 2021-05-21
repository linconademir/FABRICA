namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PontoAtencaoCodAcesso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_PONTO_ATENCAO", "COD_IN_ACESSO", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_PONTO_ATENCAO", "COD_IN_ACESSO");
        }
    }
}
