namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicio_20200524 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_ALUNO",
                c => new
                    {
                        COD_IN_ALUNO = c.Int(nullable: false, identity: true),
                        MATRICULA_VC_ALUNO = c.String(nullable: false, maxLength: 30, unicode: false),
                        NOME_VC_ALUNO = c.String(nullable: false, maxLength: 200, unicode: false),
                        SEXO_VC_ALUNO = c.String(nullable: false, maxLength: 15, unicode: false),
                        CPF_VC_ALUNO = c.String(maxLength: 11, unicode: false),
                        RG_VC_ALUNO = c.String(maxLength: 30, unicode: false),
                        RG_UF_VC_ALUNO = c.String(maxLength: 2, unicode: false),
                        RG_ORGAO_VC_ALUNO = c.String(maxLength: 5, unicode: false),
                        NASCIMENTO_DT_ALUNO = c.DateTime(nullable: false),
                        INCLUSAO_DT_ALUNO = c.DateTime(nullable: false),
                        EMAIL_VC_ALUNO = c.String(nullable: false, maxLength: 150, unicode: false),
                        MAE_VC_ALUNO = c.String(nullable: false, maxLength: 200, unicode: false),
                        MAE_PROFISSAO_VC_ALUNO = c.String(maxLength: 200, unicode: false),
                        PAI_VC_ALUNO = c.String(maxLength: 200, unicode: false),
                        PAI_PROFISSAO_VC_ALUNO = c.String(maxLength: 200, unicode: false),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                        COD_IN_LOGRADOURO = c.Int(nullable: false),
                        TP_SANGUINEO_VC_ALUNO = c.String(maxLength: 30, unicode: false),
                        FATOR_RH_VC_ALUNO = c.String(maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.COD_IN_ALUNO)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .ForeignKey("dbo.TAB_LOGRADOURO", t => t.COD_IN_LOGRADOURO)
                .Index(t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_LOGRADOURO);
            
            CreateTable(
                "dbo.TAB_ESCOLA",
                c => new
                    {
                        COD_IN_ESCOLA = c.Int(nullable: false, identity: true),
                        RAZAO_VC_ESCOLA = c.String(nullable: false, maxLength: 200, unicode: false),
                        FANTASIA_VC_ESCOLA = c.String(nullable: false, maxLength: 200, unicode: false),
                        CNPJ_VC_ESCOLA = c.String(nullable: false, maxLength: 14, unicode: false),
                    })
                .PrimaryKey(t => t.COD_IN_ESCOLA);
            
            CreateTable(
                "dbo.TAB_MATRICULA_INDICE",
                c => new
                    {
                        COD_IN_MATRICULA = c.Int(nullable: false, identity: true),
                        MATRICULA_VC_MATRICULA_INDICE = c.String(nullable: false, maxLength: 50, unicode: false),
                        ANO_IN_MATRICULA_INDICE = c.Int(nullable: false),
                        STATUS_VC_MATRICULA_INDICE = c.String(nullable: false, maxLength: 30, unicode: false),
                        INCLUSAO_DT_MATRICULA_INDICE = c.DateTime(nullable: false),
                        ORDEM_IN_MATRICULA_INDICE = c.Int(nullable: false),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_MATRICULA)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_ESCOLA);
            
            CreateTable(
                "dbo.TAB_INFORMACAO",
                c => new
                    {
                        COD_IN_INFORMACAO = c.Int(nullable: false, identity: true),
                        TIPO_VC_INFORMACAO = c.String(nullable: false, maxLength: 100, unicode: false),
                        DESCRICAO_VC_INFORMACAO = c.String(nullable: false, maxLength: 300, unicode: false),
                        COD_IN_ALUNO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_INFORMACAO)
                .ForeignKey("dbo.TAB_ALUNO", t => t.COD_IN_ALUNO)
                .Index(t => t.COD_IN_ALUNO);
            
            CreateTable(
                "dbo.TAB_LOGRADOURO",
                c => new
                    {
                        COD_IN_LOGRADOURO = c.Int(nullable: false, identity: true),
                        CEP_VC_LOGRADOURO = c.String(nullable: false, maxLength: 8, unicode: false),
                        RUA_VC_LOGRADOURO = c.String(nullable: false, maxLength: 200, unicode: false),
                        NUMERO_VC_LOGRADOURO = c.String(nullable: false, maxLength: 10, unicode: false),
                        COMPLEMENTO_VC_LOGRADOURO = c.String(maxLength: 200, unicode: false),
                        BAIRRO_VC_LOGRADOURO = c.String(nullable: false, maxLength: 100, unicode: false),
                        MUNICIPIO_VC_LOGRADOURO = c.String(nullable: false, maxLength: 100, unicode: false),
                        UF_VC_LOGRADOURO = c.String(nullable: false, maxLength: 2, unicode: false),
                        REFERENCIA_VC_LOGRADOURO = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.COD_IN_LOGRADOURO);
            
            CreateTable(
                "dbo.TAB_RESPONSAVEL",
                c => new
                    {
                        COD_IN_RESPONSAVEL = c.Int(nullable: false, identity: true),
                        NOME_VC_RESPONSAVEL = c.String(nullable: false, maxLength: 200, unicode: false),
                        CPF_VC_RESPONSAVEL = c.String(nullable: false, maxLength: 11, unicode: false),
                        TIPO_VC_RESPONSAVEL = c.String(nullable: false, maxLength: 60, unicode: false),
                        RG_VC_RESPONSAVEL = c.String(maxLength: 10, unicode: false),
                        RG_ORGAO_VC_RESPONSAVEL = c.String(maxLength: 10, unicode: false),
                        RG_UF_VC_RESPONSAVEL = c.String(maxLength: 3, unicode: false),
                        PROFISSAO_VC_RESPONSAVEL = c.String(maxLength: 160, unicode: false),
                        ESTADO_CIVIL_VC_RESPONSAVEL = c.String(maxLength: 30, unicode: false),
                        NATURALIDADE_VC_RESPONSAVEL = c.String(maxLength: 60, unicode: false),
                        NACIONALIDADE_VC_RESPONSAVEL = c.String(maxLength: 60, unicode: false),
                        COD_IN_LOGRADOURO = c.Int(),
                        COD_IN_ALUNO = c.Int(nullable: false),
                        DECLARA_IR_VC_RESPONSAVEL = c.String(nullable: false, maxLength: 3, unicode: false),
                    })
                .PrimaryKey(t => t.COD_IN_RESPONSAVEL)
                .ForeignKey("dbo.TAB_ALUNO", t => t.COD_IN_ALUNO)
                .ForeignKey("dbo.TAB_LOGRADOURO", t => t.COD_IN_LOGRADOURO)
                .Index(t => t.COD_IN_LOGRADOURO)
                .Index(t => t.COD_IN_ALUNO);
            
            CreateTable(
                "dbo.TAB_TELEFONE",
                c => new
                    {
                        COD_IN_TELEFONE = c.Int(nullable: false, identity: true),
                        TIPO_VC_TELEFONE = c.String(nullable: false, maxLength: 60, unicode: false),
                        NUMERO_VC_TELEFONE = c.String(nullable: false, maxLength: 9, unicode: false),
                        DDD_VC_TELEFONE = c.String(nullable: false, maxLength: 2, unicode: false),
                        LOCAL_VC_TELEFONE = c.String(nullable: false, maxLength: 60, unicode: false),
                        INCLUSAO_VC_TELEFONE = c.DateTime(nullable: false),
                        COD_IN_ALUNO = c.Int(),
                        COD_IN_RESPONSAVEL = c.Int(),
                    })
                .PrimaryKey(t => t.COD_IN_TELEFONE)
                .ForeignKey("dbo.TAB_ALUNO", t => t.COD_IN_ALUNO)
                .ForeignKey("dbo.TAB_RESPONSAVEL", t => t.COD_IN_RESPONSAVEL)
                .Index(t => t.COD_IN_ALUNO)
                .Index(t => t.COD_IN_RESPONSAVEL);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_ALUNO", "COD_IN_LOGRADOURO", "dbo.TAB_LOGRADOURO");
            DropForeignKey("dbo.TAB_TELEFONE", "COD_IN_RESPONSAVEL", "dbo.TAB_RESPONSAVEL");
            DropForeignKey("dbo.TAB_TELEFONE", "COD_IN_ALUNO", "dbo.TAB_ALUNO");
            DropForeignKey("dbo.TAB_RESPONSAVEL", "COD_IN_LOGRADOURO", "dbo.TAB_LOGRADOURO");
            DropForeignKey("dbo.TAB_RESPONSAVEL", "COD_IN_ALUNO", "dbo.TAB_ALUNO");
            DropForeignKey("dbo.TAB_INFORMACAO", "COD_IN_ALUNO", "dbo.TAB_ALUNO");
            DropForeignKey("dbo.TAB_ALUNO", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropForeignKey("dbo.TAB_MATRICULA_INDICE", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_TELEFONE", new[] { "COD_IN_RESPONSAVEL" });
            DropIndex("dbo.TAB_TELEFONE", new[] { "COD_IN_ALUNO" });
            DropIndex("dbo.TAB_RESPONSAVEL", new[] { "COD_IN_ALUNO" });
            DropIndex("dbo.TAB_RESPONSAVEL", new[] { "COD_IN_LOGRADOURO" });
            DropIndex("dbo.TAB_INFORMACAO", new[] { "COD_IN_ALUNO" });
            DropIndex("dbo.TAB_MATRICULA_INDICE", new[] { "COD_IN_ESCOLA" });
            DropIndex("dbo.TAB_ALUNO", new[] { "COD_IN_LOGRADOURO" });
            DropIndex("dbo.TAB_ALUNO", new[] { "COD_IN_ESCOLA" });
            DropTable("dbo.TAB_TELEFONE");
            DropTable("dbo.TAB_RESPONSAVEL");
            DropTable("dbo.TAB_LOGRADOURO");
            DropTable("dbo.TAB_INFORMACAO");
            DropTable("dbo.TAB_MATRICULA_INDICE");
            DropTable("dbo.TAB_ESCOLA");
            DropTable("dbo.TAB_ALUNO");
        }
    }
}
