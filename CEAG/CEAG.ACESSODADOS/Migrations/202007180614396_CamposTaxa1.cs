namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposTaxa1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TAXA", "VIAS_IN_TAXA", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_TAXA", "VIAS_IN_TAXA");
        }
    }
}
