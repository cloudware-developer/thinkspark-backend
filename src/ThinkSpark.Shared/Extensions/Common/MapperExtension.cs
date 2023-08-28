using FastMapper.NetCore;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace ThinkSpark.Shared.Extensions.Common
{
    public static class MapperExtension
    {
        /// <summary>
        /// Mapeia uma entidade de banco de dados para uma entidade de visualização, utilizando o padrão MVVM.
        /// </summary>
        /// <typeparam name="TEntity">Modelo de banco de dados.</typeparam>
        /// <typeparam name="TEntityVm">Modelo de Visualização.</typeparam>
        /// <returns>Obtem uma entidade de visualização</returns>
        public static TEntityVm ToViewModel<TEntity, TEntityVm>(this TEntity entity)
        {
            try
            {
                var result = TypeAdapter.Adapt<TEntity, TEntityVm>(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Mapeia uma entidade de visualização para uma entidade modelo de banco de dados, utilizando o padrão MVVM.
        /// </summary>
        /// <typeparam name="TEntity">Modelo de banco de dados.</typeparam>
        /// <typeparam name="TEntityVm">Modelo de Visualização.</typeparam>
        /// <returns>Obtem uma entidade de banco de dados</returns>
        public static TEntity ToModelView<TEntity, TEntityVm>(this TEntityVm entity)
        {
            try
            {
                var result = TypeAdapter.Adapt<TEntityVm, TEntity>(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Mapeia uma coleção de entidade de banco de dados para uma coleção de entidade de visualização, utilizando o padrão MVVM.
        /// </summary>
        /// <typeparam name="TEntity">Modelo de banco de dados.</typeparam>
        /// <typeparam name="TEntityVm">Modelo de Visualização.</typeparam>
        /// <returns>Obtem uma coleção de entidade de banco de dados.</returns>
        public static List<TEntityVm> ToViewModel<TEntity, TEntityVm>(this List<TEntity> entity)
        {
            try
            {
                var result = entity.Select(x => x.ToViewModel<TEntity, TEntityVm>()).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Mapeia uma coleção de entidade de visualização para uma coleção de entidade de banco de dados, utilizando o padrão MVVM.
        /// </summary>
        /// <typeparam name="TEntity">Modelo de banco de dados.</typeparam>
        /// <typeparam name="TEntityVm">Modelo de Visualização.</typeparam>
        /// <returns>Obtem uma coleção de entidade de visualização</returns>
        public static List<TEntity> ToModelView<TEntity, TEntityVm>(this List<TEntityVm> entity)
        {
            try
            {
                var result = entity.Select(x => x.ToModelView<TEntity, TEntityVm>()).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Mapea uma entidade para padrão json.
        /// </summary>
        /// <param name="entity">Entitdade de a ser mapeada</param>
        /// <returns>Obtem a entidade mapeada para json.</returns>
        public static string ToJson<TEntity>(this TEntity entity)
        {
            if (entity == null) return null;
            var json = JsonConvert.SerializeObject(entity);
            return json;
        }

        /// <summary>
        /// Mapeia uma coleção em DataTable.
        /// </summary>
        /// <typeparam name="TEntity">Entidade tipo;</typeparam>
        /// <param name="collection">Coleção.</param>
        /// <returns>Obtem um DataTable da coleção.</returns>
        public static DataTable ToDataTable<TEntity>(this List<TEntity> collection)
        {
            var dataTable = new DataTable();
            PropertyInfo[] propertyCollection = null;

            if (collection == null) return dataTable;

            foreach (TEntity item in collection)
            {
                if (propertyCollection == null)
                {
                    propertyCollection = item.GetType().GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyCollection)
                    {
                        Type columnType = propertyInfo.PropertyType;

                        if (columnType.IsGenericType && columnType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            columnType = columnType.GetGenericArguments()[0];
                        }

                        dataTable.Columns.Add(new DataColumn(propertyInfo.Name, columnType));
                    }
                }

                DataRow dataRow = dataTable.NewRow();

                foreach (PropertyInfo propertyInfo in propertyCollection)
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(item, null) == null ? DBNull.Value : propertyInfo.GetValue(item, null);
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        /// <summary>
        /// Efetua lógicas de manipulação das propriedades do objeto.
        /// </summary>
        /// <typeparam name="TEntity">Objeto a ser manipulado.</typeparam>
        /// <param name="item">Objeto a ser trabalhado.</param>
        /// <param name="action">Ação a ser construída.</param>
        public static void IfType<TEntity>(this object item, Action<TEntity> action) where TEntity : class
        {
            if (item is TEntity)
                action(item as TEntity);
        }

        /// <summary>
        /// Extensão que adiciona um elemento a uma coleção apenas se uma determinada condição for atendida.
        /// </summary>
        /// <typeparam name="TEntity">Objeto a ser manipulado.</typeparam>
        /// <param name="collection">Coleção a ser trabalhada.</param>
        /// <param name="predicate">Ação a ser construída</param>
        /// <param name="item">Objeto a ser adicionado.</param>
        public static void AddIf<TEntity>(this ICollection<TEntity> collection, Func<bool> predicate, TEntity item)
        {
            if (predicate.Invoke())
                collection.Add(item);
        }

        /// <summary>
        /// Extensão que adiciona um elemento a uma coleção apenas se uma determinada condição for atendida.
        /// </summary>
        /// <typeparam name="TEntity">Objeto a ser manipulado.</typeparam>
        /// <param name="collection">Coleção a ser trabalhada.</param>
        /// <param name="predicate">Ação a ser construída</param>
        /// <param name="item">Objeto a ser adicionado.</param>
        public static void AddIf<TEntity>(this ICollection<TEntity> collection, Func<TEntity, bool> predicate, TEntity item)
        {
            if (predicate.Invoke(item))
                collection.Add(item);
        }

        /// <summary>
        /// Obtem o valor de uma propriedade de um objeto.
        /// </summary>
        /// <param name="source">Objeto que contem a propriedade.</param>
        /// <param name="propertyName">Nome da propriedade.</param>
        /// <returns>Retorna o valor do objeto.</returns>
        public static object GetPropertyValue(object source, string propertyName)
        {
            var result = source.GetType().GetProperty(propertyName).GetValue(source, null);
            return result;
        }

        /// <summary>
        /// Verifica se um propriedade existe em um objeto.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns>Retorna verdadeiro ou falso.</returns>
        public static bool HasProperty(this object source, string propertyName)
        {
            var result = source.GetType().GetProperty(propertyName) != null;
            return result;
        }

        /// <summary>
        /// Deixa caracteres minúsculos de uma entidade que forem das propriedades [Email,,,,].
        /// </summary>
        /// <param name="value">Entidade</param>
        public static object ToUpperFields(this object value)
        {
            try
            {
                var type = value.GetType();
                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(c => c.PropertyType == typeof(string));

                foreach (var propertyInfo in properties)
                {
                    if (propertyInfo.Name == "Email")
                    {
                        var newValue = (string)propertyInfo.GetValue(value);

                        if (!string.IsNullOrEmpty(newValue))
                            newValue = newValue.ToLower();

                        propertyInfo.SetValue(value, newValue);
                    }
                    else if (propertyInfo.Name == "Senha" || propertyInfo.Name == "Password" || propertyInfo.Name == "Authorization" || propertyInfo.Name == "Token" || propertyInfo.Name == "Remetente" || propertyInfo.Name == "Destinatario")
                    {
                        var newValue = (string)propertyInfo.GetValue(value);
                        propertyInfo.SetValue(value, newValue);
                    }
                    else
                    {
                        var newValue = (string)propertyInfo.GetValue(value);

                        if (!string.IsNullOrEmpty(newValue))
                            newValue = newValue.ToUpper();

                        propertyInfo.SetValue(value, newValue);
                    }
                }

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao executar o método {nameof(ToUpperFields)}. Erro {ex.Message}");
            }
        }
    }
}
