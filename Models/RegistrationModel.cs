using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RegistrationModel
    {
    }

    public class RegistrationAttribute {
		public string RegID { get; set; }
		public string RejectReason { get; set; }
		public string NamaLengkap { get; set; }
		public string TanggalLahir { get; set; }
		public string JenisKelamin { get; set; }
		public int MemilikiLahan { get; set; }
		public float LuasLahan { get; set; }
		public int SaluranIrigasi { get; set; }
		public float JarakIrigasi { get; set; }
		public int PengalamanJaheMerah { get; set; }
		public string TerkahirMenanamJaheMerah { get; set; }
		public string Email { get; set; }
		public string NoTelp { get; set; }
    }
}