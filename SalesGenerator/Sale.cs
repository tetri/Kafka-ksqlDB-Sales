using System;
using System.Text.Json.Serialization;

public class Sale
{
    [JsonPropertyName("id")]
    public string Id { get; set; }  // Identifica��o �nica da venda

    [JsonPropertyName("product_id")]
    public string ProductId { get; set; }  // Identifica��o do produto

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }  // Quantidade do produto vendido

    [JsonPropertyName("price")]
    public decimal Price { get; set; }  // Pre�o unit�rio do produto

    [JsonPropertyName("total_value")]
    public decimal TotalValue => Quantity * Price;  // Valor total da venda

    [JsonPropertyName("buyer_id")]
    public string BuyerId { get; set; }  // Identifica��o do comprador

    [JsonPropertyName("seller_id")]
    public string SellerId { get; set; }  // Identifica��o do vendedor

    [JsonPropertyName("sale_date")]
    public DateTime SaleDate { get; set; }  // Data da venda

    [JsonPropertyName("payment_method")]
    public string PaymentMethod { get; set; }  // M�todo de pagamento

    [JsonPropertyName("discount")]
    public decimal Discount { get; set; }  // Desconto aplicado

    [JsonPropertyName("tax")]
    public decimal Tax { get; set; }  // Valor de imposto

    [JsonPropertyName("status")]
    public string Status { get; set; }  // Status da venda (e.g., "Pendente", "Conclu�da")

    [JsonPropertyName("delivery_date")]
    public DateTime? DeliveryDate { get; set; }  // Data prevista de entrega

    [JsonPropertyName("shipping_address")]
    public string ShippingAddress { get; set; }  // Endere�o de entrega

    [JsonPropertyName("shipping_cost")]
    public decimal ShippingCost { get; set; }  // Custo do frete

    [JsonPropertyName("is_gift")]
    public bool IsGift { get; set; }  // Indica se a venda � um presente

    [JsonPropertyName("gift_message")]
    public string GiftMessage { get; set; }  // Mensagem de presente, se aplic�vel

    [JsonPropertyName("invoice_id")]
    public string InvoiceId { get; set; }  // Identifica��o da fatura gerada

    [JsonPropertyName("currency")]
    public string Currency { get; set; }  // Moeda usada na transa��o

    [JsonPropertyName("promotion_code")]
    public string PromotionCode { get; set; }  // C�digo promocional usado na compra

    [JsonPropertyName("delivery_status")]
    public string DeliveryStatus { get; set; }  // Status da entrega (e.g., "Em tr�nsito", "Entregue")
}
