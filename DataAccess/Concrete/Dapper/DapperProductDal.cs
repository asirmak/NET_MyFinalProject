using Core.DataAccess.Dapper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Dapper
{
    public class DapperProductDal : DapperEntityRepositoryBase<Product>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
