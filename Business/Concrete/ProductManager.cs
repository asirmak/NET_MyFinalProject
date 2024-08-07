﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Security;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //[SecuredOperation("product.add, admin")]
        //[ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]

        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfSameProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryNumberMaximum());

            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        private IResult CheckIfSameProductNameExists(string productName)
        {
            if (_productDal.GetAll(p => p.ProductName == productName).Any())
            {
                return new ErrorResult("Aynı isimden ürün var");
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            if (_productDal.GetAll(p => p.CategoryId == categoryId).Count > 10)
            {
                return new ErrorResult(Messages.ProductCategoryMaximumNumber);
            }
            return new SuccessResult();

        }
        [CacheAspect]
        [PerformanceAspect(1)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 22 )
            {
                return new ErrorDataResult<List<Product>>
                    (Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>
                (_productDal.GetAll(), Messages.ProductListed);
        }

        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>
                (_productDal.GetAll(p=> p.CategoryId == id), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>
                (_productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice <= max), Messages.ProductListed);
        }

        [CacheAspect]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>
                (_productDal.Get(p=>p.ProductId == productId), Messages.ProductListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>
                (_productDal.GetProductDetails(), Messages.ProductListed);
        }

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult();
        }

        public IResult CheckIfCategoryNumberMaximum()
        {
            if (_categoryService.GetAll().Data.Count > 15)
            {
                return new ErrorResult(Messages.ProductCategoryLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
