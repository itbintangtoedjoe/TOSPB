using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MonitoringPanenModel
    {
    }
    public class MonitoringPanenAttribute
    {
		public string NoBatch { get; set; }
		public string DokumentasiAttch { get; set; }
		public int JumlahJaheBasah { get; set; }
		public int JumlahJaheKering { get; set; }
		public int JumlahPetaniPekerja { get; set; }
		public string KadarAir { get; set; }
		public string Pengeringan { get; set; }
		public string NamaPengering { get; set; }
		public string MetodePengeringan { get; set; }
		public string WaktuPengeringan { get; set; }
		public int JumlahJaheKeringBagus { get; set; }
		public int JumlahJaheKeringAyakan { get; set; }
		public int JumlahJaheRusak { get; set; }
		public string Slicing { get; set; }
		public string TempatPengeringan { get; set; }

	}
}