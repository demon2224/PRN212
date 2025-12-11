namespace Observer_Pattern
{
    // Observer interface
    public interface ISubscriber
    {
        void Update(string message);
        string GetName();
    }

    // Concrete Observer
    public class User : ISubscriber
    {
        private string name;
        public User(string name)
        {
            this.name = name;
        }
        public void Update(string message)
        {
            Console.WriteLine($"{name} nhan thong bao: {message}");
        }

        public string GetName()
        {
            return name;
        }
    }

    // Subject (Publisher)
    public class EventPublisher
    {
        private List<ISubscriber> subscribers = new List<ISubscriber>();
        public void Subscribe(ISubscriber subscriber)
        {
            subscribers.Add(subscriber);
            Console.WriteLine($"{subscriber.GetName()} da SUBSCRIBE.");
        }
        public void Unsubscribe(ISubscriber subscriber)
        {
            subscribers.Remove(subscriber);
            Console.WriteLine($"{subscriber.GetName()} da UNSUBSCRIBE.");
        }
        public void Notify(string message)
        {
            Console.WriteLine($"\nTHONG BAO: {message}");
            foreach (var subscriber in subscribers)
            {
                subscriber.Update(message);
            }
        }

        public void ShowSubscribers()
        {
            Console.WriteLine("\nDanh sach subscribers hien tai:");
            if (subscribers.Count == 0)
            {
                Console.WriteLine("(Khong co ai subscriber)");
            }
            else
            {
                foreach (var sub in subscribers)
                {
                    Console.WriteLine($"{sub.GetName()}");
                }
            }
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Observer_Pattern:");

            var publisher = new EventPublisher();

            var user1 = new User("A");
            var user2 = new User("B");
            var user3 = new User("C");

            publisher.Subscribe(user1);
            publisher.Subscribe(user2);
            publisher.Subscribe(user3);

            publisher.ShowSubscribers();

            publisher.Notify("sale 50%!");

            publisher.Unsubscribe(user2);

            publisher.ShowSubscribers();


            publisher.Notify("sale 30%!");
        }
    }
}
