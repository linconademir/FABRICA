namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValorMensalidade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_IRMAO_MO_MENSALIDADE_VALOR", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_IRMAO_MO_MENSALIDADE_VALOR");
        }
    }
}
