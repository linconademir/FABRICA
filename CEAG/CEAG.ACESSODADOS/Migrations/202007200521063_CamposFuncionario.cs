namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposFuncionario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_FUNCIONARIO", "CANCELAMENTO_DT_FUNCIONARIO", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_FUNCIONARIO", "CANCELAMENTO_DT_FUNCIONARIO");
        }
    }
}
