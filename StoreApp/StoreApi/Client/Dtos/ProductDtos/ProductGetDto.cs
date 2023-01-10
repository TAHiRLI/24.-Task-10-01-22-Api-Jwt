namespace StoreApi.Client.Dtos.ProductDtos
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool StockStatus { get; set; }
        public CategoryInProductGetDto Category { get; set; }
    }
    public class CategoryInProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
