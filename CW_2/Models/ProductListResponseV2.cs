namespace CW_2.Models
{
    public class ProductListResponseV2
    {
        public List<ProductV2> Data { get; set; } = new List<ProductV2>();
        public int Count { get; set; }

    }
}
