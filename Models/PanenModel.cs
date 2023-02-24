using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PanenModel
    {
    }
    public class PanenAttribute { 
        public string NoBatch { get; set; }
        public string DokumentasiAttch { get; set; }
        public string WaktuPanenStart { get; set; }
        public string WaktuPanenEnd { get; set; }
        public int QtyPanen { get; set; }
        public string UsiaPanen { get; set; }
        public string TanggalPanen { get; set; }
    }
}