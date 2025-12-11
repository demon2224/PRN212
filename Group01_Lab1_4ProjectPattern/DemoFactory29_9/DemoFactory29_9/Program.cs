namespace DemoFactory29_9
{
    using static System.Console; //static directive 
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Factory Method Demo");
            WriteLine("*");

            List<AnimalFactory> aFList = new List<AnimalFactory>()
            { };
            AnimalFactory fac = new AnimalFactory();

            string anim = ReadLine();
            fac.CreateAnimal(anim).bahavior();

            Tiger tig = new Tiger();

            ReadLine();
        }

        public interface IAnimal
        {
            void bahavior();

        }

        public class Lion : IAnimal
        {
            public void bahavior()
            {
                WriteLine("Lion catch a rabit");
                Console.WriteLine("This is Lion");
            }

        }

        public class Tiger : IAnimal
        {
            public void bahavior()
            {
                WriteLine("This is Tiger");
                WriteLine("Tiger hunt a deer");
            }
        }

        public class AnimalFactory
        {
            public IAnimal CreateAnimal(string ani)
            {
                if (ani == "tiger")
                {
                    return new Tiger();
                }
                else if (ani == "lion")
                {
                    return new Lion();

                }
                return null;
            }
        }
    }
}
