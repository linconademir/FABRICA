namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposHorarioTurma : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA", "DIA_SEMANA_IN_T_F_D", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA", "DIA_SEMANA_IN_T_F_D");
        }
    }
}
