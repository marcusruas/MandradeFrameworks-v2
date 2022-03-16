using MandradeFrameworks.Repositorios.Helpers;
using MandradeFrameworks.Repositorios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MandradeFrameworks.Repositorios.Configuration
{
    public static class DbContextConfiguration
    {
        /// <summary>
        /// Adiciona um novo DBContext com base no tipo <see cref="StandardContext"/> informado.
        /// /// Caso ocorra algum erro (exception) na função, será enviado uma exception comum informando o problema e nenhuma outra ação será realizada
        /// </summary>
        /// <param name="nomeBanco">O nome da connection string que será utilizada para registrar o DBContext. 
        /// Para o nome da tabela de Migrations deste DBContext, será utilizado o valor desta variavel com o prefixo "__" (por ex: __nomeBanco)</param>
        public static IServiceCollection AddDbContextSqlServer<TContext>(
            this IServiceCollection services, 
            IConfiguration configuration, 
            string nomeBanco
        ) where TContext : StandardContext
        {
            var cnnHelper = new ConnectionStringHelper(configuration);
            
            var connectionString = cnnHelper.ObterConnectionString(nomeBanco);
            var migrationsTable = $"__{nomeBanco}";

            services.AddDbContext<TContext>(config =>
                config.UseSqlServer(
                    connectionString, 
                    x => x.MigrationsHistoryTable(migrationsTable)
                )
            );

            return services;
        }

        /// <summary>
        /// Método utilizado para configurar o DBContext de forma a aceitar todas as classes que configuram a tabela utilizando a interface
        /// <see cref="IEntityTableConfiguration{TEntity}"/>.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AplicarModelBuilders(this ModelBuilder modelBuilder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var tiposParaRegistrar = new List<Type>();

            foreach(var assembly in assemblies)
            {
                var tipo = assembly.GetTypes()
                    .Where(x => x.GetInterfaces().Any(gi => 
                        gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTableConfiguration<>))
                    ).ToList();

                tiposParaRegistrar.AddRange(tipo);
            }

            foreach(var tipo in tiposParaRegistrar)
            {
                dynamic instanciaTipo = Activator.CreateInstance(tipo);
                modelBuilder.ApplyConfiguration(instanciaTipo);
            }
        }
    }
}