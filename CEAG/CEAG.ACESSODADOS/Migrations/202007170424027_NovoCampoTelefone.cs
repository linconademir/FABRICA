namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovoCampoTelefone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TELEFONE", "TIPO_CONTATO_VC_TELEFONE", c => c.String(nullable: false, maxLength: 60, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_TELEFONE", "TIPO_CONTATO_VC_TELEFONE");
        }
    }
}
