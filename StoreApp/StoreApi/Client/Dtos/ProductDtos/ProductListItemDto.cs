namespace StoreApi.Client.Dtos.ProductDtos
{
    public class ProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool StockStatus { get; set; }
        public int CategoryId { get; set; }
    }
}
