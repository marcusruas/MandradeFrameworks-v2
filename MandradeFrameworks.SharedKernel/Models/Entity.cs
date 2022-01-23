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
        /// <summary>
        /// Instanciar entidade com novo Id e data de criação como a data atual
        /// </summary>
        public Entity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }

        /// <summary>
        /// Identificador da entidade
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Data em que a entidade foi criada
        /// </summary>
        public DateTime DataCriacao { get; set; }
    }
}
