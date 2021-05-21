namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Parentesco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_PARENTESCO_ALUNO",
                c => new
                    {
                        COD_IN_PARENTESCO_ALUNO = c.Int(nullable: false, identity: true),
                        COD_IN_PARENTESCO = c.Int(),
                        COD_IN_ALUNO = c.Int(),
                        INCLUSAO_DT_PARENTESCO_ALUNO = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_PARENTESCO_ALUNO)
                .ForeignKey("dbo.TAB_ALUNO", t => t.COD_IN_ALUNO)
                .ForeignKey("dbo.TAB_PARENTESCO", t => t.COD_IN_PARENTESCO)
                .Index(t => t.COD_IN_PARENTESCO)
                .Index(t => t.COD_IN_ALUNO);
            
            CreateTable(
                "dbo.TAB_PARENTESCO",
                c => new
                    {
                        COD_IN_PARENTESCO = c.Int(nullable: false, identity: true),
                        DESCRICAO_VC_PARENTESCO = c.String(nullable: false, maxLength: 100, unicode: false),
                        INCLUSAO_DT_PARENTESCO = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_PARENTESCO);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_PARENTESCO_ALUNO", "COD_IN_PARENTESCO", "dbo.TAB_PARENTESCO");
            DropForeignKey("dbo.TAB_PARENTESCO_ALUNO", "COD_IN_ALUNO", "dbo.TAB_ALUNO");
            DropIndex("dbo.TAB_PARENTESCO_ALUNO", new[] { "COD_IN_ALUNO" });
            DropIndex("dbo.TAB_PARENTESCO_ALUNO", new[] { "COD_IN_PARENTESCO" });
            DropTable("dbo.TAB_PARENTESCO");
            DropTable("dbo.TAB_PARENTESCO_ALUNO");
        }
    }
}
