using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Extensions
{
    public static class EnumExtensions
    {
		/// <summary>
		/// Obtém o atributo no enum informado
		/// </summary>
		/// <param name="value">O enum para obter o atributo.</param>
		/// <returns>O objeto de atributo informado no tipo.</returns>
		/// <returns>null se o Enum informado não possuir o atributo do tipo informado.</returns>
		public static T GetAttribute<T>(this Enum value) where T : Attribute
		{
			FieldInfo campo = value.GetType().GetField(value.ToString());
			return (T)campo.GetCustomAttribute(typeof(T), true);
		}

		/// <summary>
		/// Obtém o valor do primeiro atributo do tipo <see cref="DescriptionAttribute" />
		/// </summary>
		/// <param name="value">O enum para obter a descrição.</param>
		/// <returns>O valor do primeiro atributo do tipo <see cref="DescriptionAttribute" /></returns>
		public static string StringValueOf(this Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes.Length > 0)
				return attributes[0].Description;
			else
				return value.ToString();
		}
	}
}
