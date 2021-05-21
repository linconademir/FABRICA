namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvertenciaMensagem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_ADVERTENCIA",
                c => new
                    {
                        COD_IN_ADVERTENCIA = c.Int(nullable: false, identity: true),
                        TIPO_VC_ADVERTENCIA = c.String(nullable: false, maxLength: 100, unicode: false),
                        TITULO_VC_ADVERTENCIA = c.String(nullable: false, maxLength: 200, unicode: false),
                        DESCRICAO_VC_ADVERTENCIA = c.String(nullable: false, maxLength: 800, unicode: false),
                        URGENCIA_VC_ADVERTENCIA = c.String(nullable: false, maxLength: 60, unicode: false),
                        INCLUSAO_DT_ADVERTENCIA = c.DateTime(nullable: false),
                        COMPARECIMENTO_DT_ADVERTENCIA = c.DateTime(),
                        COD_IN_ACESSO = c.Int(),
                        RESOLUCAO_DT_ADVERTENCIA = c.DateTime(),
                        COD_ALUNO_MATRICULA = c.Int(nullable: false),
                        COD_AULA = c.Int(),
                    })
                .PrimaryKey(t => t.COD_IN_ADVERTENCIA)
                .ForeignKey("dbo.TAB_ALUNO_MATRICULA", t => t.COD_ALUNO_MATRICULA)
                .ForeignKey("dbo.TAB_AULA", t => t.COD_AULA)
                .Index(t => t.COD_ALUNO_MATRICULA)
                .Index(t => t.COD_AULA);
            
            CreateTable(
                "dbo.TAB_MENSAGEM",
                c => new
                    {
                        COD_IN_MENSAGEM = c.Int(nullable: false, identity: true),
                        TITULO_VC_MENSAGEM = c.String(nullable: false, maxLength: 200, unicode: false),
                        DESCRICAO_VC_MENSAGEM = c.String(nullable: false, maxLength: 800, unicode: false),
                        STATUS_VC_MENSAGEM = c.String(nullable: false, maxLength: 30, unicode: false),
                        INCLUSAO_DT_MENSAGEM = c.DateTime(nullable: false),
                        ENVIO_DT_MENSAGEM = c.DateTime(),
                        DESTINATARIO_VC_MENSAGEM = c.String(maxLength: 300, unicode: false),
                        DESTINATARIO_COPIA_VC_MENSAGEM = c.String(maxLength: 300, unicode: false),
                        TIPO_VC_MENSAGEM = c.String(nullable: false, maxLength: 30, unicode: false),
                        PRIORIDADE_IN_MENSAGEM = c.Int(),
                        COD_ALUNO_MATRICULA = c.Int(),
                    })
                .PrimaryKey(t => t.COD_IN_MENSAGEM)
                .ForeignKey("dbo.TAB_ALUNO_MATRICULA", t => t.COD_ALUNO_MATRICULA)
                .Index(t => t.COD_ALUNO_MATRICULA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_MENSAGEM", "COD_ALUNO_MATRICULA", "dbo.TAB_ALUNO_MATRICULA");
            DropForeignKey("dbo.TAB_ADVERTENCIA", "COD_AULA", "dbo.TAB_AULA");
            DropForeignKey("dbo.TAB_ADVERTENCIA", "COD_ALUNO_MATRICULA", "dbo.TAB_ALUNO_MATRICULA");
            DropIndex("dbo.TAB_MENSAGEM", new[] { "COD_ALUNO_MATRICULA" });
            DropIndex("dbo.TAB_ADVERTENCIA", new[] { "COD_AULA" });
            DropIndex("dbo.TAB_ADVERTENCIA", new[] { "COD_ALUNO_MATRICULA" });
            DropTable("dbo.TAB_MENSAGEM");
            DropTable("dbo.TAB_ADVERTENCIA");
        }
    }
}
