namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // bu hangi user ın claim i sorusuna cevaben UserId property si de ekledik
        public int OperationClaimId { get; set; }
    }
}
