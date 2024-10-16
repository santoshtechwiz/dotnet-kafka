using System.Threading.Tasks;

public class KafkaService
{
    private readonly KafkaProducer _kafkaProducer;
    private readonly MessageRepository _messageRepository;

    public KafkaService(KafkaProducer kafkaProducer, MessageRepository messageRepository)
    {
        _kafkaProducer = kafkaProducer;
        _messageRepository = messageRepository;
    }

    public async Task SendMessageAsync(string message)
    {
        // Save message to repository (database simulation)
        _messageRepository.SaveMessage(message);

        // Send message to Kafka
        await _kafkaProducer.SendMessageAsync(message);
    }

    internal List<string> GetSavedMessages()
    {
            return _messageRepository.GetAllMessages().ToList();
    }
}
