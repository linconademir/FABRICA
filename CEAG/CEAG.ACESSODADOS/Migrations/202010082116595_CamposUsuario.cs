namespace CEAG.ACESSODADOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAB_ALUNO", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_ALUNO_MATRICULA", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_DISCIPLINA", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_AULA", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_AULA_ALUNO", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_HORARIO_AULA", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_HORARIO", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_TURMA", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_FUNCIONARIO_DISCIPLINA", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_LOGRADOURO", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_RESPONSAVEL", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_TELEFONE", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_MENSALIDADE", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_CONTA_BANCARIA", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_ALUNO_QUESTIONARIO", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_QUESTIONARIO", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_INFORMACAO", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_FERIADO", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_MATRICULA_INDICE", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_MENSALIDADE_VALOR", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_TAXA", "COD_IN_ACESSO", c => c.Int());
            AddColumn("dbo.TAB_UNIDADE", "COD_IN_ACESSO", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAB_UNIDADE", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_TAXA", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_MENSALIDADE_VALOR", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_MATRICULA_INDICE", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_FERIADO", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_INFORMACAO", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_QUESTIONARIO", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_ALUNO_QUESTIONARIO", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_CONTA_BANCARIA", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_MENSALIDADE", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_TELEFONE", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_RESPONSAVEL", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_LOGRADOURO", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_FUNCIONARIO_DISCIPLINA", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_TURMA_FUNCIONARIO_DISCIPLINA", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_TURMA", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_HORARIO", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_HORARIO_AULA", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_AULA_ALUNO", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_AULA", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_DISCIPLINA", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_ALUNO_MATRICULA_UNIDADE", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_ALUNO_MATRICULA", "COD_IN_ACESSO");
            DropColumn("dbo.TAB_ALUNO", "COD_IN_ACESSO");
        }
    }
}
