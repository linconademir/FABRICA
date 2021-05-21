namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarCampos2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_QUESTIONARIO", "TEM_COMPLEMENTO_VC_QUESTIONARIO", c => c.String(nullable: false, maxLength: 3, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_QUESTIONARIO", "TEM_COMPLEMENTO_VC_QUESTIONARIO");
        }
    }
}
