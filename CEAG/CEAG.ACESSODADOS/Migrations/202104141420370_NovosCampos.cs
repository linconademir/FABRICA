namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovosCampos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_TAXA_MATERIAL_MO_MENSALIDADE_VALOR", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_MENSALIDADE_VALOR", "VALOR_TAXA_MATERIAL_MO_MENSALIDADE_VALOR");
        }
    }
}
