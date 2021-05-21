namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValorMensalidade1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_DESCONTO_MENSAL_MO_MENSALIDADE_VALOR", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_DESCONTO_MENSAL_MO_MENSALIDADE_VALOR", c => c.Double(nullable: false));
        }
    }
}
