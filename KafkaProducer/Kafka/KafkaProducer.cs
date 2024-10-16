using Confluent.Kafka;
using System;
using System.Threading.Tasks;

public class KafkaProducer
{
    private readonly string _bootstrapServers;
    private readonly string _topic;

    public KafkaProducer(string bootstrapServers, string topic)
    {
        _bootstrapServers = bootstrapServers;
        _topic = topic;
    }

    public async Task SendMessageAsync(string message)
    {
        var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            try
            {
                var deliveryResult = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
                Console.WriteLine($"Message sent to topic: {_topic}, partition: {deliveryResult.Partition}, offset: {deliveryResult.Offset}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error producing message: {ex.Message}");
            }
        }
    }
}
