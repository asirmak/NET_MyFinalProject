using Core.Utilities.Attributes;

namespace Core.Entities.Concrete
{
    [Table("UserOperationClaims")]
    public class UserOperationClaim : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }

}
