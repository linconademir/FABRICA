    using CEAG.ACESSODADOS.TypeConfiguration;
using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS.Context
{
    public class CeagDbContext : DbContext
    {
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Acesso> Acessos { get; set; }
        public DbSet<AcessoRole> AcessoRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EscolaGrupo> EscolaGrupos { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
        public DbSet<Responsavel> Responsavels { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Informacao> Informacaos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<AlunoMatricula> AlunoMatriculas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<MensalidadeValor> MensalidadeValors { get; set; }
        public DbSet<Taxa> Taxas { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<FuncionarioDisciplina> FuncionarioDisciplinas { get; set; }
        public DbSet<TurmaFuncionarioDisciplina> TurmaFuncionarioDisciplinas { get; set; }
        public DbSet<AlunoMatriculaUnidade> AlunoMatriculaUnidades { get; set; }
        public DbSet<Questionario> Questionarios { get; set; }
        public DbSet<AlunoQuestionario> AlunoQuestionarios { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<AulaAluno> AulaAlunos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<HorarioAula> HorarioAulas { get; set; }
        public DbSet<Feriado> Feriados { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<Debito> Debitos { get; set; }
        public DbSet<ContaBancaria> ContaBancarias { get; set; }
        public DbSet<DebitoHistorico> DebitoHistoricos { get; set; }
        public DbSet<Mensagem> Mensagems { get; set; }
        public DbSet<Advertencia> Advertencias { get; set; }
        public DbSet<PontoAtencao> PontoAtencaos { get; set; }
        public DbSet<Parentesco> Parentescos { get; set; }
        public DbSet<ParentescoAluno> ParentescoAlunos { get; set; }

        public CeagDbContext() : base("CeagDbContext")
        {
            //LazyLoadingEnabled por padrão é true, é o carregamento preguiçoso. (grandessissimo numero de dados)
            Configuration.LazyLoadingEnabled = false;


            //Desabilita o proxy da minha classse, evitando que ele crie a classe fantasma fazendo o meio de campo entre a minha classe e o código entity (se o LazyLoading está desabilitado não faz sentidno o proxy es´tar habilitado)
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove exclusão em casacata
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new AlunoTypeConfiguration());
            modelBuilder.Configurations.Add(new InformacaoTypeConfiguration());
            modelBuilder.Configurations.Add(new EscolaTypeConfiguration());
            modelBuilder.Configurations.Add(new LogradouroTypeConfiguration());
            modelBuilder.Configurations.Add(new TelefoneTypeConfiguration());
            modelBuilder.Configurations.Add(new ResponsavelTypeConfiguration());
            modelBuilder.Configurations.Add(new MatriculaTypeConfiguration());
            modelBuilder.Configurations.Add(new MensalidadeValorTypeConfiguration());
            modelBuilder.Configurations.Add(new TaxaTypeConfiguration());
            modelBuilder.Configurations.Add(new TurmaTypeConfiguration());
            modelBuilder.Configurations.Add(new AlunoMatriculaTypeConfiguration());
            modelBuilder.Configurations.Add(new FuncionarioTypeConfiguration());
            modelBuilder.Configurations.Add(new DisciplinaTypeConfiguration());
            modelBuilder.Configurations.Add(new AcessoTypeConfiguration());
            modelBuilder.Configurations.Add(new AcessoRoleTypeConfiguration());
            modelBuilder.Configurations.Add(new RoleTypeConfiguration());
            modelBuilder.Configurations.Add(new EscolaGrupoTypeConfiguration());
            modelBuilder.Configurations.Add(new FuncionarioDisciplinaTypeConfiguration());
            modelBuilder.Configurations.Add(new TurmaFuncionarioDisciplinaTypeConfiguration());
            modelBuilder.Configurations.Add(new AlunoMatriculaUnidadeTypeConfiguration());
            modelBuilder.Configurations.Add(new QuestionarioTypeConfiguration());
            modelBuilder.Configurations.Add(new AlunoQuestionarioTypeConfiguration());
            modelBuilder.Configurations.Add(new AulaTypeConfiguration());
            modelBuilder.Configurations.Add(new AulaAlunoTypeConfiguration());
            modelBuilder.Configurations.Add(new HorarioTypeConfiguration());
            modelBuilder.Configurations.Add(new HorarioAulaTypeConfiguration());
            modelBuilder.Configurations.Add(new FeriadoTypeConfiguration());
            modelBuilder.Configurations.Add(new UnidadeTypeConfiguration());
            modelBuilder.Configurations.Add(new DebitoTypeConfiguration());
            modelBuilder.Configurations.Add(new ContaBancariaTypeConfiguration());
            modelBuilder.Configurations.Add(new DebitoHistoricoTypeConfiguration());
            modelBuilder.Configurations.Add(new AdvertenciaTypeConfiguration());
            modelBuilder.Configurations.Add(new MensagemTypeConfiguration());
            modelBuilder.Configurations.Add(new PontoAtencaoTypeConfiguration());
            modelBuilder.Configurations.Add(new ParentescoTypeConfiguration());
            modelBuilder.Configurations.Add(new ParentescoAlunoTypeConfiguration());
            //base.OnModelCreating(modelBuilder);
        }
    }
}
