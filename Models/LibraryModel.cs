using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAOnline.Models
{
    public class LibraryModel
    {
        [Display(Name = "QRCode Text")]
        public string QRCodeText { get; set; }
        [Display(Name = "QRCode Image")]
        public string QRCodeImagePath { get; set; }
        public string NoSurat { get; set; }
        public string NoTO { get; set; }
        public string Flag { get; set; }
    }
}