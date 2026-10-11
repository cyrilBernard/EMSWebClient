using Microsoft.AspNetCore.Mvc.Rendering;

namespace webclient.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Invoiceid { get; set; }
        public string Invoicenumber { get; set; }
        public DateTime? Invoicedate { get; set; }
        public string Invoicedatestr { get; set; }
        public string Customername { get; set; }
        public string Customeraddress { get; set; }
        public string Customerphonenumber { get; set; }
        public string Customeremail { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Modifiedat { get; set; }
        public decimal? Totalamount { get; set; }
        public string invoicesdetailsliststr { get; set; }
        public List<Invoicedetails> invoicedetailslist { get; set; }
        public List<SelectListItem> productlist { get; set; }
        public string? action { get; set; }
        public string Originalinvoiceno { get; set; }
        public string remarks { get; set; }

    }
    public class Invoicedetails
    {
        public int Id { get; set; }
        public string Invoiceid { get; set; }
        public string Productid { get; set; }
        public string Productdescription { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Totalprice { get; set; }
    }

}
