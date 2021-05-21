namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraDebito : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_DEBITO", "IDENTIFICADOR_VC_DEBITO", c => c.String(maxLength: 200, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_DEBITO", "IDENTIFICADOR_VC_DEBITO");
        }
    }
}
