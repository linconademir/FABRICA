namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Naturalidade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_ALUNO", "NACIONALIDADE_VC_ALUNO", c => c.String(maxLength: 60, unicode: false));
            AddColumn("dbo.TAB_ALUNO", "NATURALIDADE_VC_ALUNO", c => c.String(maxLength: 60, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_ALUNO", "NATURALIDADE_VC_ALUNO");
            DropColumn("dbo.TAB_ALUNO", "NACIONALIDADE_VC_ALUNO");
        }
    }
}
