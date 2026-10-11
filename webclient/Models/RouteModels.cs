using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webclient.Models
{
    public class RouteModels
    {

    }
    
    public  class Wfroute
    {
        public long Id { get; set; }
        public string Routeid { get; set; }
        public string Routename { get; set; }
        public string Counterid { get; set; }
        public DateTime? Createdat { get; set; }
        public string Createdby { get; set; }
        public DateTime? Lastmodifiedat { get; set; }
        public string Lastmodifedby { get; set; }
        public bool? Enabled { get; set; }
        public string Description { get; set; }
        public List<Wfroutedetails> RouteDetails { get; set; }
    }
    public class Wfroutedetails
    {
        public long Id { get; set; }
        public string Routeid { get; set; }
        public string Itemid { get; set; }
        public string Counterid { get; set; }
        public int? Routeindex { get; set; }
        public int? ComPercentage { get; set; }
    }
}
