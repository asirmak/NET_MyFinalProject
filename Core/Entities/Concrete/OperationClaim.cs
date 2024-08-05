using Core.Utilities.Attributes;

namespace Core.Entities.Concrete
{
    [Table("OperationClaims")]
    public class OperationClaim : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
