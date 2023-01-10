using FluentValidation;

namespace StoreApi.Admin.Dtos.ProductDtos
{
    public class ProductPutDto
    {
        public string Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool StockStatus { get; set; }
        public int CategoryId { get; set; }
    }

    public class ProductPutDtoValidator : AbstractValidator<ProductPutDto>
    {
        public ProductPutDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(20);
            RuleFor(x => x.CostPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SalePrice).GreaterThanOrEqualTo(0);

            RuleFor(x => x.DiscountPercent).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);

            RuleFor(x => x.ImageFile)
                .Must(x => x == null || x.ContentType == "image/png" || x.ContentType == "image/jpeg").WithMessage("File type must be png,jpg or jpeg")
                .Must(x => x == null || x.Length <= 2097152).WithMessage("File size must be less or equal than 2MB");
        }
    }
}
