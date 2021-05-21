namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarCampos5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_QUESTIONARIO", "COD_IN_ESCOLA", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_QUESTIONARIO", "COD_IN_ESCOLA");
        }
    }
}
