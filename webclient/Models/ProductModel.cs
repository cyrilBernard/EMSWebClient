using Microsoft.AspNetCore.Mvc.Rendering;

namespace webclient.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductId { get; set; }
        public string? ProductCode { get; set; }
        public string? Productname { get; set; }
        public string? Productcategory { get; set; }
        public string? Productdescription { get; set; }
        public DateTime? Modifiedat { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? CurrentStock { get; set; }
        public decimal? Price { get; set; }
        public string? action { get; set; }
        public sbyte? isDeleted { get; set; }
        public string? ProductModel { get; set; }
        public string? ProductBrand { get; set; }
        public string? Productsubcategory { get; set; }
        public string? productlist { get; set; }
        public List<SelectListItem> productlistcategory { get; set; }
        public List<SelectListItem> productlistsubcategory { get; set; }
        public decimal? TotalPrice
        {
            get
            {
                return (this.Price ?? 0) * (this.Quantity ?? 0);
            }
        }
    }
}
