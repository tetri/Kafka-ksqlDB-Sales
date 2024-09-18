using System;
using System.Text.Json.Serialization;

public class Sale
{
    [JsonPropertyName("id")]
    public string Id { get; set; }  // Identificação única da venda

    [JsonPropertyName("product_id")]
    public string ProductId { get; set; }  // Identificação do produto

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }  // Quantidade do produto vendido

    [JsonPropertyName("price")]
    public decimal Price { get; set; }  // Preço unitário do produto

    [JsonPropertyName("total_value")]
    public decimal TotalValue => Quantity * Price;  // Valor total da venda

    [JsonPropertyName("buyer_id")]
    public string BuyerId { get; set; }  // Identificação do comprador

    [JsonPropertyName("seller_id")]
    public string SellerId { get; set; }  // Identificação do vendedor

    [JsonPropertyName("sale_date")]
    public DateTime SaleDate { get; set; }  // Data da venda

    [JsonPropertyName("payment_method")]
    public string PaymentMethod { get; set; }  // Método de pagamento

    [JsonPropertyName("discount")]
    public decimal Discount { get; set; }  // Desconto aplicado

    [JsonPropertyName("tax")]
    public decimal Tax { get; set; }  // Valor de imposto

    [JsonPropertyName("status")]
    public string Status { get; set; }  // Status da venda (e.g., "Pendente", "Concluída")

    [JsonPropertyName("delivery_date")]
    public DateTime? DeliveryDate { get; set; }  // Data prevista de entrega

    [JsonPropertyName("shipping_address")]
    public string ShippingAddress { get; set; }  // Endereço de entrega

    [JsonPropertyName("shipping_cost")]
    public decimal ShippingCost { get; set; }  // Custo do frete

    [JsonPropertyName("is_gift")]
    public bool IsGift { get; set; }  // Indica se a venda é um presente

    [JsonPropertyName("gift_message")]
    public string GiftMessage { get; set; }  // Mensagem de presente, se aplicável

    [JsonPropertyName("invoice_id")]
    public string InvoiceId { get; set; }  // Identificação da fatura gerada

    [JsonPropertyName("currency")]
    public string Currency { get; set; }  // Moeda usada na transação

    [JsonPropertyName("promotion_code")]
    public string PromotionCode { get; set; }  // Código promocional usado na compra

    [JsonPropertyName("delivery_status")]
    public string DeliveryStatus { get; set; }  // Status da entrega (e.g., "Em trânsito", "Entregue")
}
