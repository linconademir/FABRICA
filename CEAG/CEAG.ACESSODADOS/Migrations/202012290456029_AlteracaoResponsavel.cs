namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoResponsavel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_RESPONSAVEL", "RECEBE_EMAIL_VC_RESPONSAVEL", c => c.String(maxLength: 3, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_RESPONSAVEL", "RECEBE_EMAIL_VC_RESPONSAVEL");
        }
    }
}
