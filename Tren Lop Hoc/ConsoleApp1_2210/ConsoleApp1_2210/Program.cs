using ConsoleApp1_2210.Models;

namespace ConsoleApp1_2210
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyStoreContext context = new MyStoreContext();

            var products = context.Products;
            var items = from p in products
                        select new { p.ProductName, p.CategoryId };
            foreach (var item in items)
            {
                Console.WriteLine($"{item.ProductName} - {item.CategoryId}");
            }

            Console.WriteLine("Moi ban nhap ten san pham moi: ");
            String newName = Console.ReadLine();
            products.Add(new Product
            {
                ProductName = newName,
                CategoryId = 1,
                UnitPrice = 100,
                UnitsInStock = 10
            });
            context.SaveChanges();

            Console.WriteLine("Moi ban nhap san pham muon xoa");
            String delName = Console.ReadLine();
            products.RemoveRange(products.Where(p => p.ProductName == delName));
            context.SaveChanges();
        }
    }
}
