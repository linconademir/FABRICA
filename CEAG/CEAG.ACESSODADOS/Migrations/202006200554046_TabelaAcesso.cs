namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaAcesso : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_ACESSO",
                c => new
                    {
                        COD_IN_ACESSO = c.Int(nullable: false, identity: true),
                        EMAIL_VC_ACESSO = c.String(nullable: false, maxLength: 200, unicode: false),
                        SENHA_VC_ACESSO = c.String(nullable: false, maxLength: 200, unicode: false),
                        ATIVO_VC_ACESSO = c.String(nullable: false, maxLength: 3, unicode: false),
                        PERFIL_VC_ACESSO = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.COD_IN_ACESSO);
            
            AddColumn("dbo.TAB_FUNCIONARIO", "COD_IN_ACESSO", c => c.Int(nullable: true));
            CreateIndex("dbo.TAB_FUNCIONARIO", "COD_IN_ACESSO");
            AddForeignKey("dbo.TAB_FUNCIONARIO", "COD_IN_ACESSO", "dbo.TAB_ACESSO", "COD_IN_ACESSO");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_FUNCIONARIO", "COD_IN_ACESSO", "dbo.TAB_ACESSO");
            DropIndex("dbo.TAB_FUNCIONARIO", new[] { "COD_IN_ACESSO" });
            DropColumn("dbo.TAB_FUNCIONARIO", "COD_IN_ACESSO");
            DropTable("dbo.TAB_ACESSO");
        }
    }
}
