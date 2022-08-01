using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int FuelTypeId { get; set; }
        public bool? MilageLimit { get; set; }
        public bool Transmission { get; set; }
        public decimal DailyPrice { get; set; }
        public bool? IsDeleted { get; set; }
        public Brand Brand { get; set; }
        public Model Model { get; set; }
        public Color Color { get; set; }
        public FuelType FuelType { get; set; }
        public List<CarImage> CarImages { get; set; }
        public List<Rental> Rentals { get; set; }
    }


}
