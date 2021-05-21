namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposResponsavel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_RESPONSAVEL", "SEXO_VC_RESPONSAVEL", c => c.String(maxLength: 30, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_RESPONSAVEL", "SEXO_VC_RESPONSAVEL");
        }
    }
}
