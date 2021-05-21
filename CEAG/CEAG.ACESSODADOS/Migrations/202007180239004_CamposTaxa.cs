namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposTaxa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TAXA", "CANCELAMENTO_DT_TAXA", c => c.DateTime());
            AddColumn("dbo.TAB_TAXA", "INCLUSAO_DT_TAXA", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_TAXA", "INCLUSAO_DT_TAXA");
            DropColumn("dbo.TAB_TAXA", "CANCELAMENTO_DT_TAXA");
        }
    }
}
