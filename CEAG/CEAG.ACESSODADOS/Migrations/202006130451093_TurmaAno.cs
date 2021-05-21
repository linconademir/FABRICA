namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurmaAno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TURMA", "VAGAS_IN_TURMA", c => c.Int(nullable: false));
            AddColumn("dbo.TAB_TURMA", "ANO_LETIVO_IN_TURMA", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_TURMA", "ANO_LETIVO_IN_TURMA");
            DropColumn("dbo.TAB_TURMA", "VAGAS_IN_TURMA");
        }
    }
}
