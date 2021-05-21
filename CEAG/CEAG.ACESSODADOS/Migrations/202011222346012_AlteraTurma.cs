namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraTurma : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TURMA", "DESCONTO_MO_TURMA", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_TURMA", "DESCONTO_MO_TURMA");
        }
    }
}
