using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webclient.Models
{
    public class Models
    {
        public int? id { get; set; }
        public string size { get; set; }
        public decimal? price { get; set; }
        public string method { get; set; }
    }
    public class PricesModel
    {
        public int? id { get; set; }
        public string size { get; set; }
        public decimal? price { get; set; }
        public string method { get; set; }
    }
    public class DataModel
    {
        public dynamic data { get; set; }
        public dynamic data2 { get; set; }
        public bool StatusCode { get; set; }
    }
    public class formmodels
    {
        public dynamic Data { get; set; }
        public dynamic Data2 { get; set; }
        public bool Status { get; set; }
        public string Error { get; set; }
        public string ErrorDesc { get; set; }
    }
    public class PageFilter
    {

        public string Value { get; set; }

        public int PageSize { get; set; } = 20;

        public int PageNumber { get; set; } = 1;
    }
    public class Response
    {
        public dynamic items { get; set; }
        public PagingModel pageresult { get; set; }
        public int TotalPage { get; set; }
        public int PageNumber { get; set; }
        public int PageSize
        {
            get; set;
        }
        public bool HasPreviousPage
        {
            get; set;
        }

        public bool HasNextPage
        {
            get; set;
        }
    }
    public class PagingResult<T>
    {
        public List<T>? Items { get; set; }
        public PagingModel? Result { get; set; }
    }
 
    public class PagingModel
    {
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int PageNumber { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string? currentfilter { get; set; }
        public string? currentsort { get; set; }
        public string? Controller { get; set; }
    }
    public class SelectList
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
    }
    public class ProductPrice
    {
        public string productid { get; set; }
        public string price { get; set; }
    }
    public class ProductQuantity
    {
        public string productid { get; set; }
        public string? Quantity { get; set; }
    }
    public class PasswordModel
    {
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string currentpassword { get; set; }
    }
    public class Menusetting
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Label { get; set; }
        public string Htmlstyle { get; set; }
    }
}
