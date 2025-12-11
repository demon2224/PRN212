namespace Builder_Pattern
{
    //product
    public class Computer
    {
        public string CPU { get; set; }
        public string RAM { get; set; }

        public void ShowConfiguration()
        {
            Console.WriteLine($"Computer with {CPU}, RAM {RAM}");
        }
    }

    //Builder
    public abstract class ComputerBuilder
    {
        protected Computer computer = new Computer();
        public abstract void BuildCPU();
        public abstract void BuildRAM();
        public Computer GetComputer()
        {
            return computer;
        }
    }

    //Concrete Builder - RAM 8GB
    public class Computer8GBBuilder : ComputerBuilder
    {
        public override void BuildCPU()
        {
            computer.CPU = "Intel i5";
        }

        public override void BuildRAM()
        {
            computer.RAM = "8GB";
        }
    }

    //Concrete Builder - RAM 16GB
    public class Computer16GBBuilder : ComputerBuilder
    {
        public override void BuildCPU()
        {
            computer.CPU = "Intel i7";
        }
        public override void BuildRAM()
        {
            computer.RAM = "16GB";
        }
    }

    //Director
    public class ComputerShop
    {
        public Computer Construct(ComputerBuilder builder)
        {
            builder.BuildCPU();
            builder.BuildRAM();
            return builder.GetComputer();
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BuilderPattern:");

            var shop = new ComputerShop();

            Console.Write("Chon RAM (8/16): ");
            string choice = Console.ReadLine();

            ComputerBuilder builder = choice == "16" ? new Computer16GBBuilder() : new Computer8GBBuilder();
            Computer pc = shop.Construct(builder);

            pc.ShowConfiguration();
        }
    }
}
