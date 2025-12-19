using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

public class Subscriber
{
    private readonly string _hostname = "localhost"; //cambiar por la dirección que corresponda
    private readonly string _queueName = "";
    private readonly string _userName = "guest"; //utilizar las credenciales de un usuario de RabbitMQ
    private readonly string _password = "guest";
    private readonly string _exchangeName = "log_topic";
    private readonly int _port = 5672;     //reemplazar por el puerto AMQP de RabbitMQ

    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IBasicProperties _properties;

    public Subscriber()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostname,
            UserName = _userName,
            Password = _password,
            Port = _port
        };

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();
        _properties = _channel.CreateBasicProperties();
        _properties.Persistent = true;

        _channel.ExchangeDeclare(_exchangeName, ExchangeType.Topic, durable: true);

        var tempQueue = _channel.QueueDeclare(
                        queue: _queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

        _queueName = tempQueue.QueueName;
    }

    public void StartConsuming(string topicKey)
    {
        var consumer = new EventingBasicConsumer(_channel);

        _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: topicKey);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var log = JsonSerializer.Deserialize<LogEntry>(json);

            Console.WriteLine($"Log recibido: {log.Message}\n");
        };

        _channel.BasicConsume(
            queue: _queueName,
            autoAck: true,
            consumer: consumer
        );
    }

    public void DisposeResources()
    {
        _channel?.Close();
        _connection?.Close();
    }
}