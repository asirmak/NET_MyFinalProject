using Core.Entities;
using Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    [Table("Categories")]
    public class Category : IEntity
    {
        [PrimaryKey]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


    }
}
