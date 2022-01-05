using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MandradeFrameworks.Repositorios.Helpers
{
    public static class DpoSqlMapper
    {
        /// <summary>
        /// Converte um objeto para o tipo <see cref="DynamicParameters"/>, podendo ser usado para passar parâmetros para uma query utilizando a biblioteca Dapper
        /// </summary>
        public static DynamicParameters DpoParaParametros<T>(T dados, object ignorarParametros = null) {
            DynamicParameters parametros = new DynamicParameters();
            var parametrosClasse = typeof(T).GetProperties();
            List<string> listIgnore = new List<string>();

            if (ignorarParametros != null)
                listIgnore = ignorarParametros.GetType().GetProperties().Select(x => x.Name).ToList();

            foreach (var prop in parametrosClasse) {
                if (listIgnore.Any(x => x == prop.Name))
                    continue;

                var dadosProp = dados.GetType().GetProperty(prop.Name).GetValue(dados);
                var descricao = ObterDescription(prop);
                var tipo = ObterTipo(prop.PropertyType);
                var tamanho = ObterTamanho(prop);

                parametros.Add(descricao, dadosProp, tipo, size: tamanho);
            }

            return parametros;
        }

        public static void MapearRetornoObjeto<T>() {
            var map = new CustomPropertyTypeMap(typeof(T),
                        (type, columnName) => type.GetProperties().FirstOrDefault(prop => ObterDescription(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(T), map);
        }

        private static string ObterDescription(PropertyInfo prop) {
            var description = prop.GetCustomAttribute<DescriptionAttribute>();
            return description == null ? prop.Name : description.Description;
        }

        private static int? ObterTamanho(PropertyInfo prop) {
            var description = prop.GetCustomAttribute<StringLengthAttribute>();

            if (description == null)
                return null;
            if (description.MinimumLength != 0 && description.MaximumLength == 0)
                return description.MinimumLength;

            return description.MaximumLength;
        }

        private static DbType? ObterTipo(Type prop) {
            var tiposSQL = Enum.GetValues(typeof(DbType));
            foreach (var tipo in tiposSQL)
                if (tipo.ToString() == prop.Name)
                    return (DbType)tipo;

            return null;
        }
    }
}
