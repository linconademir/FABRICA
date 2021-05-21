namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoMensalidade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_ANTECIPACAO_MO_MENSALIDADE_VALOR", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_ANTECIPACAO_MO_MENSALIDADE_VALOR");
        }
    }
}
