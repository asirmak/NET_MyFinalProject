using Core.DataAccess.Dapper;
using Core.Entities.Concrete;
using Dapper;
using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.Dapper
{
    public class DapperUserDal : DapperEntityRepositoryBase<User>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var _connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true;"))
            {
                var entities = _connection.Query<OperationClaim>($"select operationClaim.Id, operationClaim.Name from OperationClaims operationClaim" +
                    $" join UserOperationClaims userOperationClaim on userOperationClaim.OperationClaimId = operationClaim.Id" +
                    $" where userOperationClaim.UserId = {user.Id}").ToList();

                return entities;
            }
        }
    }
}
