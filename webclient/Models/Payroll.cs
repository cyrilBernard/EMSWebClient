using Microsoft.AspNetCore.Mvc.Rendering;

namespace webclient.Models
{
    public class Payroll
    {
        public long Id { get; set; }
        public string Payrollid { get; set; }
        public string Staffid { get; set; }
        public string Staffname { get; set; }
        public string Staffcode { get; set; }
        public string EmployeeEpfNumber { get; set; }
        public string Basic { get; set; }
        public string Commission { get; set; }
        public string Incometax { get; set; }
        public string Bonus { get; set; }
        public string Employerepf { get; set; }
        public string Epf { get; set; }
        public string Totalsalary { get; set; }
        public string Monthsalary { get; set; }
        public DateTime? Createddate { get; set; }
        public DateTime? Modifiedat { get; set; }
        public List<SelectListItem> StaffList { get; set; }
        public string Employerrate { get; set; }
        public string Employeerate { get; set; }
        public string Action { get; set; }
        public string Overtime { get; set; }

    }
}
