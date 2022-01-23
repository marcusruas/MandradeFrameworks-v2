using MandradeFrameworks.SharedKernel.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MandradeFrameworks.SharedKernel.Models
{
    /// <summary>
    /// Classe utilizada para retornar dados de um Enum para o frontend
    /// </summary>
    public class EnumDto
    {
        public EnumDto() { }
        public EnumDto(int codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
        /// <summary>
        /// Cria a DTO com base no enumerador indicado no construtor
        /// </summary>
        /// <param name="item"></param>
        public EnumDto(Enum item)
        {
            Codigo = Convert.ToInt32(item);
            Descricao = item.StringValueOf();
        }

        /// <summary>
        /// Valor no formato de <see cref="Int32"/> do enumerador.
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Descrição do enumerador. É preenchido caso o enumerador tenha o atributo <see cref="DescriptionAttribute"/> definido.
        /// </summary>
        public string Descricao { get; set; }
    }
}
