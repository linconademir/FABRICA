namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaFuncionarioDisciplina : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_FUNCIONARIO_DISCIPLINA",
                c => new
                    {
                        COD_IN_FUNCIONARIO_DISCIPLINA = c.Int(nullable: false, identity: true),
                        COD_IN_FUNCIONARIO = c.Int(nullable: false),
                        COD_IN_DISCIPLINA = c.Int(nullable: false),
                        INCLUSAO_DT_FUNCIONARIO = c.DateTime(nullable: false),
                        OBSERVACAO_VC_FUNCIONARIO = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.COD_IN_FUNCIONARIO_DISCIPLINA)
                .ForeignKey("dbo.TAB_DISCIPLINA", t => t.COD_IN_DISCIPLINA)
                .ForeignKey("dbo.TAB_FUNCIONARIO", t => t.COD_IN_FUNCIONARIO)
                .Index(t => t.COD_IN_FUNCIONARIO)
                .Index(t => t.COD_IN_DISCIPLINA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_FUNCIONARIO_DISCIPLINA", "COD_IN_FUNCIONARIO", "dbo.TAB_FUNCIONARIO");
            DropForeignKey("dbo.TAB_FUNCIONARIO_DISCIPLINA", "COD_IN_DISCIPLINA", "dbo.TAB_DISCIPLINA");
            DropIndex("dbo.TAB_FUNCIONARIO_DISCIPLINA", new[] { "COD_IN_DISCIPLINA" });
            DropIndex("dbo.TAB_FUNCIONARIO_DISCIPLINA", new[] { "COD_IN_FUNCIONARIO" });
            DropTable("dbo.TAB_FUNCIONARIO_DISCIPLINA");
        }
    }
}
