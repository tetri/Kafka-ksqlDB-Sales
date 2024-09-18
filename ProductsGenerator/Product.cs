public class Product
{
    public string Id { get; set; }  // Identificação única do produto
    public string Name { get; set; }  // Nome do produto
    public string Category { get; set; }  // Categoria do produto

    public string Description { get; set; }  // Descrição detalhada do produto
    public decimal Price { get; set; }  // Preço do produto
    public string Brand { get; set; }  // Marca do produto
    public DateTime ReleaseDate { get; set; }  // Data de lançamento do produto
    public int StockQuantity { get; set; }  // Quantidade em estoque
    public double Weight { get; set; }  // Peso do produto em quilos
    public string Dimensions { get; set; }  // Dimensões do produto (ex: 10x20x30 cm)
    public bool IsAvailable { get; set; }  // Indica se o produto está disponível para venda
    public double Rating { get; set; }  // Classificação do produto (1 a 5 estrelas)
    public string SupplierId { get; set; }  // ID do fornecedor do produto
}
