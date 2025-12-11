using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_Pattern
{
    // Lớp trừu tượng Car - cha của các loại xe
    public abstract class Car
    {
        protected int basePrice = 0, onRoadPrice = 0;
        public String ModelName { get; set; }
        // Property cho giá gốc
        public int BasePrice { get { return basePrice; } set { basePrice = value; } }
        // Property cho giá lăn bánh
        public int OnRoadPrice { get { return onRoadPrice; } set { onRoadPrice = value; } }
        // Hàm tạo giá phát sinh ngẫu nhiên 
        public static int SetAdditionalPrice()
        {
            Random random = new Random();
            int additionnalPrice = random.Next(200_000, 500_000);
            return additionnalPrice;
        }
        // Hàm Clone trừu tượng - các lớp con phải override
        public abstract Car Clone();
    }
}
