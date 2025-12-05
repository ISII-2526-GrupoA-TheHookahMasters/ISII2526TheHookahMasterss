namespace LogViewer;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- VISOR DE LOGS RABBITMQ ---");
        Console.WriteLine("Selecciona qué logs quieres escuchar:");
        Console.WriteLine("1. Errores (topic: *.error)");
        Console.WriteLine("2. Información (topic: *.info)");
        Console.WriteLine("3. Traza (topic: *.trace)");
        Console.WriteLine("4. Debug (topic: *.debug)");
        Console.WriteLine("5. Warning (topic: *.warning)");
        Console.WriteLine("6. Critical (topic: *.critical)");
        Console.WriteLine("7. Salir");
        Console.Write("Opción: ");

        var input = Console.ReadLine();
        string topicKey = "";

        switch (input)
        {
            case "1": topicKey = "*.error"; break;
            case "2": topicKey = "*.information"; break;
            case "3": topicKey = "*.trace"; break;
            case "4": topicKey = "*.debug"; break;
            case "5": topicKey = "*.warning"; break;
            case "6": topicKey = "*.critical"; break;
            case "7": return;
            default: topicKey = "#"; break;
        }

        Subscriber subscriber = new Subscriber();

        subscriber.StartConsuming(topicKey);

        Console.WriteLine($"Escuchando logs con el patrón: {topicKey}...");

        Console.ReadLine();

        subscriber.DisposeResources();
    }
}
