namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurmaFuncionarioDisciplina : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA",
                c => new
                    {
                        COD_IN_T_F_D = c.Int(nullable: false, identity: true),
                        INCLUSAO_DT_T_F_D = c.DateTime(nullable: false),
                        CARGA_HORARIA_IN_T_F_D = c.Int(),
                        COD_IN_TURMA = c.Int(nullable: false),
                        COD_IN_DISCIPLINA = c.Int(nullable: false),
                        COD_IN_FUNCIONARIO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_T_F_D)
                .ForeignKey("dbo.TAB_DISCIPLINA", t => t.COD_IN_DISCIPLINA)
                .ForeignKey("dbo.TAB_FUNCIONARIO", t => t.COD_IN_FUNCIONARIO)
                .ForeignKey("dbo.TAB_TURMA", t => t.COD_IN_TURMA)
                .Index(t => t.COD_IN_TURMA)
                .Index(t => t.COD_IN_DISCIPLINA)
                .Index(t => t.COD_IN_FUNCIONARIO);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA", "COD_IN_TURMA", "dbo.TAB_TURMA");
            DropForeignKey("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA", "COD_IN_FUNCIONARIO", "dbo.TAB_FUNCIONARIO");
            DropForeignKey("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA", "COD_IN_DISCIPLINA", "dbo.TAB_DISCIPLINA");
            DropIndex("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA", new[] { "COD_IN_FUNCIONARIO" });
            DropIndex("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA", new[] { "COD_IN_DISCIPLINA" });
            DropIndex("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA", new[] { "COD_IN_TURMA" });
            DropTable("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA");
        }
    }
}
