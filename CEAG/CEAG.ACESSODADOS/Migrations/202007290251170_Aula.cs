namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aula : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_AULA",
                c => new
                    {
                        COD_IN_AULA = c.Int(nullable: false, identity: true),
                        REALIZADA_DT_AULA = c.DateTime(nullable: false),
                        INCLUSAO_DT_AULA = c.DateTime(nullable: false),
                        CANCELAMENTO_DT_AULA = c.DateTime(),
                        OBSERVACAO_VC_AULA = c.String(maxLength: 500, unicode: false),
                        ASSUNTO_VC_AULA = c.String(maxLength: 500, unicode: false),
                        COD_IN_TURMA = c.Int(nullable: false),
                        COD_IN_DISCIPLINA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_AULA)
                .ForeignKey("dbo.TAB_DISCIPLINA", t => t.COD_IN_DISCIPLINA)
                .ForeignKey("dbo.TAB_TURMA", t => t.COD_IN_TURMA)
                .Index(t => t.COD_IN_TURMA)
                .Index(t => t.COD_IN_DISCIPLINA);
            
            CreateTable(
                "dbo.TAB_AULA_ALUNO",
                c => new
                    {
                        COD_IN_AULA_ALUNO = c.Int(nullable: false, identity: true),
                        INCLUSAO_DT_AULA_ALUNO = c.DateTime(nullable: false),
                        SITUACAO_VC_AULA_ALUNO = c.String(nullable: false, maxLength: 20, unicode: false),
                        OBSERVACAO_VC_AULA_ALUNO = c.String(maxLength: 500, unicode: false),
                        COD_IN_ALUNO_MATRICULA = c.Int(nullable: false),
                        COD_IN_AULA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_AULA_ALUNO)
                .ForeignKey("dbo.TAB_ALUNO_MATRICULA", t => t.COD_IN_ALUNO_MATRICULA)
                .ForeignKey("dbo.TAB_AULA", t => t.COD_IN_AULA)
                .Index(t => t.COD_IN_ALUNO_MATRICULA)
                .Index(t => t.COD_IN_AULA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_AULA", "COD_IN_TURMA", "dbo.TAB_TURMA");
            DropForeignKey("dbo.TAB_AULA", "COD_IN_DISCIPLINA", "dbo.TAB_DISCIPLINA");
            DropForeignKey("dbo.TAB_AULA_ALUNO", "COD_IN_AULA", "dbo.TAB_AULA");
            DropForeignKey("dbo.TAB_AULA_ALUNO", "COD_IN_ALUNO_MATRICULA", "dbo.TAB_ALUNO_MATRICULA");
            DropIndex("dbo.TAB_AULA_ALUNO", new[] { "COD_IN_AULA" });
            DropIndex("dbo.TAB_AULA_ALUNO", new[] { "COD_IN_ALUNO_MATRICULA" });
            DropIndex("dbo.TAB_AULA", new[] { "COD_IN_DISCIPLINA" });
            DropIndex("dbo.TAB_AULA", new[] { "COD_IN_TURMA" });
            DropTable("dbo.TAB_AULA_ALUNO");
            DropTable("dbo.TAB_AULA");
        }
    }
}
