namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposFuncionario1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TAB_FUNCIONARIO_DISCIPLINA", name: "INCLUSAO_DT_FUNCIONARIO", newName: "INCLUSAO_DT_FUNCIONARIO_DISCIPLINA");
            RenameColumn(table: "dbo.TAB_FUNCIONARIO_DISCIPLINA", name: "OBSERVACAO_VC_FUNCIONARIO", newName: "OBSERVACAO_VC_FUNCIONARIO_DISCIPLINA");
            AddColumn("dbo.TAB_FUNCIONARIO_DISCIPLINA", "CANCELAMENTO_DT_FUNCIONARIO_DISCIPLINA", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_FUNCIONARIO_DISCIPLINA", "CANCELAMENTO_DT_FUNCIONARIO_DISCIPLINA");
            RenameColumn(table: "dbo.TAB_FUNCIONARIO_DISCIPLINA", name: "OBSERVACAO_VC_FUNCIONARIO_DISCIPLINA", newName: "OBSERVACAO_VC_FUNCIONARIO");
            RenameColumn(table: "dbo.TAB_FUNCIONARIO_DISCIPLINA", name: "INCLUSAO_DT_FUNCIONARIO_DISCIPLINA", newName: "INCLUSAO_DT_FUNCIONARIO");
        }
    }
}
