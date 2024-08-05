using Core.DataAccess.Dapper;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Dapper
{
    public class DapperUserDal : DapperEntityRepositoryBase<User>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            throw new NotImplementedException();
        }
    }
}
