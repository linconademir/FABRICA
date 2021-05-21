namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraMatricula : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "IDENTIFICADOR_VC_ALUNO_MATRICULA", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "IDENTIFICADOR_VC_ALUNO_MATRICULA");
        }
    }
}
