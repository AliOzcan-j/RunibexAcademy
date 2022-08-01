using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CarDetailDto
    {
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ColorName { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }//1 ise otomatik 0 ise manuel
        public string Milage { get; set; }//1 ise 1500km limit 0 ise limitsiz
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }//tüm özelliklerini ardarda eklenerek oluşacak
    }
}
