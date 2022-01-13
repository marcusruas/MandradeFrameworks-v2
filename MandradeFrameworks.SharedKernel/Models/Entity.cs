using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Models
{
    /// <summary>
    /// Entidade com propriedades básicas
    /// </summary>
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
