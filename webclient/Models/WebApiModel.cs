using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webclient.Models
{
    public class WebApiModel
    {
    }
    public class LoginModel
    {
        public string username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class ResponseModel
    {
        public dynamic data { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
    }
    public class RequestModel
    {
        public dynamic data { get; set; }
        public string itemid { get; set; }
        public string search { get; set; }
        public string postcode { get; set; }
        public string size { get; set; }
        public string total { get; set; }
        public string category { get; set; }
    }
    public class PostCodeModel
    {
        public string postcode { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string country { get; set; }
        public string countrycode { get; set; }
    }
}
