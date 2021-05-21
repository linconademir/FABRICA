namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoNumero : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_DEBITO", "NUMERO_IN_DEBITO", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_DEBITO", "NUMERO_IN_DEBITO");
        }
    }
}
