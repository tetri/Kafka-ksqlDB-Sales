public class Product
{
    public string Id { get; set; }  // Identifica��o �nica do produto
    public string Name { get; set; }  // Nome do produto
    public string Category { get; set; }  // Categoria do produto

    public string Description { get; set; }  // Descri��o detalhada do produto
    public decimal Price { get; set; }  // Pre�o do produto
    public string Brand { get; set; }  // Marca do produto
    public DateTime ReleaseDate { get; set; }  // Data de lan�amento do produto
    public int StockQuantity { get; set; }  // Quantidade em estoque
    public double Weight { get; set; }  // Peso do produto em quilos
    public string Dimensions { get; set; }  // Dimens�es do produto (ex: 10x20x30 cm)
    public bool IsAvailable { get; set; }  // Indica se o produto est� dispon�vel para venda
    public double Rating { get; set; }  // Classifica��o do produto (1 a 5 estrelas)
    public string SupplierId { get; set; }  // ID do fornecedor do produto
}
