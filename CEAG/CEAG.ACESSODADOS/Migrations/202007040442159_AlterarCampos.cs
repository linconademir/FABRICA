namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarCampos : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA_UNIDADE", name: "TESTE_IN_A_M_U", newName: "TESTE_MO_A_M_U");
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA_UNIDADE", name: "PROVA_IN_A_M_U", newName: "PROVA_MO_A_M_U");
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA_UNIDADE", name: "PC_IN_A_M_U", newName: "PC_MO_A_M_U");
            AlterColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "TESTE_MO_A_M_U", c => c.Double());
            AlterColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "PROVA_MO_A_M_U", c => c.Double());
            AlterColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "PC_MO_A_M_U", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "PC_MO_A_M_U", c => c.Int());
            AlterColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "PROVA_MO_A_M_U", c => c.Int());
            AlterColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "TESTE_MO_A_M_U", c => c.Int());
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA_UNIDADE", name: "PC_MO_A_M_U", newName: "PC_IN_A_M_U");
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA_UNIDADE", name: "PROVA_MO_A_M_U", newName: "PROVA_IN_A_M_U");
            RenameColumn(table: "dbo.TAB_ALUNO_MATRICULA_UNIDADE", name: "TESTE_MO_A_M_U", newName: "TESTE_IN_A_M_U");
        }
    }
}
