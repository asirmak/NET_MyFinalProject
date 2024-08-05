using Core.DataAccess.Dapper;
using Dapper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.Dapper
{
    public class DapperProductDal : DapperEntityRepositoryBase<Product>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (var _connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true;"))
            {
                var entities = _connection.Query<ProductDetailDto>($"select p.ProductId, p.ProductName, c.CategoryName, p.UnitsInStock from products p" +
                    $" join categories c on c.CategoryId = p.CategoryId").ToList();

                return entities;
            }
        }
    }
}
