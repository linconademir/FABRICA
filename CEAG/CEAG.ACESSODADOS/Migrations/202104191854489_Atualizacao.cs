namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualizacao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TAB_DEBITO", "OBSERVACAO_VC_DEBITO", c => c.String(nullable: false, maxLength: 800, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TAB_DEBITO", "OBSERVACAO_VC_DEBITO", c => c.String(nullable: false, maxLength: 500, unicode: false));
        }
    }
}
