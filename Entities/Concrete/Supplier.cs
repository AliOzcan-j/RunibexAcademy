using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Supplier : IEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public User User { get; set; }
        public List<Car> Cars { get; set; }
    }


}
