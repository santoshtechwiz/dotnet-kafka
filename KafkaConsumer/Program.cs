using Confluent.Kafka;
using System;
using System.Threading;

class Program
{
    private static readonly string _bootstrapServers = "localhost:9092";
    private static readonly string _topic = "test-topic";
    private static readonly string _groupId = "consumer-group-1";

    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _bootstrapServers,
            GroupId = _groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
        {
            consumer.Subscribe(_topic);
            Console.WriteLine($"Subscribed to topic: {_topic}");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // Prevent the process from terminating.
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    var consumeResult = consumer.Consume(cts.Token);
                    Console.WriteLine($"Received message: {consumeResult.Message.Value} at {consumeResult.TopicPartitionOffset}");
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}
