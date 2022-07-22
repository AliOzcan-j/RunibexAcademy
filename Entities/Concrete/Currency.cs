using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Currency : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public List<Payment> Payments { get; set; }
    }


}
