using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_Pattern
{
    public class Mustang : Car
    {
        // Constructor: gán model name và giá gốc mặc định 200k
        public Mustang(string model) => (ModelName, BasePrice) = (model, 200_000);
        // Cài đặt Clone bằng MemberwiseClone (shallow copy)
        public override Car Clone() => this.MemberwiseClone() as Mustang;
    }
}
