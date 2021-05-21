namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MensalidadeCampos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_DESCONTO_MENSAL_MO_MENSALIDADE_VALOR", c => c.Double(nullable: false));
            AddColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_ANUAL_VISTA_MO_MENSALIDADE_VALOR", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_ANUAL_VISTA_MO_MENSALIDADE_VALOR");
            DropColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_DESCONTO_MENSAL_MO_MENSALIDADE_VALOR");
        }
    }
}
