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
    public class CeagDbContextAcesso : DbContext
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

     
        public DbSet<Acesso> Acessos { get; set; }
        public DbSet<AcessoRole> AcessoRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EscolaGrupo> EscolaGrupos { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
        public CeagDbContextAcesso() : base("CeagDbContext")
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
           
            modelBuilder.Configurations.Add(new AcessoTypeConfiguration());
            modelBuilder.Configurations.Add(new AcessoRoleTypeConfiguration());
            modelBuilder.Configurations.Add(new RoleTypeConfiguration());
            modelBuilder.Configurations.Add(new EscolaGrupoTypeConfiguration());
            modelBuilder.Configurations.Add(new EscolaTypeConfiguration());
            modelBuilder.Configurations.Add(new LogradouroTypeConfiguration());

            //modelBuilder.Ignore<AulaPresencaTypeConfig>();
            //base.OnModelCreating(modelBuilder);
        }
    }
}
