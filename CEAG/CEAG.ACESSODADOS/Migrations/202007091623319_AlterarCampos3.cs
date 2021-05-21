namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarCampos3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TELEFONE", "PESSOA_VC_TELEFONE", c => c.String(nullable: false, maxLength: 60, unicode: false));
            AddColumn("dbo.TAB_TELEFONE", "OBSERVACAO_VC_TELEFONE", c => c.String(nullable: false, maxLength: 300, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_TELEFONE", "OBSERVACAO_VC_TELEFONE");
            DropColumn("dbo.TAB_TELEFONE", "PESSOA_VC_TELEFONE");
        }
    }
}
