using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webclient.Models
{
    public class UsersModel
    {
    }
    public class UserProfileModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public string itemid { get; set; }
        public string usertype { get; set; }
        public string ExpiredCtrl { get; set; }
    }
    public class RegisterModel
    {
        public string itemid { get; set; }
        public string username { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public bool? Enabled { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phoneno { get; set; }
        public string Email { get; set; }
        public string Usertype { get; set; }
        public string method { get; set; }
        public string counterid { get; set; }
        public string carno { get; set; }
    }
    public class UserModel
    {
        public string? itemid { get; set; }
        public string? userid { get; set; }
        public string? username { get; set; }
        public string? usertype { get; set; }
        public string? email { get; set; }
        public string? phoneno { get; set; }
        public bool? enabled { get; set; }
        public string? Countername { get; set; }
        public string? counterid { get; set; }
    }
    public class RefreshModel
    {
        public string? refreshtoken { get; set; }
        public string? AccessToken { get; set; }
        public string? ipaddress { get; set;  }
    }
}
