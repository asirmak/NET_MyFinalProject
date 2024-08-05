using Core.Entities;
using Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    [Table("Customers")]
    public class Customer : IEntity
    {
        [PrimaryKey]
        public string CustomerId { get; set; }
        public string ContactName{ get; set; }
        public string CompanyName{ get; set; }
        public string City{ get; set; }
    }
}
