namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Feriado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_FERIADO",
                c => new
                    {
                        COD_IN_FERIADO = c.Int(nullable: false, identity: true),
                        DATA_DT_FERIADO = c.DateTime(nullable: false),
                        DESCRICAO_VC_FERIADO = c.String(maxLength: 160, unicode: false),
                        TIPO_VC_FERIADO = c.String(maxLength: 60, unicode: false),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_FERIADO)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_ESCOLA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_FERIADO", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_FERIADO", new[] { "COD_IN_ESCOLA" });
            DropTable("dbo.TAB_FERIADO");
        }
    }
}
