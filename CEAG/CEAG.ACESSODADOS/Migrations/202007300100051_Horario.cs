namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Horario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAB_HORARIO",
                c => new
                    {
                        COD_IN_HORARIO = c.Int(nullable: false, identity: true),
                        DESCRICAO_VC_HORARIO = c.String(nullable: false, maxLength: 150, unicode: false),
                        COD_IN_ESCOLA = c.Int(nullable: false),
                        INCLUSAO_DT_HORARIO = c.DateTime(nullable: false),
                        CANCELAMENTO_DT_HORARIO = c.DateTime(),
                    })
                .PrimaryKey(t => t.COD_IN_HORARIO)
                .ForeignKey("dbo.TAB_ESCOLA", t => t.COD_IN_ESCOLA)
                .Index(t => t.COD_IN_ESCOLA);
            
            CreateTable(
                "dbo.TAB_HORARIO_AULA",
                c => new
                    {
                        COD_IN_HORARIO_AULA = c.Int(nullable: false, identity: true),
                        TURNO_VC_HORARIO_AULA = c.String(nullable: false, maxLength: 30, unicode: false),
                        INICIO_TM_HORARIO_AULA = c.Time(nullable: false, precision: 7),
                        TERMINO_TM_HORARIO_AULA = c.Time(nullable: false, precision: 7),
                        DIA_SEMANA_IN_HORARIO_AULA = c.Int(nullable: false),
                        COD_IN_HORARIO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_IN_HORARIO_AULA)
                .ForeignKey("dbo.TAB_HORARIO", t => t.COD_IN_HORARIO)
                .Index(t => t.COD_IN_HORARIO);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_HORARIO_AULA", "COD_IN_HORARIO", "dbo.TAB_HORARIO");
            DropForeignKey("dbo.TAB_HORARIO", "COD_IN_ESCOLA", "dbo.TAB_ESCOLA");
            DropIndex("dbo.TAB_HORARIO_AULA", new[] { "COD_IN_HORARIO" });
            DropIndex("dbo.TAB_HORARIO", new[] { "COD_IN_ESCOLA" });
            DropTable("dbo.TAB_HORARIO_AULA");
            DropTable("dbo.TAB_HORARIO");
        }
    }
}
