using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webclient.Models
{
    public class CounterModel
    {
        public long Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemText { get; set; }
        public string Address { get; set; }
        public string Logitud { get; set; }
        public string Latitud { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public bool? Enabled { get; set; }
        public string ShortForm { get; set; }
    }
    public class CounterInfoModel
    {
        public long Id { get; set; }
        public string itemid { get; set; }
        public string itemtext { get; set; }
        public string countername { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string address { get; set; }
        public string logitud { get; set; }
        public string latitud { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public bool? enabled { get; set; }
        public string shortform { get; set; }
        public string method { get; set; }
        public List<OperationCounter> operation { get; set; }
        public List<ContactCounter> contact { get; set; }
        public List<ScheduleCounter> schedule { get; set; }
        public bool EnabledCounter
        {
            get => this.enabled.GetValueOrDefault();
            set { this.enabled = value; }

        }
    }
    public class OperationCounter
    {
        public long idint { get; set; }
        public string counterid { get; set; }
        public string counterday { get; set; }
        public string starttime { get; set; }
        public string startend { get; set; }
        public string loadstart { get; set; }
        public string loadend { get; set; }
    }
    public class ContactCounter
    {
        public long idint { get; set; }
        public string counterid { get; set; }
        public string category { get; set; }
        public string contactno { get; set; }
        public string contactperson { get; set; }

    }
    public class ScheduleCounter
    {
        public long id { get; set; }
        public string dest_from { get; set; }
        public string dest_to { get; set; }
        public string start_from { get; set; }
        public string start_to { get; set; }
    }

    public class VCounterSchedule
    {
        public long Id { get; set; }
        public string DestFrom { get; set; }
        public string FromCounter { get; set; }
        public string DestTo { get; set; }
        public string ToCounter { get; set; }
        public List<FromKaunter> ListTime { get; set; }
    }
    public class FromKaunter
    {
        public string FromID { get; set; }
        public string DestFrom { get; set; }
        public string DestTo { get; set; }
        public string StartFrom { get; set; }
        public string StartTo { get; set; }

    }
    public class PriceModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Size { get; set; }
        public decimal? Amount { get; set; }
        public bool? Enabled { get; set; }
        public string Sizetext { get; set; }
        
    }
    public class TermsModel
    {
        public string parcelid { get; set; }
        public string parceltype { get; set; }
        public string parcelname { get; set; }
        public string doctype { get; set; }
        public string docext { get; set; }
        public string docname { get; set; }
        public byte[] doccontent { get; set; }
        public string type { get; set; }
    }
}
