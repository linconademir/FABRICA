namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovoCampoQuestionario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_QUESTIONARIO", "PERGUNTA_COMPLEMENTO_VC_QUESTIONARIO", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_QUESTIONARIO", "PERGUNTA_COMPLEMENTO_VC_QUESTIONARIO");
        }
    }
}
