using System.Text;
using RabbitMQ.Client;
using user.Models;
using Newtonsoft.Json;

namespace user.RabbitMQ
{
    public class UserDeleteService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public UserDeleteService()
        {
            _factory = new ConnectionFactory() { HostName = "rabbitmq" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "user-exchange", type: ExchangeType.Topic);

        }

        public void SendMessage(User user)
        {
            var message = JsonConvert.SerializeObject(user);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "user-exchange",
                                              routingKey: "user.delete", // Specific topic for user creation
                                              basicProperties: null,
                                              body: body);

            Console.WriteLine($" [x] Sent {message}");
        }


        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}