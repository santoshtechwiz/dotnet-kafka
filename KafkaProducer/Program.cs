var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Kafka settings (hardcoded for now, but you could move these to configuration)
string kafkaBootstrapServers = "localhost:9092";
string kafkaTopic = "test-topic";

// Add DI for Kafka producer, service, and repository
builder.Services.AddSingleton(new KafkaProducer(kafkaBootstrapServers, kafkaTopic));
builder.Services.AddSingleton<KafkaService>();
builder.Services.AddSingleton<MessageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
