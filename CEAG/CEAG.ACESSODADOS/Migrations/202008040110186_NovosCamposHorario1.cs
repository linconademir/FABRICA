namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovosCamposHorario1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_TURMA", "COD_IN_HORARIO", c => c.Int());
            CreateIndex("dbo.TAB_TURMA", "COD_IN_HORARIO");
            AddForeignKey("dbo.TAB_TURMA", "COD_IN_HORARIO", "dbo.TAB_HORARIO", "COD_IN_HORARIO");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAB_TURMA", "COD_IN_HORARIO", "dbo.TAB_HORARIO");
            DropIndex("dbo.TAB_TURMA", new[] { "COD_IN_HORARIO" });
            DropColumn("dbo.TAB_TURMA", "COD_IN_HORARIO");
        }
    }
}
