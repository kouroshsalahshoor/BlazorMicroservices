using System.ComponentModel.DataAnnotations.Schema;

namespace CartApi.Data
{
    public class CartDetail
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        [NotMapped]
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
