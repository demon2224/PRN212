using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_Pattern
{
    internal class Bently : Car
    {
        // Constructor: gán model name và giá gốc mặc định 300k
        public Bently(string model) => (ModelName, BasePrice) = (model, 300_000);
        // Cài đặt Clone bằng MemberwiseClone (shallow copy)
        public override Car Clone() => this.MemberwiseClone() as Bently;
    }
}
