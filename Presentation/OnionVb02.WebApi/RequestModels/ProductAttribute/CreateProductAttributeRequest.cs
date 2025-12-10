namespace OnionVb02.WebApi.RequestModels.ProductAttribute
{
    public class CreateProductAttributeRequest
    {
        public string AttributeName { get; set; }
        public string AttributeType { get; set; }
        public string? Description { get; set; }
    }
}
