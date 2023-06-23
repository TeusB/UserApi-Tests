// using System.Text;
// using RabbitMQ.Client;
// using user.Models;
// using Newtonsoft.Json;

// namespace user.RabbitMQ
// {
//     public class UserCreateService
//     {
//         private readonly ConnectionFactory _factory;
//         private readonly IConnection _connection;
//         private readonly IModel _channel;

//         public UserCreateService()
//         {
//             // _factory = new ConnectionFactory() { HostName = "rabbitmq" };
//             // _connection = _factory.CreateConnection();
//             // _channel = _connection.CreateModel();
//             // _channel.ExchangeDeclare(exchange: "user-exchange",
//             //                          type: ExchangeType.Topic,
//             //                          durable: true);
//                                     //  autoDelete: false,
//                                     //  arguments: null);

// _factory = new ConnectionFactory() { HostName = "rabbitmq" };
// _connection = _factory.CreateConnection();
// _channel = _connection.CreateModel();
// _channel.QueueDeclare(queue: "user-receive",
//                       durable: false,
//                       exclusive: false,
//                       autoDelete: false,
//                       arguments: null);
//         }

//         public void SendMessage(User user, string RoutingKey)
//         {
//             var message = JsonConvert.SerializeObject(user);
//             var body = Encoding.UTF8.GetBytes(message);
//             _channel.BasicPublish(exchange: "",
//                                   routingKey: RoutingKey,
//                                   basicProperties: null,
//                                   body: body);
//             Console.WriteLine($" [x] Sent {message}");

//             // var message = JsonConvert.SerializeObject(user);
//             // var body = Encoding.UTF8.GetBytes(message);
//             // var properties = _channel.CreateBasicProperties();
//             // properties.Persistent = true;

//             // _channel.BasicPublish(exchange: "user-exchange",
//             //                                 routingKey: RoutingKey, // Specific topic for user creation
//             //                                 mandatory: true,
//             //                                 basicProperties: properties,
//             //                                 body: body);

//             // Console.WriteLine($" [x] Sent {message}");
//         }


//         public void Dispose()
//         {
//             _channel.Close();
//             _connection.Close();
//         }
//     }
// }

using System;
using System.Text;
using RabbitMQ.Client;
using user.Models;
using Newtonsoft.Json;

namespace user.RabbitMQ
{
    public class UserCreateService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public UserCreateService()
        {
            _factory = new ConnectionFactory() { HostName = "rabbitmq" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            // _channel.ExchangeDeclare(exchange: "user-exchange",
            //                          type: ExchangeType.Topic,
            //                          durable: true);
            //                          autoDelete: false,
            //                          arguments: null);
        }

        public void SendMessage(User user, string exchange)
        {
            _channel.QueueDeclare(queue: exchange,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);
            var message = JsonConvert.SerializeObject(user);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                  routingKey: exchange,
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