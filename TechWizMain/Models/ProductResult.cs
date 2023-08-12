
namespace TechWizMain.Models // Use the appropriate namespace based on your project structure
{
    public class ProductResult
    {
        public Product Product { get; set; }
        public int TotalQuantitySold { get; set; }
        public Discount Discount { get; set; }
    }
}