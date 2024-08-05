using Core.DataAccess.Dapper;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Dapper
{
    public class DapperCategoryDal : DapperEntityRepositoryBase<Category>, ICategoryDal
    {
    }
}
