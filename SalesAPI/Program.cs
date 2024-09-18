using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/api/sales", async () =>
{
    var ksqlDbUrl = "http://localhost:8088/query"; // Substitua pela URL do seu ksqlDB
    var ksqlQuery = "SELECT * FROM TOTAL_SALES_BY_PRODUCT;";
    var httpClient = new HttpClient();

    // Configura a solicitação para o ksqlDB
    var request = new HttpRequestMessage(HttpMethod.Post, ksqlDbUrl)
    {
        Content = new StringContent(
            JsonSerializer.Serialize(new { ksql = ksqlQuery }),
            System.Text.Encoding.UTF8,
            "application/vnd.ksql.v1+json")
    };

    // Envia a solicitação e obtém a resposta
    var response = await httpClient.SendAsync(request);
    response.EnsureSuccessStatusCode();

    // Lê e retorna o conteúdo da resposta
    var responseBody = await response.Content.ReadAsStringAsync();
    var salesData = JsonSerializer.Deserialize<object>(responseBody);

    return Results.Ok(salesData);
});

app.Run();
