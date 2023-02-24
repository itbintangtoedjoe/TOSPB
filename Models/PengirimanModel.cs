using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PengirimanModel
    {
    }
    public class PengirimanAttribute {
        public string NoBatch { get; set; }
		public int JumlahSimplisia { get; set; }
		public string NoPO { get; set; }
		public int JumlahPO { get; set; }
		public string WaktuPengiriman { get; set; }
		public string WaktuSampai { get; set; }
        public int JumlahSimplisiaAyakan { get; set; }
        public int JumlahSimplisiaRusak { get; set; }
        public int JumlahDiterima { get; set; }
		public string NamaEkstraktor { get; set; }
		public string Alamat { get; set; }
		public string TanggalPengiriman { get; set; }
		public string TanggalDiterimaGet { get; set; }
		public int QtyDiterima { get; set; }
			
	}
}