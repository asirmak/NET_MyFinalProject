using Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{ 
    [Table("Users")]
    public class User : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
    }
}
