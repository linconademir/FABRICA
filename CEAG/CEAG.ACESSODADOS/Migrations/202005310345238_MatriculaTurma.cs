namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatriculaTurma : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_ALUNO_MATRICULA",
                c => new
                    {
                        COD_IN_ALUNO_MATRICULA = c.Int(nullable: false, identity: true),
                        INCLUSAO_DT_ALUNO_MATRICLA = c.DateTime(nullable: false),
                        COD_IN_ALUNO = c.Int(nullable: false),
                        COD_IN_TURMA = c.Int(nullable: false),
                        ANO_IN_ALUNO_MATRICLA = c.Int(nullable: false),
                        STATUS_VC_ALUNO_MATRICLA = c.String(maxLength: 60, unicode: false),
                    })
                .PrimaryKey(t => t.COD_IN_ALUNO_MATRICULA)
                .ForeignKey("dbo.TAB_ALUNO", t => t.COD_IN_ALUNO)
                .ForeignKey("dbo.TAB_TURMA", t => t.COD_IN_TURMA)
                .Index(t => t.COD_IN_ALUNO)
                .Index(t => t.COD_IN_TURMA);
            
            CreateTable(
                "dbo.TAB_TURMA",
                c => new
                    {
                        COD_IN_TURMA = c.Int(nullable: false, identity: true),
                        DESCRICAO_VC_TURMA = c.String(nullable: false, maxLength: 160, unicode: false),
                        SEGMENTO_VC_TURMA = c.String(maxLength: 160, unicode: false),
                        PORTARIA_VC_TURMA = c.String(maxLength: 160, unicode: false),
                        INCLUSAO_DT_TURMA = c.DateTime(nullable: false),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_TURMA)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_ESCOLA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_ALUNO_MATRICULA", "COD_IN_TURMA", "dbo.TAB_TURMA");
            DropForeignKey("dbo.TAB_ALUNO_MATRICULA", "COD_IN_ALUNO", "dbo.TAB_ALUNO");
            DropForeignKey("dbo.TAB_TURMA", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_TURMA", new[] { "COD_IN_ESCOLA" });
            DropIndex("dbo.TAB_ALUNO_MATRICULA", new[] { "COD_IN_TURMA" });
            DropIndex("dbo.TAB_ALUNO_MATRICULA", new[] { "COD_IN_ALUNO" });
            DropTable("dbo.TAB_TURMA");
            DropTable("dbo.TAB_ALUNO_MATRICULA");
        }
    }
}
