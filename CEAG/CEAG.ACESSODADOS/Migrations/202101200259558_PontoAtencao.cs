namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PontoAtencao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_PONTO_ATENCAO",
                c => new
                    {
                        COD_IN_PONTO_ATENCAO = c.Int(nullable: false, identity: true),
                        TIPO_VC_PONTO_ATENCAO = c.String(nullable: false, maxLength: 60, unicode: false),
                        DESCRICAO_VC_PONTO_ATENCAO = c.String(nullable: false, maxLength: 500, unicode: false),
                        REFERENCIA_IN_PONTO_ATENCAO = c.Int(nullable: false),
                        RESOLUCAO_DT_PONTO_ATENCAO = c.DateTime(),
                        INCLUSAO_DT_PONTO_ATENCAO = c.DateTime(nullable: false),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_PONTO_ATENCAO)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_ESCOLA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_PONTO_ATENCAO", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_PONTO_ATENCAO", new[] { "COD_IN_ESCOLA" });
            DropTable("dbo.TAB_PONTO_ATENCAO");
        }
    }
}
