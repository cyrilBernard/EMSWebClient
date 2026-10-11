using System;
using System.Collections.Generic;
namespace webclient.Models
{
    public class ProductsCategory
    {
        public int Id { get; set; }
        public string? Productid { get; set; }
        public string? Productcode { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? Createddate { get; set; }
        public DateTime? Modifiedat { get; set; }
        public List<ProductSubCategory>? productSubCategories { get; set; }
        public string? productsubcatstring { get; set; }
    }
    public class ProductSubCategory
    {
        public int Id { get; set; }
        public string? Parentid { get; set; }
        public string? Itemkey { get; set; }
        public string? Itemtext { get; set; }
        public sbyte? Status { get; set; }
    }
}
