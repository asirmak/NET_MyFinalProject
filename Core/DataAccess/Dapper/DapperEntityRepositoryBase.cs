using Core.Entities;
using Core.Utilities.Dapper;
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
            _tableName = DapperHelper<TEntity>.GetTableName(typeof(TEntity));
        }

        public void Add(TEntity entity)
        {
            var values = DapperHelper<TEntity>.GetColumnValues(entity);
            var columns = values.Replace("@", string.Empty);
            var parameters = DapperHelper<TEntity>.GetAllParameters(entity);

            var query = $"insert into {_tableName} ({columns}) values ({values})";
            _connection.Execute(query, parameters);
        }

        public void Delete(TEntity entity)
        {
            var primaryKeyName = DapperHelper<TEntity>.GetPrimaryKeyName();
            var primaryKey = DapperHelper<TEntity>.GetPrimaryKey(entity);
            var query = $"delete from {_tableName} where {primaryKeyName} = @{primaryKeyName}";
            _connection.Execute(query, primaryKey);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            var entities = _connection.Query<TEntity>($"select * from {_tableName}").ToList();
            var compiledFilter = filter.Compile();
            return entities.SingleOrDefault(compiledFilter);
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
            var values = DapperHelper<TEntity>.GetColumnValues(entity);
            var columns = values.Replace("@", string.Empty);
            var parameters = DapperHelper<TEntity>.GetAllParameters(entity);
            var pairs = DapperHelper<TEntity>.KeyValuePair(columns, values);
            var primaryKey = DapperHelper<TEntity>.GetPrimaryKeyName();

            var query = $"update {_tableName} set {pairs} where {primaryKey} = @{primaryKey}";
            _connection.Execute(query, parameters);
        }
    }
}
