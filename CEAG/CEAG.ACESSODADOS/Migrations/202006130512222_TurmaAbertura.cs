namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurmaAbertura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TURMA", "FECHAMENTO_DT_TURMA", c => c.DateTime(nullable: false));
            AddColumn("dbo.TAB_TURMA", "ABERTURA_DT_TURMA", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_TURMA", "ABERTURA_DT_TURMA");
            DropColumn("dbo.TAB_TURMA", "FECHAMENTO_DT_TURMA");
        }
    }
}
