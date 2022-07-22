using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Rental : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public int PaymentId { get; set; }
        public bool MilageLimit { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsActive { get; set; }
        public Car Car { get; set; }
        public User User { get; set; }
        public Supplier Supplier { get; set; }
        public Payment Payment { get; set; }
    }


}
