using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerce.Models
{
    public class OrderDetail
    {
        public virtual int OrderDetailId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int IID { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}