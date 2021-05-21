namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HorarioAulas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_AULA", "COD_IN_HORARIO_AULA", c => c.Int(nullable: false));
            CreateIndex("dbo.TAB_AULA", "COD_IN_HORARIO_AULA");
            AddForeignKey("dbo.TAB_AULA", "COD_IN_HORARIO_AULA", "dbo.TAB_HORARIO_AULA", "COD_IN_HORARIO_AULA");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_AULA", "COD_IN_HORARIO_AULA", "dbo.TAB_HORARIO_AULA");
            DropIndex("dbo.TAB_AULA", new[] { "COD_IN_HORARIO_AULA" });
            DropColumn("dbo.TAB_AULA", "COD_IN_HORARIO_AULA");
        }
    }
}
