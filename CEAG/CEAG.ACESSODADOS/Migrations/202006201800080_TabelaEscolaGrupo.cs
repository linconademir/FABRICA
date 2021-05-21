namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaEscolaGrupo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_ESCOLA_GRUPO",
                c => new
                    {
                        COD_IN_ESCOLA_GRUPO = c.Int(nullable: false, identity: true),
                        RAZAO_VC_ESCOLA_GRUPO = c.String(maxLength: 200, unicode: false),
                        FANTASIA_VC_ESCOLA_GRUPO = c.String(maxLength: 200, unicode: false),
                        CNPJ_VC_ESCOLA_GRUPO = c.String(maxLength: 14, unicode: false),
                        LOGO_VC_ESCOLA_GRUPO = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.COD_IN_ESCOLA_GRUPO);
            
            AddColumn("dbo.TAB_ESCOLA", "LOGO_VC_ESCOLA", c => c.String(maxLength: 200, unicode: false));
            AddColumn("dbo.TAB_ESCOLA", "COD_IN_ESCOLA_GRUPO", c => c.Int(nullable: true));
            CreateIndex("dbo.TAB_ESCOLA", "COD_IN_ESCOLA_GRUPO");
            AddForeignKey("dbo.TAB_ESCOLA", "COD_IN_ESCOLA_GRUPO", "dbo.TAB_ESCOLA_GRUPO", "COD_IN_ESCOLA_GRUPO");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_ESCOLA", "COD_IN_ESCOLA_GRUPO", "dbo.TAB_ESCOLA_GRUPO");
            DropIndex("dbo.TAB_ESCOLA", new[] { "COD_IN_ESCOLA_GRUPO" });
            DropColumn("dbo.TAB_ESCOLA", "COD_IN_ESCOLA_GRUPO");
            DropColumn("dbo.TAB_ESCOLA", "LOGO_VC_ESCOLA");
            DropTable("dbo.TAB_ESCOLA_GRUPO");
        }
    }
}
