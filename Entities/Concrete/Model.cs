using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Model : IEntity
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public List<Color> Colors { get; set; } = new();
        public List<FuelType> FuelTypes { get; set; } = new();
    }


}
