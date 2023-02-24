using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MoUModel
    {
        
    }
    public class MoUAttribute
    {
        public string RegID { get; set; }
        public string NoBatch { get; set; }
        public int QtyMoU { get; set; }
        public string MoU { get; set; }
        public int QtyBatch { get; set; }
        public string AlamatMoU { get; set; }
        public string TanggalEdukasi { get; set; }
        public string KTPKoord { get; set; }
        public string NPWPKoord { get; set; }
        public string RekeningKoord { get; set; }
        public string KTPKoordAttch { get; set; }
        public string NPWPKoordAttch { get; set; }
        public string BukuTabunganKoordAttch { get; set; }
        public string DokumenMoUAttch { get; set; }
        public string NamaPetani { get; set; }
        public string AlamatPetani { get; set; }
        public string NoKTPPetani { get; set; }
        public string KTPPetaniAttch { get; set; }
        public int LuasLahan { get; set; }
    }
}