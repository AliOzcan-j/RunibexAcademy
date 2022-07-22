using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Color : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Car> Cars { get; set; } = new();
        public List<Model> Models { get; set; } = new();
    }


}
