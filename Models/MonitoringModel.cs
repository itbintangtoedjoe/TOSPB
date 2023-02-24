using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MonitoringModel
    {
    }
	public class MonitoringAttribute
	{
		public string NoBatchGet { get; set; }
		public int MonitoringKe { get; set; }
		public string DokMonitoringAttch { get; set; }
		public int UsiaTanaman { get; set; }
		public string TerkenaPenyakit { get; set; }
		public string Kematian { get; set; }
		public string TanggalPemupukan { get; set; }
		public string Pupukygdigunakan { get; set; }
		public int JumlahPenggunaanPupuk { get; set; }
		public string TanggalMonitoring { get; set; }

	}
}