using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    // generic constraint
    // class : referans tip, sadece classlara izin veriyor
    // IEntity: Sadece IEntity referansını tutan classlara ve kendisine izin var.
    // new(): sadece classlara izin var. interface olan IEntity giremez.

    public interface IEntityRepository<T> where T : class, IEntity, new()  
    {
        List<T> GetAll(Expression<Func<T, bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
