namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurmaNivel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TURMA", "NIVEL_VC_TURMA", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_TURMA", "NIVEL_VC_TURMA");
        }
    }
}
