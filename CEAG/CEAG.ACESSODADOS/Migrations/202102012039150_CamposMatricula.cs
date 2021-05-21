namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposMatricula : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "DESCONTO_MENSALIDADE_VC_ALUNO_MATRICULA", c => c.String(maxLength: 3, unicode: false));
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "BOLSISTA_VC_ALUNO_MATRICULA", c => c.String(maxLength: 3, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "BOLSISTA_VC_ALUNO_MATRICULA");
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "DESCONTO_MENSALIDADE_VC_ALUNO_MATRICULA");
        }
    }
}
