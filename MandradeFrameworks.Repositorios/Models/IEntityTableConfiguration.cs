using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Repositorios.Models
{
    /// <summary>
    /// Interface utilizada para adicionar configurações de mapeamento de uma entidade para uma tabela do DBContext
    /// </summary>
    public interface IEntityTableConfiguration<TEntity> where TEntity : class
    {
        void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
