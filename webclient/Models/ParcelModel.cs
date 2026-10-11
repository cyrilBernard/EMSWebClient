using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webclient.Models
{
    public class ParcelModel
    {
        public string ItemId { get; set; }
        public string trackingid { get; set; }
        public string sender { get; set; }
        public string contact_sender { get; set; }
        public string receiver { get; set; }
        public string contact_receiver { get; set; }
        public string destfrom { get; set; }
        public string destto { get; set; }
        public string item { get; set; }
        public string itemdescription { get; set; }
        public string total { get; set; }
        public string size { get; set; }
        public string price { get; set; }
        public string payment { get; set; }
        public DateTime? datesubmit { get; set; }
        public string submitAtText { get; set; }
        public DateTime? Submittedat { get; set; }
        public string Submittedby { get; set; }
        public DateTime? Confirmat { get; set; }
        public string Confirmby { get; set; }
        public string Status { get; set; }
        public DateTime? Statusat { get; set; }
        public bool? paid { get; set; }
    }
    public class TrackModel
    {
        public List<Items> items { get; set; }
        public string countersender { get; set; }
        public string counterdest { get; set; }
        public string sender { get; set; }
        public string contactno { get; set; }
        public string receiver { get; set; }
        public string receivercontactno { get; set; }
        public string receiptno { get; set; }
        public string submitdate { get; set; }
        public string orderno { get; set; }
        public string itemid { get; set; }
        public bool? confirmed { get; set; }
        public bool? paid { get; set; }
        public decimal? totalpaid()
        {
            return items.Sum(x => decimal.Parse(x.amount));
        }
    }
    public class Items
    {
        public string itemid { get; set; }
        public string description { get; set; }
        public string itemdescription { get; set; }
        public string size { get; set; }
        public string total { get; set; }
        public string amount { get; set; }
        public string paymentstatus { get; set; }
    }
    public class TrackingModel
    {
        public string itemid { get; set; }
        public string countersender { get; set; }
        public string counterdest { get; set; }
        public string itemdesc { get; set; }
        public string itemdescription { get; set; }
        
        public string price { get; set; }
        public string paymentstatus { get; set; }
        public string paymentstatustext { get; set; }
        public string receiver { get; set; }
        public string receivercontactno { get; set; }
        public string sender { get; set; }
        public string contactno { get; set; }
        public string size { get; set; }
        public string total { get; set; }
        public string parcelstatus { get; set; }
        public string Trackingid { get; set; }
        public string destfrom { get; set; }
        public string destto { get; set; }
        public string destfromtext { get; set; }
        public string desttotext { get; set; }
        public string DateSubmitted { get; set; }
        public string type { get; set; }
        public string taskid { get; set; }
        public string routeid { get; set; }
        public string transporter { get; set; }
        public bool paid { get; set; }
        public List<SelectList> cList { get; set; }
        public List<SelectList> iList { get; set; }
        public List<SelectList> sList { get; set; }
        public List<SelectList> tList { get; set; }
        public List<SelectList> pList { get; set; }
        
    }
   
    public class OrderItemsModel
    {
        public List<TrackingModel> Items { get; set; }
        public PagingModel PagingResult { get; set; }
    }

    public class ParcelInfo
    {
        public string itemid { get; set; }
        public string trackingid { get; set; }
    }

}
