using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Country : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string IsoCode { get; set; }
        public List<User> Users { get; set; } = new ();
    }


}
