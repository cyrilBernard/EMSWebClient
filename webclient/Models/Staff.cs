using webclient.Modules;

namespace webclient.Models
{
    public class Staff
    {
        public long Id { get; set; }
        public string Staffid { get; set; }
        public string Staffcode { get; set; }
        public string Staffname { get; set; }
        public string Staffposition { get; set; }
        public string Icno { get; set; }
        public string Address { get; set; }
        public string Incometax { get; set; }
        public string Epfnumber { get; set; }
        public string Soscono { get; set; }
        public string Epfpercent { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public DateTime? Createddate { get; set; }
        public DateTime? Modifieddate { get; set; }
        public string Action { get; set; }
        public decimal? Basicsalary { get; set; }
        public string Department { get; set; }
        public DateTime? Datejoin { get; set; } = DateTime.Now;
        public string? Datejointxt { get; set; }
        public string Role { get; set; }
        public string? TempPassword { get; set; }
    }
    public class StaffDetailsSelected
    {
        public string employeename { get; set; }
        public decimal? basicsalary { get; set; }
        public decimal? employeerate { get; set; }
        public decimal? employerrate { get; set; }

    }
}
