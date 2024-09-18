using Bogus;

using Confluent.Kafka;

using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "127.0.0.1:9092"
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        int productId = 1;

        // Dados agrícolas
        var categories = new[] { "Sementes", "Fertilizantes", "Defensivos Agrícolas", "Máquinas Agrícolas", "Irrigação", "Ferramentas" };
        var brands = new[] { "AgroBrandA", "AgroBrandB", "AgroBrandC", "AgroTech", "FarmTech", "PlantPower" };
        var suppliers = new[] { "SupplierA", "SupplierB", "SupplierC", "AgroSupplier", "RuralSupplier" };

        var faker = new Faker<Product>("pt_BR")
            .RuleFor(p => p.Id, f => productId++.ToString())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName() + " " + f.PickRandom(new[] { "Premium", "Organic", "Pro", "Eco" }))
            .RuleFor(p => p.Category, f => f.PickRandom(categories))
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.Price, f => f.Finance.Amount(50, 10000))  // Produtos agrícolas variando de R$50 a R$10.000
            .RuleFor(p => p.Brand, f => f.PickRandom(brands))
            .RuleFor(p => p.ReleaseDate, f => f.Date.Past(5))  // Produtos lançados nos últimos 5 anos
            .RuleFor(p => p.StockQuantity, f => f.Random.Int(0, 500))  // Estoque entre 0 e 500 unidades
            .RuleFor(p => p.Weight, f => f.Random.Double(0.5, 100))  // Peso variando de 0.5kg a 100kg, para insumos e máquinas
            .RuleFor(p => p.Dimensions, f => $"{f.Random.Double(10, 200)}x{f.Random.Double(10, 200)}x{f.Random.Double(10, 200)} cm")
            .RuleFor(p => p.IsAvailable, f => f.Random.Bool())
            .RuleFor(p => p.Rating, f => f.Random.Double(1, 5))  // Avaliação entre 1 e 5
            .RuleFor(p => p.SupplierId, f => f.PickRandom(suppliers));

        var products = faker.Generate(10);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        foreach (var product in products)
        {
            var productJson = JsonSerializer.Serialize(product, options);

            var result = await producer.ProduceAsync("products", new Message<Null, string> { Value = productJson });
            Console.WriteLine($"Produto {product.Id} enviado para o tópico {result.TopicPartitionOffset}");
        }

        producer.Flush(TimeSpan.FromSeconds(10));
    }
}
