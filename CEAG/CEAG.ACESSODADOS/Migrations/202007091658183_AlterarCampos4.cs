namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarCampos4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TAB_TELEFONE", "OBSERVACAO_VC_TELEFONE", c => c.String(maxLength: 300, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TAB_TELEFONE", "OBSERVACAO_VC_TELEFONE", c => c.String(nullable: false, maxLength: 300, unicode: false));
        }
    }
}
