using Microsoft.Extensions.Configuration;
using off_tone.Infrastructure.AsyncDataServices.Interfaces;
using off_tone.Infrastructure.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace off_tone.Infrastructure.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IModel _channel;
        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"]),
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Connected to Message Bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
            }
        }

        public void PublishLoginEvent(PublishLoginEventDto publishLoginEventDto)
        {
            var message = JsonSerializer.Serialize(publishLoginEventDto);

            if(_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ connection is open, sending the login message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ connection is closed, not sending the message.");
            }
        }

        public void PublishRegisterEvent(PublishRegisterEventDto publishRegisterEventDto)
        {
            var message = JsonSerializer.Serialize(publishRegisterEventDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ connection is open, sending the register message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ connection is closed, not sending the message.");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                                routingKey: "",
                                basicProperties: null,
                                body: body);
            Console.WriteLine($"--> Message sent: {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("Message Bus is disposed.");

            if (!_channel.IsOpen) return;
            _channel.Close();
            _connection.Close();
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection is shutdown");
        }
    }
}
