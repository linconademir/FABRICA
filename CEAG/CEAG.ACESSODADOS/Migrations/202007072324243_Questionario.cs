namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Questionario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_ALUNO_QUESTIONARIO",
                c => new
                    {
                        COD_IN_ALUNO_QUESTIONARIO = c.Int(nullable: false, identity: true),
                        COD_IN_ALUNO = c.Int(nullable: false),
                        COD_IN_QUESTIONARIO = c.Int(nullable: false),
                        RESPOSTA_VC_ALUNO_QUESTIONARIO = c.String(nullable: false, maxLength: 3, unicode: false),
                        COMPLEMENTO_VC_ALUNO_QUESTIONARIO = c.String(maxLength: 200, unicode: false),
                        INCLUSAO_DT_ALUNO_QUESTIONARIO = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_ALUNO_QUESTIONARIO)
                .ForeignKey("dbo.TAB_ALUNO", t => t.COD_IN_ALUNO)
                .ForeignKey("dbo.TAB_QUESTIONARIO", t => t.COD_IN_QUESTIONARIO)
                .Index(t => t.COD_IN_ALUNO)
                .Index(t => t.COD_IN_QUESTIONARIO);
            
            CreateTable(
                "dbo.TAB_QUESTIONARIO",
                c => new
                    {
                        COD_IN_QUESTIONARIO = c.Int(nullable: false, identity: true),
                        DESCRICAO_VC_QUESTIONARIO = c.String(nullable: false, maxLength: 200, unicode: false),
                        TIPO_VC_QUESTIONARIO = c.String(nullable: false, maxLength: 60, unicode: false),
                    })
                .PrimaryKey(t => t.COD_IN_QUESTIONARIO);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_ALUNO_QUESTIONARIO", "COD_IN_QUESTIONARIO", "dbo.TAB_QUESTIONARIO");
            DropForeignKey("dbo.TAB_ALUNO_QUESTIONARIO", "COD_IN_ALUNO", "dbo.TAB_ALUNO");
            DropIndex("dbo.TAB_ALUNO_QUESTIONARIO", new[] { "COD_IN_QUESTIONARIO" });
            DropIndex("dbo.TAB_ALUNO_QUESTIONARIO", new[] { "COD_IN_ALUNO" });
            DropTable("dbo.TAB_QUESTIONARIO");
            DropTable("dbo.TAB_ALUNO_QUESTIONARIO");
        }
    }
}
