using Core.Utilities.Attributes;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Utilities.Dapper
{
    public static class DapperHelper<TEntity>
    {
        public static string GetColumnValues(TEntity entity)
        {
            var propertyNames = typeof(TEntity).GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(PrimaryKeyAttribute)))
                .Select(p => $"@{p.Name}");
            return string.Join(", ", propertyNames);
        }

        public static DynamicParameters GetAllParameters(TEntity entity)
        {
            var parameters = new DynamicParameters();
            foreach (var property in typeof(TEntity).GetProperties())
            {
                parameters.Add($"@{property.Name}", property.GetValue(entity));
            }
            return parameters;
        }

        public static DynamicParameters GetPrimaryKey(TEntity entity)
        {
            var primaryKey = new DynamicParameters();
            foreach (var property in typeof(TEntity).GetProperties().Where(p => Attribute.IsDefined(p, typeof(PrimaryKeyAttribute))))
            {
                primaryKey.Add($"@{property.Name}", property.GetValue(entity));
            }
            return primaryKey;
        }

        public static string KeyValuePair(string key, string value)
        {
            string pairs = string.Empty;
            string[] keys = key.Split(',');
            string[] values = value.Split(',');

            for (int i = 0; i < keys.Length; i++)
            {
                string pair = keys[i].Trim() + "=" + values[i].Trim() + ", ";
                pairs += pair;
            }
            return pairs.Substring(0, pairs.Length - 2);
        }

        public static string GetPrimaryKeyName()
        {
            var primaryKey = typeof(TEntity).GetProperties().Where(p => Attribute.IsDefined(p, typeof(PrimaryKeyAttribute))).First();
            return primaryKey.Name;
        }

        public static string GetTableName(Type type)
        {
            var attribute = typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), false).Cast<TableAttribute>().Single();
            return attribute.Name;
        }

    }
}
