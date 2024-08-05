using Core.Attributes;
using Core.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.Dapper
{
    public class DapperEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    {
        private SqlConnection _connection;
        private string _tableName;

        public DapperEntityRepositoryBase()
        {
            _connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true;");
            _tableName = GetTableName(typeof(TEntity));
        }

        public void Add(TEntity entity)
        {
            var values = GetColumnValues(entity);
            var columns = values.Replace("@", string.Empty);
            var parameters = GetAllParameters(entity);

            var query = $"insert into {_tableName} ({columns}) values ({values})";
            _connection.Execute(query, parameters);
        }

        public void Delete(TEntity entity)
        {
            var primaryKeyName = GetPrimaryKeyName();
            var primaryKey = GetPrimaryKey(entity);
            var query = $"delete from {_tableName} where {primaryKeyName} = @{primaryKeyName}";
            _connection.Execute(query, primaryKey);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            var entities = _connection.Query<TEntity>($"select * from {_tableName}").ToList();
            var compiledFilter = filter.Compile();
            return entities.Single(compiledFilter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            var entities = _connection.Query<TEntity>($"select * from {_tableName}").ToList();
            if (filter != null)
            {
                var compiledFilter = filter.Compile();
                entities = entities.Where(compiledFilter).ToList();
            }

            return entities;
        }

        public void Update(TEntity entity)
        {
            var values = GetColumnValues(entity);
            var columns = values.Replace("@", string.Empty);
            var parameters = GetAllParameters(entity);
            var pairs = KeyValuePair(columns, values);
            var primaryKey = GetPrimaryKeyName();

            var query = $"update {_tableName} set {pairs} where {primaryKey} = @{primaryKey}";
            _connection.Execute(query, parameters);
        }

        private static string GetColumnValues(TEntity entity)
        {
            var propertyNames = typeof(TEntity).GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(PrimaryKeyAttribute)))
                .Select(p => $"@{p.Name}");
            return string.Join(", ", propertyNames);
        }

        private static DynamicParameters GetAllParameters(TEntity entity)
        {
            var parameters = new DynamicParameters();
            foreach (var property in typeof(TEntity).GetProperties())
            {
                parameters.Add($"@{property.Name}", property.GetValue(entity));
            }
            return parameters;
        }

        private static DynamicParameters GetPrimaryKey(TEntity entity)
        {
            var primaryKey = new DynamicParameters();
            foreach (var property in typeof(TEntity).GetProperties().Where(p => Attribute.IsDefined(p, typeof(PrimaryKeyAttribute))))
            {
                primaryKey.Add($"@{property.Name}", property.GetValue(entity));
            }
            return primaryKey;
        }

        public string KeyValuePair(string key, string value)
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

        public string GetPrimaryKeyName()
        {
            var primaryKey = typeof(TEntity).GetProperties().Where(p => Attribute.IsDefined(p, typeof(PrimaryKeyAttribute))).First();
            return primaryKey.Name;
        }

        public string GetTableName(Type type)
        {
            var attribute = typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), false).Cast<TableAttribute>().Single();
            return attribute.Name;
        }
    }
}
