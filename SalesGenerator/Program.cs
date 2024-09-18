using System;
using System.Text.Json;
using System.Threading.Tasks;

using Bogus;

using Confluent.Kafka;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        var productIds = Enumerable.Range(1, 10).Select(x => x.ToString()).ToList();
        var buyerIds = Enumerable.Range(1, 100).Select(x => x.ToString()).ToList();
        var sellerIds = Enumerable.Range(1, 20).Select(x => x.ToString()).ToList();
        var paymentMethods = new[] { "Credit Card", "Debit Card", "Cash", "Transfer" };
        var saleStatuses = new[] { "Pending", "Completed", "Cancelled" };
        var deliveryStatuses = new[] { "In transit", "Delivered", "Awaiting shipment" };

        var faker = new Faker<Sale>("pt_BR")
            .RuleFor(s => s.Id, f => Guid.NewGuid().ToString())
            .RuleFor(s => s.ProductId, f => f.PickRandom(productIds))
            .RuleFor(s => s.Quantity, f => f.Random.Int(1, 10))
            .RuleFor(s => s.Price, f => f.Finance.Amount(10, 100))
            .RuleFor(s => s.BuyerId, f => f.PickRandom(buyerIds))
            .RuleFor(s => s.SellerId, f => f.PickRandom(sellerIds))
            .RuleFor(s => s.SaleDate, f => f.Date.Recent(30))
            .RuleFor(s => s.PaymentMethod, f => f.PickRandom(paymentMethods))
            .RuleFor(s => s.Discount, f => f.Finance.Amount(0, 10))
            .RuleFor(s => s.Tax, f => f.Finance.Amount(1, 15))
            .RuleFor(s => s.Status, f => f.PickRandom(saleStatuses))
            .RuleFor(s => s.DeliveryDate, f => f.Date.Soon(10))
            .RuleFor(s => s.ShippingAddress, f => f.Address.FullAddress())
            .RuleFor(s => s.ShippingCost, f => f.Finance.Amount(5, 20))
            .RuleFor(s => s.IsGift, f => f.Random.Bool())
            .RuleFor(s => s.GiftMessage, (f, s) => s.IsGift ? f.Lorem.Sentence() : null)
            .RuleFor(s => s.InvoiceId, f => f.Random.Guid().ToString())
            .RuleFor(s => s.Currency, f => "BRL")
            .RuleFor(s => s.PromotionCode, f => f.Random.AlphaNumeric(6).ToUpper())
            .RuleFor(s => s.DeliveryStatus, f => f.PickRandom(deliveryStatuses));

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        for (int i = 0; i < 100000; i++)
        {
            var sale = faker.Generate();
            var saleJson = JsonSerializer.Serialize(sale, options);

            await producer.ProduceAsync("sales", new Message<Null, string> { Value = saleJson });

            Console.WriteLine($"Venda {sale.Id} de {sale.ProductId} enviada ({i + 1}/100000)");

            //await Task.Delay(100);
        }

        producer.Flush(TimeSpan.FromSeconds(10));
    }
}
