using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webclient.Models
{
    public class FilesModel
    {
        public string filedescription{ get; set; }
        public string filename { get; set; }
        public List<Document> documents { get; set; }
    }
    public class Document
    {
        public string Itemid { get; set; }
        public string Docname { get; set; }
        public string Category { get; set; }
        public string Docext { get; set; }
        public string Doctype { get; set; }
        public DateTime? Createddate { get; set; }
        public string Createdby { get; set; }
        public byte[] Doccontent { get; set; }
        public string Docdecriptions { get; set; }
        public bool? Publish { get; set; }
    }
    public class FileModel
    {
        public string filedesc { get; set; }
        public List<IFormFile> files { get; set; }
    }
    public class Download
    {
        public string filename { get; set; }
        public byte[] content { get; set; }
        public string contenttype { get; set; }
    }
}
