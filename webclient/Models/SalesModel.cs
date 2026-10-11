using Microsoft.AspNetCore.Mvc.Rendering;

namespace webclient.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public string? SaleId { get; set; }
        public DateTime? SaleAt { get; set; }
        public string SaleDatestr { get; set; }
        public string? action { get; set; }
        public string? Salecode { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<SelectListItem> productlist { get; set; }
        public List<salesdetails> saleslist { get; set; }
        public string? salesliststring { get; set; }
    }
    public class salesdetails
    {
        public int Id { get; set; }
        public string? SaleId { get; set; }
        public string? ProductId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice
        {
            get
            {
                return (this.Price ?? 0) * (this.Quantity ?? 0);
            }
        }
    }
}
