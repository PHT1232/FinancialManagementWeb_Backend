using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.ReceiptComponents
{
    public class ReceiptItem
    {
        public Receipt receipt { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public float TotalAmount { get; set; }
    }
}
