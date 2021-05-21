namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlunoMatriculaCampos : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA", name: "INCLUSAO_DT_ALUNO_MATRICLA", newName: "INCLUSAO_DT_ALUNO_MATRICULA");
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA", name: "ANO_IN_ALUNO_MATRICLA", newName: "ANO_IN_ALUNO_MATRICULA");
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA", name: "STATUS_VC_ALUNO_MATRICLA", newName: "STATUS_VC_ALUNO_MATRICULA");
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "MATRICULA_MO_ALUNO_MATRICULA", c => c.Double(nullable: false));
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "MENSALIDADE_MO_ALUNO_MATRICULA", c => c.Double(nullable: false));
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "TAXA_MATERIAL_MO_ALUNO_MATRICULA", c => c.Double(nullable: false));
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "PARCELAS_IN_ALUNO_MATRICULA", c => c.Int(nullable: false));
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "FORMA_PAGAMENTO_VC_ALUNO_MATRICULA", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "OBS_VC_ALUNO_MATRICULA", c => c.String(maxLength: 200, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "OBS_VC_ALUNO_MATRICULA");
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "FORMA_PAGAMENTO_VC_ALUNO_MATRICULA");
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "PARCELAS_IN_ALUNO_MATRICULA");
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "TAXA_MATERIAL_MO_ALUNO_MATRICULA");
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "MENSALIDADE_MO_ALUNO_MATRICULA");
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "MATRICULA_MO_ALUNO_MATRICULA");
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA", name: "STATUS_VC_ALUNO_MATRICULA", newName: "STATUS_VC_ALUNO_MATRICLA");
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA", name: "ANO_IN_ALUNO_MATRICULA", newName: "ANO_IN_ALUNO_MATRICLA");
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA", name: "INCLUSAO_DT_ALUNO_MATRICULA", newName: "INCLUSAO_DT_ALUNO_MATRICLA");
        }
    }
}
