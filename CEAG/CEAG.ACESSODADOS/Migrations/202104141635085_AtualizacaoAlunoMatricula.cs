namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizacaoAlunoMatricula : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TAB_ALUNO_MATRICULA", "BOLSISTA_VC_ALUNO_MATRICULA", c => c.String(maxLength: 15, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TAB_ALUNO_MATRICULA", "BOLSISTA_VC_ALUNO_MATRICULA", c => c.String(maxLength: 3, unicode: false));
        }
    }
}
