namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LiberarAluno : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TAB_ALUNO", "EMAIL_VC_ALUNO", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("dbo.TAB_ALUNO", "MAE_VC_ALUNO", c => c.String(maxLength: 200, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TAB_ALUNO", "MAE_VC_ALUNO", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.TAB_ALUNO", "EMAIL_VC_ALUNO", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
    }
}
