namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoADvertencia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_ADVERTENCIA", "MOTIVO_VC_ADVERTENCIA", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_ADVERTENCIA", "MOTIVO_VC_ADVERTENCIA");
        }
    }
}
