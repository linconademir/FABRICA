namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaDisciplina : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_DISCIPLINA",
                c => new
                    {
                        COD_IN_DISCIPLINA = c.Int(nullable: false, identity: true),
                        DESCRICAO_VC_DISCIPLINA = c.String(nullable: false, maxLength: 200, unicode: false),
                        TIPO_VC_DISCIPLINA = c.String(nullable: false, maxLength: 200, unicode: false),
                        INCLUSAO_VC_DISCIPLINA = c.DateTime(nullable: false),
                        OBRIGATORIA_VC_DISCIPLINA = c.String(nullable: false, maxLength: 3, unicode: false),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_DISCIPLINA)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_ESCOLA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_DISCIPLINA", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_DISCIPLINA", new[] { "COD_IN_ESCOLA" });
            DropTable("dbo.TAB_DISCIPLINA");
        }
    }
}
