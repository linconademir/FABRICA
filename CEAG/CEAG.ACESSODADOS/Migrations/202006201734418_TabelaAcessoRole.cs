namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaAcessoRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_ACESSO_ROLE",
                c => new
                    {
                        COD_IN_ACESSO_ROLE = c.Int(nullable: false, identity: true),
                        COD_IN_ROLE = c.Int(nullable: false),
                        COD_IN_ACESSO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_ACESSO_ROLE)
                .ForeignKey("dbo.TAB_ACESSO", t => t.COD_IN_ACESSO)
                .ForeignKey("dbo.TAB_ROLE", t => t.COD_IN_ROLE)
                .Index(t => t.COD_IN_ROLE)
                .Index(t => t.COD_IN_ACESSO);
            
            CreateTable(
                "dbo.TAB_ROLE",
                c => new
                    {
                        COD_IN_ROLE = c.Int(nullable: false, identity: true),
                        DESCRICAO_VC_ROLE = c.String(nullable: false, maxLength: 60, unicode: false),
                    })
                .PrimaryKey(t => t.COD_IN_ROLE);
            
            AddColumn("dbo.TAB_ACESSO", "NOME_VC_ACESSO", c => c.String(nullable: false, maxLength: 60, unicode: false));
            AddColumn("dbo.TAB_ACESSO", "SOBRENOME_VC_ACESSO", c => c.String(nullable: false, maxLength: 60, unicode: false));
            AddColumn("dbo.TAB_ACESSO", "FOTO_VC_ACESSO", c => c.String(nullable: false, maxLength: 200, unicode: false));
            DropColumn("dbo.TAB_ACESSO", "PERFIL_VC_ACESSO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TAB_ACESSO", "PERFIL_VC_ACESSO", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropForeignKey("dbo.TAB_ACESSO_ROLE", "COD_IN_ROLE", "dbo.TAB_ROLE");
            DropForeignKey("dbo.TAB_ACESSO_ROLE", "COD_IN_ACESSO", "dbo.TAB_ACESSO");
            DropIndex("dbo.TAB_ACESSO_ROLE", new[] { "COD_IN_ACESSO" });
            DropIndex("dbo.TAB_ACESSO_ROLE", new[] { "COD_IN_ROLE" });
            DropColumn("dbo.TAB_ACESSO", "FOTO_VC_ACESSO");
            DropColumn("dbo.TAB_ACESSO", "SOBRENOME_VC_ACESSO");
            DropColumn("dbo.TAB_ACESSO", "NOME_VC_ACESSO");
            DropTable("dbo.TAB_ROLE");
            DropTable("dbo.TAB_ACESSO_ROLE");
        }
    }
}
