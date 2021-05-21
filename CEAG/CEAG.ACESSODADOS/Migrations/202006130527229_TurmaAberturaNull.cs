namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurmaAberturaNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TAB_TURMA", "FECHAMENTO_DT_TURMA", c => c.DateTime());
            AlterColumn("dbo.TAB_TURMA", "ABERTURA_DT_TURMA", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TAB_TURMA", "ABERTURA_DT_TURMA", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TAB_TURMA", "FECHAMENTO_DT_TURMA", c => c.DateTime(nullable: false));
        }
    }
}
