using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Model> Models { get; set; } = new();
        public List<Car> Cars { get; set; } = new();
    }


}
