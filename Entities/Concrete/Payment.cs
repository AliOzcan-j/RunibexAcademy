using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CreditCardId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public User User { get; set; }
        public CreditCard CreditCard { get; set; }
        public Currency Currency { get; set; }
        public Rental Rental { get; set; }
    }
}
