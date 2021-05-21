namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurmaPeriodo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TURMA", "PERIODO_VC_TURMA", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_TURMA", "PERIODO_VC_TURMA");
        }
    }
}
