namespace AbstractFactory_Pattern
{
    // Product Interfaces
    public interface ITable
    {
        void Show();
    }
    public interface IChair
    {
        void Show();
    }
    public interface ICarpet
    {
        void Show();
    }

    // Concrete Products - Modern
    public class ModernTable : ITable
    {
        public void Show()
        {
            Console.WriteLine("This is a Modern Table.");
        }
    }
    public class ModernChair : IChair
    {
        public void Show()
        {
            Console.WriteLine("This is a Modern Chair.");
        }
    }
    public class ModernCarpet : ICarpet
    {
        public void Show()
        {
            Console.WriteLine("This is a Modern Carpet.");
        }
    }

    // Concrete Products - Classic
    public class ClassicTable : ITable
    {
        public void Show()
        {
            Console.WriteLine("This is a Classic Table.");
        }
    }
    public class ClassicChair : IChair
    {
        public void Show()
        {
            Console.WriteLine("This is a Classic Chair.");
        }
    }
    public class ClassicCarpet : ICarpet
    {
        public void Show()
        {
            Console.WriteLine("This is a Classic Carpet.");
        }
    }

    // Abstract Factory
    public interface IFurnitureFactory
    {
        ITable CreateTable();
        IChair CreateChair();
        ICarpet CreateCarpet();
    }

    // Concrete Factories
    public class ModernFurnitureFactory : IFurnitureFactory
    {
        public ITable CreateTable()
        {
            return new ModernTable();
        }
        public IChair CreateChair()
        {
            return new ModernChair();
        }
        public ICarpet CreateCarpet()
        {
            return new ModernCarpet();
        }
    }
    public class ClassicFurnitureFactory : IFurnitureFactory
    {
        public ITable CreateTable()
        {
            return new ClassicTable();
        }
        public IChair CreateChair()
        {
            return new ClassicChair();
        }
        public ICarpet CreateCarpet()
        {
            return new ClassicCarpet();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Abstract Factory");
            IFurnitureFactory factory;

            Console.WriteLine("Enter furniture style (modern/classic): ");
            string type = Console.ReadLine().ToLower();

            factory = type == "classic" ? new ClassicFurnitureFactory() : new ModernFurnitureFactory();

            var table = factory.CreateTable();
            var chair = factory.CreateChair();
            var carpet = factory.CreateCarpet();

            table.Show();
            chair.Show();
            carpet.Show();
        }
    }
}
