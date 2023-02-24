using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class SosialisasiModel
    {
        
    }
    public class SosialisasiAttribute
    {
        public string RegID { get; set; }
        public string NoBatch { get; set; }
        public int JumlahBenih { get; set; }
        public string WaktuPenyemaian { get; set; }
        public string WaktuPembelianBenih { get; set; }
        public string DokPenyemaianAttch { get; set; }
        public string DokHadirPeserta { get; set; }
        public string Varietas { get; set; }
    }
}