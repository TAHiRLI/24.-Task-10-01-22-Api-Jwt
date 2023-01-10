using FluentValidation;

namespace StoreApi.Admin.Dtos.ProductDtos
{
    public class ProductPostDto
    {
        public string  Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool StockStatus { get; set; }
        public int CategoryId { get; set; }
    }

    public class ProductPostDtoValidator: AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(20).NotEmpty().NotNull();
            RuleFor(x => x.CostPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SalePrice).GreaterThanOrEqualTo(0);
            RuleFor(x=> x.DiscountPercent).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);

        }
    }
}
