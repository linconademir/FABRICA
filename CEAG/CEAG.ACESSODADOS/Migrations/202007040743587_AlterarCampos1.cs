namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarCampos1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "FINAL_MO_VC_ALUNO_MATRICULA", c => c.Double());
            AddColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "RECUPERACAO_MO_A_M_U", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "RECUPERACAO_MO_A_M_U");
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "FINAL_MO_VC_ALUNO_MATRICULA");
        }
    }
}
