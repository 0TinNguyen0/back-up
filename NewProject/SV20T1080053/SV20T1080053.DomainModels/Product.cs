using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1080053.DomainModels
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = "";
        public string SupplierID { get; set; } = "";
        public string CategoryID { get; set; } = "";
        public string Unit { get; set; } = "";
        public string Price { get; set; } = "";
        public string Photo { get; set; } = "";
        public string IsSelling { get; set; }
    }
}
