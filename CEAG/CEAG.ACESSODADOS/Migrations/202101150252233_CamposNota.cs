namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposNota : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "CONSELHO_IN_A_M_U", c => c.Double());
            AddColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "FECHAMENTO_DT_A_M_U", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "FECHAMENTO_DT_A_M_U");
            DropColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "CONSELHO_IN_A_M_U");
        }
    }
}
