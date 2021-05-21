namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoDebito : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_DEBITO", "QRCODE_VC_DEBITO", c => c.String(maxLength: 800, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_DEBITO", "QRCODE_VC_DEBITO");
        }
    }
}
