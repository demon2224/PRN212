namespace Prototype_Pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Prototype Pattern Demo \n");

            // 2 object gốc
            Car mustang = new Mustang("Mustang EcoBoost");
            Car bently = new Bently("Continental GT Mulliner");

            Console.WriteLine($"car : {mustang.ModelName} ,base price : {mustang.BasePrice}");
            Console.WriteLine($"car : {bently.ModelName} ,base price : {bently.BasePrice}");
            // Clone từ mustang và tính giá lăn bánh
            Car Car;
            Car = mustang.Clone();
            Car.OnRoadPrice = Car.BasePrice + Car.SetAdditionalPrice();
            Console.WriteLine($"car : {Car.ModelName} ,base price : {Car.OnRoadPrice}");

            Car = bently.Clone();
            Car.OnRoadPrice = Car.BasePrice + Car.SetAdditionalPrice();
            Console.WriteLine($"car : {Car.ModelName} ,base price : {Car.OnRoadPrice}");
        }
    }
}
