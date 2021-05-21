namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaNotasUnidade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_ALUNO_MATRICULA_UNIDADE",
                c => new
                    {
                        COD_IN_A_M_U = c.Int(nullable: false, identity: true),
                        TESTE_IN_A_M_U = c.Int(),
                        PROVA_IN_A_M_U = c.Int(),
                        PC_IN_A_M_U = c.Int(),
                        INCLUSAO_DT_A_M_U = c.DateTime(nullable: false),
                        UNDADE_IN_A_M_U = c.Int(nullable: false),
                        COD_IN_FUNCIONARIO = c.Int(nullable: false),
                        COD_IN_DISCIPLINA = c.Int(nullable: false),
                        COD_IN_ALUNO_MATRICULA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_A_M_U)
                .ForeignKey("dbo.TAB_ALUNO_MATRICULA", t => t.COD_IN_ALUNO_MATRICULA)
                .ForeignKey("dbo.TAB_DISCIPLINA", t => t.COD_IN_DISCIPLINA)
                .ForeignKey("dbo.TAB_FUNCIONARIO", t => t.COD_IN_FUNCIONARIO)
                .Index(t => t.COD_IN_FUNCIONARIO)
                .Index(t => t.COD_IN_DISCIPLINA)
                .Index(t => t.COD_IN_ALUNO_MATRICULA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "COD_IN_FUNCIONARIO", "dbo.TAB_FUNCIONARIO");
            DropForeignKey("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "COD_IN_DISCIPLINA", "dbo.TAB_DISCIPLINA");
            DropForeignKey("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "COD_IN_ALUNO_MATRICULA", "dbo.TAB_ALUNO_MATRICULA");
            DropIndex("dbo.TAB_ALUNO_MATRICULA_UNIDADE", new[] { "COD_IN_ALUNO_MATRICULA" });
            DropIndex("dbo.TAB_ALUNO_MATRICULA_UNIDADE", new[] { "COD_IN_DISCIPLINA" });
            DropIndex("dbo.TAB_ALUNO_MATRICULA_UNIDADE", new[] { "COD_IN_FUNCIONARIO" });
            DropTable("dbo.TAB_ALUNO_MATRICULA_UNIDADE");
        }
    }
}
