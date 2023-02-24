using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DapperModel
    {
        public class InsertPaymentDet
        {
            public List<InsertPaymentDetAttr> PaymentDetail { get; set; }

        }
        public class InsertPaymentDetAttr
        {
            public string id { get; set; }
            public string PERIODSPB { get; set; }
            public string REG { get; set; }
            public string BANK { get; set; }
            public string NOKTP { get; set; }
            public string NOMORREKENING { get; set; }
            public string NAMATEAM { get; set; }
            public string NOSURAT { get; set; }
            public string TOTAL_INCOME { get; set; }
        }
        public class DynamicModel
        {
            public Dictionary<string, object> Model { get; set; }
        }
    }
}