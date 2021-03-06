// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderProcessor.Models;
using System.Net;

Console.WriteLine("Order Processor");
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .Build();

var config = new ConsumerConfig
{
    BootstrapServers = configuration.GetSection("KafkaSettings").GetSection("Server").Value,
    GroupId = "tester",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

//Connect to Kafka
var topic = "finalcase";
CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

using (var consumer = new ConsumerBuilder<string, string>(config).Build())
{

    Console.WriteLine("Connected");
    consumer.Subscribe(topic);
    try
    {
        while (true)
        {
            var cr = consumer.Consume(cts.Token);
            Console.WriteLine($"Consumed record with key: {cr.Message.Key} and value: {cr.Message.Value}");

            // EF
            using (var context = new StudyCaseContext())
            {
                Order order = JsonConvert.DeserializeObject<Order>(cr.Message.Value);
                order.OrderCode = cr.Message.Key;
                order.Created = DateTime.Now;
                order.OrderContent = cr.Message.Value;

                context.Orders.Add(order);
                context.SaveChanges();
                Console.WriteLine("Order Submitted");
            }

        }
    }
    catch (OperationCanceledException)
    {
        // Ctrl-C was pressed.
    }
    finally
    {
        consumer.Close();
    }
}
