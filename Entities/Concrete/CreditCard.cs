using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public byte[] CardNumberSalt { get; set; }
        public byte[] CardNumberHash { get; set; }
        public byte[] ExpirationDateSalt { get; set; }
        public byte[] ExpirationDateHash { get; set; }
        public byte[] CardHolderNameSalt { get; set; }
        public byte[] CardHolderNameHash { get; set; }
        public User User { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
