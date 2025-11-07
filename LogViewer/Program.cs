namespace LogViewer;

class Program
{
    static void Main(string[] args)
    {
        Subscriber subscriber = new Subscriber();

        subscriber.StartConsuming();

        Console.ReadLine();

        subscriber.DisposeResources();
    }
}
