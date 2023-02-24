using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PenanamanModel
    {
    }
    public class PenanamanAttribute
    {
		public string NoRegistrasi { get; set; }
		public string DokFotoPenanaman { get; set; }
		public int LuasAreaTotal { get; set; }
		public int WaktuTanam { get; set; }
		public string NoBatch { get; set; }
		public int JumlahBibit { get; set; }
		public string TanggalTanam { get; set; }
		public string UkuranGuludan { get; set; }
		public int JumlahPupukAwal { get; set; }
		public string Varietas { get; set; }
		public int PenggunaanPupuk { get; set; }
		public string JenisMoulsa { get; set; }
		public string NamaPupuk { get; set; }
		public string WaktuPemupukan { get; set; }


	}
}