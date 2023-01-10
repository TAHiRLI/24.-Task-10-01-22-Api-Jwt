using FluentValidation;

namespace StoreApi.Admin.Dtos.ProductDtos
{
    public class ProductPostDto
    {
        public string  Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool StockStatus { get; set; }
        public int CategoryId { get; set; }
    }

    public class ProductPostDtoValidator: AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(x => x.CostPrice).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.SalePrice).NotNull().GreaterThanOrEqualTo(0);
          
            RuleFor(x=> x.DiscountPercent).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.ImageFile == null)
                {
                    context.AddFailure("ImageFile", "Image is required");
                }
                else if (x.ImageFile.ContentType != "image/png" && x.ImageFile.ContentType != "image/jpeg")
                {
                    context.AddFailure("ImageFile", "File Type must be jpeg or png");
                }
                else if (x.ImageFile.Length > 2097152)
                {
                    context.AddFailure("ImageFile", "File size must be less than 2mb");
                }
            });
        }
    }
}
