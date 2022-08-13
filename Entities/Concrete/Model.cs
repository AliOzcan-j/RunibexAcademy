using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Model : IEntity
    {
        public int Id { get; set; }
        public string NamePrefix { get; set; }
        public string? NameSuffix { get; set; }
        public string ModelYear { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<Car> Cars { get; set; } = new();
    }


}
