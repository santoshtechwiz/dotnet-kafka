using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProducerController : ControllerBase
{
    private readonly KafkaService _kafkaService;

    public ProducerController(KafkaService kafkaService)
    {
        _kafkaService = kafkaService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string message)
    {
        await _kafkaService.SendMessageAsync(message);
        return Ok($"Message '{message}' has been sent and saved.");
    }

    [HttpGet]
    public IActionResult Get()
    {
        // Return all saved messages
        return Ok(_kafkaService.GetSavedMessages());
    }
}
