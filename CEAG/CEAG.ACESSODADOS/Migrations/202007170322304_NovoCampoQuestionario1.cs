namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovoCampoQuestionario1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_QUESTIONARIO", "ORDEM_IN_QUESTIONARIO", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_QUESTIONARIO", "ORDEM_IN_QUESTIONARIO");
        }
    }
}
