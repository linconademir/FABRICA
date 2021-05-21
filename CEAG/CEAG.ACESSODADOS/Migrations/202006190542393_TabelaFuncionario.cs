namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaFuncionario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_FUNCIONARIO",
                c => new
                    {
                        COD_IN_FUNCIONARIO = c.Int(nullable: false, identity: true),
                        NOME_VC_FUNCIONARIO = c.String(nullable: false, maxLength: 200, unicode: false),
                        CPF_VC_FUNCIONARIO = c.String(maxLength: 11, unicode: false),
                        SEXO_VC_FUNCIONARIO = c.String(nullable: false, maxLength: 15, unicode: false),
                        RG_VC_FUNCIONARIO = c.String(maxLength: 30, unicode: false),
                        FUNCAO_VC_FUNCIONARIO = c.String(maxLength: 150, unicode: false),
                        TITULACAO_VC_FUNCIONARIO = c.String(maxLength: 60, unicode: false),
                        OBSERVACAO_VC_FUNCIONARIO = c.String(maxLength: 300, unicode: false),
                        NACIONALIDADE_VC_FUNCIONARIO = c.String(maxLength: 60, unicode: false),
                        NATURALIDADE_VC_FUNCIONARIO = c.String(maxLength: 60, unicode: false),
                        RG_ORGAO_VC_FUNCIONARIO = c.String(maxLength: 5, unicode: false),
                        RG_UF_VC_FUNCIONARIO = c.String(maxLength: 2, unicode: false),
                        NASCIMENTO_DT_FUNCIONARIO = c.DateTime(nullable: false),
                        INCLUSAO_DT_FUNCIONARIO = c.DateTime(nullable: false),
                        EMAIL_VC_FUNCIONARIO = c.String(maxLength: 150, unicode: false),
                        COD_IN_LOGRADOURO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_FUNCIONARIO)
                .ForeignKey("dbo.TAB_LOGRADOURO", t => t.COD_IN_LOGRADOURO)
                .Index(t => t.COD_IN_LOGRADOURO);
            
            AddColumn("dbo.TAB_TELEFONE", "COD_IN_FUNCIONARIO", c => c.Int(nullable: true));
            CreateIndex("dbo.TAB_TELEFONE", "COD_IN_FUNCIONARIO");
            AddForeignKey("dbo.TAB_TELEFONE", "COD_IN_FUNCIONARIO", "dbo.TAB_FUNCIONARIO", "COD_IN_FUNCIONARIO");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_TELEFONE", "COD_IN_FUNCIONARIO", "dbo.TAB_FUNCIONARIO");
            DropForeignKey("dbo.TAB_FUNCIONARIO", "COD_IN_LOGRADOURO", "dbo.TAB_LOGRADOURO");
            DropIndex("dbo.TAB_TELEFONE", new[] { "COD_IN_FUNCIONARIO" });
            DropIndex("dbo.TAB_FUNCIONARIO", new[] { "COD_IN_LOGRADOURO" });
            DropColumn("dbo.TAB_TELEFONE", "COD_IN_FUNCIONARIO");
            DropTable("dbo.TAB_FUNCIONARIO");
        }
    }
}
